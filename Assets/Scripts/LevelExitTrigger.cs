using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExitTrigger : MonoBehaviour
{
    [SerializeField] private string _nextLevelName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int remainingHealth = other.GetComponent<Avatar>().GetHealth();

            GameManager.Instance.LoadNextScene(_nextLevelName, remainingHealth);
        }
    }
}
