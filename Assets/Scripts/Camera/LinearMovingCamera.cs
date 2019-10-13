using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMovingCamera : MonoBehaviour
{
    public GameObject[] cameras;
    public Transform[] destPos;
    Vector3[] startPos;
    bool initiateMove = false;
    float moveLerp = 0f;
    public float moveTerm = 3f;

    public void ResetMovingCamera()
    {
        cameras[0].transform.position = startPos[0];
        cameras[1].transform.position = startPos[1];
        initiateMove = false;
        moveLerp = 0;
        GetComponent<Collider>().enabled = true;
    }

    private void Start()
    {
        startPos = new Vector3[2];
        startPos[0] = cameras[0].transform.position;
        startPos[1] = cameras[1].transform.position;
    }

    private void Update()
    {
        if(initiateMove)
        {
            moveLerp = Mathf.Min(moveLerp + Time.deltaTime, moveTerm);
            cameras[0].transform.position = Vector3.Lerp(startPos[0], destPos[0].position, moveLerp / moveTerm);
            cameras[1].transform.position = Vector3.Lerp(startPos[1], destPos[1].position, moveLerp / moveTerm);
            if (moveLerp == moveTerm) initiateMove = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            GetComponent<Collider>().enabled = false;
            initiateMove = true;
        }
    }
}
