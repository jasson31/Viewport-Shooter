using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateEnemySpawn : MonoBehaviour
{
    public GameObject enemyObj;

    public void EnemySpawn()
    {
        foreach (Transform trns in GetComponentsInChildren<Transform>())
        {
            if (trns != transform)
                Instantiate(enemyObj, trns.position, Quaternion.identity, GameObject.Find("Enemies").transform);
        }
    }
    void Start()
    {
        EnemySpawn();
    }

}
