using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10, yVelocity = 0, gravity = -20;
    CharacterController characterController = null;
    Animator animator = null;
    Camera mainCamera = null;
    float rotationX = 0, rotationY = 0, sensitivity = 100, keyInputX, keyInputZ;
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        keyInputX = Input.GetAxis("Horizontal");
        keyInputZ = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(keyInputX * 0.5f, 0, keyInputZ);
        animator.SetBool("isWalking", direction.magnitude != 0);
        bool forward = false, backward = false, left = false, right = false;
        if (keyInputZ != 0 || keyInputX != 0)
        {
            if (Mathf.Abs(keyInputZ) > Mathf.Abs(keyInputX))
            {
                if (keyInputZ > 0) forward = true;
                else backward = true;
            }
            else
            {
                if (keyInputX > 0) right = true;
                else left = true;
            }
        }
        animator.SetBool("isWalkingForward", forward);
        animator.SetBool("isWalkingBackward", backward);
        animator.SetBool("isWalkingLeft", left);
        animator.SetBool("isWalkingRight", right);
        characterController.Move(transform.TransformDirection(direction) * speed * Time.deltaTime);
        yVelocity += gravity * Time.deltaTime;
        direction.y = yVelocity;
        characterController.Move(direction * Time.deltaTime);


        float mouseMoveValueX = Input.GetAxis("Mouse X");
        float mouseMoveValueY = Input.GetAxis("Mouse Y");
        rotationX += mouseMoveValueX * Time.deltaTime * sensitivity;
        rotationY += mouseMoveValueY * Time.deltaTime * sensitivity;

        rotationY = Mathf.Clamp(rotationY, -20, 30);
        transform.eulerAngles = new Vector3(0, rotationX, 0);
        mainCamera.transform.eulerAngles = new Vector3(-rotationY, mainCamera.transform.eulerAngles.y, 0);
        animator.SetFloat("LookAngle", (rotationY + 20) / 50);
    }
}
