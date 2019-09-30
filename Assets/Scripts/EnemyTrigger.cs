using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public GameObject enemyObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            GetComponent<Collider>().enabled = false;
            foreach (SpawnInfo spwn in GetComponentsInChildren<SpawnInfo>())
            {
                spwn.SpawnSelf(enemyObj);
            }
        }
    }
    
}
