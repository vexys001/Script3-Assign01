using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    private int _remainingHealth;

    //Reload
    private string _lastScene;

    //Stats
    private int _score = 0;
    private int _deaths = 0;

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
    public int Score { get => _score; set => _score = value; }
    public int Deaths { get => _deaths; set => _deaths = value; }

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

        _lastScene = sceneName;
    }

    public void LoadDeathScreen()
    {
        SceneManager.LoadScene("Death", LoadSceneMode.Single);
        _deaths++;
    }

    public void ReloadLastScene()
    {
        SceneManager.LoadScene(_lastScene, LoadSceneMode.Single);

        _score -= 100;
        _remainingHealth = 100;
    }

    public void AddScore(int added)
    {
        _score += added;
        UIManager.Instance.ModifyScore(_score);
    }
}
