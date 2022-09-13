using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    static UIManager instance;

    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _gameOverText;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject();
                instance = go.AddComponent<UIManager>();
                go.name = "(Singleton) UIManager";
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ModifyHealth(int _healthValue)
    {
        if (!_healthText)
        {
            instance._healthText = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();
        }
        _healthText.text = "Health: " + _healthValue;
    }
}
