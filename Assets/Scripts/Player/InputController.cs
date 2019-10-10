using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerInput
{
    public float horizontalInput;
    public float verticalInput;
    public float mouseXInput;
    public float mouseYInput;
    public int cameraInput;
    public bool fireInput;
    public void ClearInput()
    {
        horizontalInput = verticalInput = mouseXInput = mouseYInput = cameraInput = 0;
        fireInput = false;
    }
}

public class InputController : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerInput input;
    private void Awake()
    {
        input = new PlayerInput();
    }

    private void Update()
    {

        input.ClearInput();

        //Player move
        input.horizontalInput = Input.GetAxis("Horizontal");
        input.verticalInput = Input.GetAxis("Vertical");

        //Camera rotate
        input.mouseXInput = Input.GetAxis("Mouse X");
        input.mouseYInput = Input.GetAxis("Mouse Y");

        input.fireInput = Input.GetMouseButtonDown(0);
        input.cameraInput = CameraManager.inst.currentCamera;




        playerController.input = input;
    }
}