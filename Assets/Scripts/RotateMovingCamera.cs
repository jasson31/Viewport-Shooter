using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMovingCamera : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = PlayerController.inst.gameObject;
    }

    void Update()
    {
        float befAngle = transform.eulerAngles.y;

        transform.LookAt(player.transform);
        
        transform.eulerAngles = new Vector3(0,Mathf.LerpAngle(befAngle, transform.eulerAngles.y, 0.05f),0);
    }
}
