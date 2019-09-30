using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateEnemySpawn : MonoBehaviour
{
    public GameObject enemyObj;
    void Start()
    {
        foreach(Transform trns in GetComponentsInChildren<Transform>())
        {
            Instantiate(enemyObj, trns.position, Quaternion.identity);
        }
    }

}
