using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poundable : MonoBehaviour
{
    public void GotPounded()
    {
        gameObject.SetActive(false);
        if (GetComponent<EnemyAI>()) { EncounterManager.Instance.KilledEnemy(); }
    }
}
