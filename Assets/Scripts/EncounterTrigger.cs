using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterTrigger : MonoBehaviour
{
    public Transform EncounterHolder;
    public GameObject Exit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int childnum = EncounterHolder.childCount;
            EncounterManager.Instance.SetUpEncounter(childnum,Exit);
            for (int i = 0; i < childnum; i++)
            {
                EncounterHolder.GetChild(i).GetComponent<EnemyAI>().StartAlert();
            }
            gameObject.SetActive(false);
        }
    }
}
