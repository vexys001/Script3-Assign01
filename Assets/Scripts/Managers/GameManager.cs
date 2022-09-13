using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    private int _remainingHealth = 100;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject();
                instance = go.AddComponent<GameManager>();
                go.name = "(Singleton) GameManager";
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    public int RemainingHealth { get => _remainingHealth; set => _remainingHealth = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void LoadNextScene(string sceneName, int health)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        _remainingHealth = health;
    }
}
