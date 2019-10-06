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
        transform.LookAt(player.transform);
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
    }
}
