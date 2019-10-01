using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMovingCamera : MonoBehaviour
{
    public GameObject[] cameras;
    public GameObject[] destination;
    bool initiateMove = false;
    float moveLerp = 0f;
    public float moveTerm = 3f;

    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            GetComponent<Collider>().enabled = false;
            foreach (SpawnInfo spwn in GetComponentsInChildren<SpawnInfo>())
            {
                initiateMove = true;
            }
        }
    }
}
