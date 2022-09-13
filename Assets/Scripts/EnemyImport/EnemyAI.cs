using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAI : MonoBehaviour
{
    public enum AIStates { Idle, Alert }

    [Header("AI")]
    [SerializeField] protected NavMeshAgent _navAgent;
    [SerializeField] protected AIStates _currentState;

    public GameObject Target;

    //Called when the script instance is being loaded
    protected virtual void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();

        Target = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        StartIdle();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        RunStates();
    }

    void RunStates()
    {
        if (_currentState == AIStates.Idle) Idle();
        else if (_currentState == AIStates.Alert) Alert();
    }

    protected abstract void Move();

    public abstract void StartIdle();
    protected abstract void Idle();
    public void StartAlert()
    {
        _navAgent.enabled = true;
        _currentState = AIStates.Alert;
    }

    public void Alert()
    {
        Move();
    }
}
