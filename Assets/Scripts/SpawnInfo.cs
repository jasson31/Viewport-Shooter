using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInfo : MonoBehaviour
{
    public float delay;
    GameObject obj;
    
    public void SpawnSelf(GameObject _obj)
    {
        obj = _obj;
        StartCoroutine(DelaySpawn());
    }
    IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(delay);
        Instantiate(obj, transform.position, Quaternion.identity);
    }
}
