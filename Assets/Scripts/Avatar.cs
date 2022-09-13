using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    [Header("Components")]
    private CharacterController _cc;
    public Transform Cam;
    public Transform GroundCheck;
    private AudioSource _audioSource;

    [Header("Health")]
    [SerializeField] private int _maxHealth = 100;
    private int _health;

    [Header("Movement")]
    public float Speed = 6;
    public float TurnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;

    [Header("Gravity")]
    public float Gravity = -9.81f;
    public float GroundDistance = 0.4f;
    public float MinVelocity = -2f;
    public LayerMask GroundMask;

    private Vector3 _velocity;
    private bool _isGrounded;

    [Header("Jump")]
    public float JumpHeight = 5f;

    [Header("Statue Mode")]
    [SerializeField] private bool _stoneMode = false;
    public float StoneModeLength = 2;

    [SerializeField] private Material _originalMat;
    [SerializeField] private Material _stoneMat;
    [SerializeField] private GameObject[] _buttGO;
    [SerializeField] private Material _buttMat;
    private Renderer _renderer;

    [Header("Ground Pound")]
    [SerializeField] private GameObject _shockwaveGO;

    private bool _pounded = false;
    [SerializeField] private float _groundPoundSpeedMult = 3;
    public float PoundDistance = 6f;
    public LayerMask PoundMask;



    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _cc = GetComponent<CharacterController>();
        _audioSource = GetComponent<AudioSource>();

        _renderer = GetComponent<Renderer>();
        _originalMat = _renderer.material;
        _shockwaveGO.SetActive(false);

        _health = GameManager.Instance.RemainingHealth;

        UIManager.Instance.ModifyHealth(_health);
    }

    // Update is called once per frame
    void Update()
    {
        ApplyGravity();
        if (!_stoneMode)
        {
            Move();
            Jump();
        }
        StoneOn();
    }

    void ApplyGravity()
    {
        _isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = MinVelocity;

            if (_stoneMode && !_pounded)
            {
                Shockwave();
            }
        }

        _velocity.y += !_stoneMode ? Gravity * Time.deltaTime : Gravity * _groundPoundSpeedMult * Time.deltaTime;

        _cc.Move(_velocity * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(JumpHeight * -2 * Gravity);
        }
    }

    private void Move()
    {
        //Input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            //Rotation
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, TurnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //Movement
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0f) * Vector3.forward;
            _cc.Move(moveDir.normalized * Speed * Time.deltaTime);
        }
    }

    private void StoneOn()
    {
        if (Input.GetKeyDown(KeyCode.E) && !_stoneMode && !_isGrounded)
        {
            _stoneMode = true;
            _renderer.material = _stoneMat;

            foreach (GameObject cheek in _buttGO)
            {
                cheek.GetComponent<Renderer>().material = _buttMat;
            }

            StartCoroutine("TurnOffStoneMode");
        }
    }

    private IEnumerator TurnOffStoneMode()
    {
        yield return new WaitForSeconds(StoneModeLength);

        _stoneMode = false;

        _renderer.material = _originalMat;
        foreach (GameObject cheek in _buttGO)
        {
            cheek.GetComponent<Renderer>().material = _originalMat;
        }

        _pounded = false;
    }

    private void Shockwave()
    {
        _pounded = true;
        _shockwaveGO.SetActive(true);
        _audioSource.Play();

        foreach (GameObject cheek in _buttGO)
        {
            cheek.GetComponent<Renderer>().material = _stoneMat;
        }

        //_isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);
        Collider[] poundHits = Physics.OverlapSphere(GroundCheck.position, PoundDistance, PoundMask);

        foreach (Collider hit in poundHits)
        {
            hit.GetComponent<Poundable>().GotPounded();
        }

        StartCoroutine("TurnOffShockwave");
    }

    private IEnumerator TurnOffShockwave()
    {
        yield return new WaitForSeconds(0.5f);

        _shockwaveGO.SetActive(false);
    }

    public void Heal(int healValue)
    {
        _health += healValue;
        if (_health > _maxHealth) { _health = _maxHealth; }
        UIManager.Instance.ModifyHealth(_health);
    }

    public void TakeDamage(int damageValue)
    {
        if (!_stoneMode)
        {
            _health -= damageValue;
            UIManager.Instance.ModifyHealth(_health);
            if (_health <= 0) { Die(); }
        }
    }

    public int GetHealth()
    {
        return _health;
    }

    private void Die()
    {
        GameManager.Instance.LoadNextScene("GameOverLost", _health);
    }
}
