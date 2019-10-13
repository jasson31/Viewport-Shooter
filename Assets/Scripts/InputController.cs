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
        horizontalInput = verticalInput = mouseXInput = mouseYInput = 0;
        fireInput = false;
    }
}

public class InputController : MonoBehaviour
{
    public PlayerInput input;

    public bool isFixedUpdated = false;

    private void Awake()
    {
        input = new PlayerInput();
    }

    private void Update()
    {
        if (!GameManager.inst.isGameOver)
        {
            if (isFixedUpdated)
            {
                input.ClearInput();
                isFixedUpdated = false;
            }

            //Player move
            input.horizontalInput += Input.GetAxis("Horizontal");
            input.verticalInput += Input.GetAxis("Vertical");

            //Camera rotate
            input.mouseXInput += Input.GetAxis("Mouse X");
            input.mouseYInput += Input.GetAxis("Mouse Y");

            input.fireInput |= Input.GetMouseButtonDown(0);

            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput != 0)
            {
                input.cameraInput = (CameraManager.inst.currentCamera + (Input.GetAxis("Mouse ScrollWheel") > 0 ? 1 : 5)) % 6;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Alpha1)) input.cameraInput = 0;
                else if (Input.GetKeyDown(KeyCode.Alpha2)) input.cameraInput = 1;
                else if (Input.GetKeyDown(KeyCode.Alpha3)) input.cameraInput = 2;
                else if (Input.GetKeyDown(KeyCode.Alpha4)) input.cameraInput = 3;
                else if (Input.GetKeyDown(KeyCode.Alpha5)) input.cameraInput = 4;
                else if (Input.GetKeyDown(KeyCode.Alpha6)) input.cameraInput = 5;
            }

            CameraManager.inst.cameraInput = input.cameraInput;
            PlayerController.inst.input = input;
        }
    }
}