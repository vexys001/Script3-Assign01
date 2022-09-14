using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    static EncounterManager instance;

    [SerializeField] private float _enemyNum;
    [SerializeField] private GameObject _exit;

    public static EncounterManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject();
                instance = go.AddComponent<EncounterManager>();
                go.name = "(Singleton) EncounterManager";
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

    public void SetUpEncounter(int enemyNum, GameObject exit)
    {
        _enemyNum = enemyNum;
        _exit = exit;
    }

    public void KilledEnemy()
    {
        _enemyNum--;

        GameManager.Instance.AddScore(25);

        if (_enemyNum <= 0)
        {
            _exit.SetActive(false);
        }
    }
}
