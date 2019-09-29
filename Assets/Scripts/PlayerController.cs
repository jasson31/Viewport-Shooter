using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10, yVelocity = 0, gravity = -20, inputX, inputZ;
    CharacterController characterController = null;
    Animator animator = null;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(inputX * 0.5f, 0, inputZ);
        animator.SetBool("isWalking", direction.magnitude != 0);
        bool forward = false, backward = false, left = false, right = false;
        if (inputZ != 0 || inputX != 0)
        {
            if (Mathf.Abs(inputZ) > Mathf.Abs(inputX))
            {
                if (inputZ > 0) forward = true;
                else backward = true;
            }
            else
            {
                if (inputX > 0) right = true;
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
        rotationX += mouseMoveValueX * 100 * Time.deltaTime;
        rotationY += mouseMoveValueY * 100 * Time.deltaTime;

        rotationY = Mathf.Clamp(rotationY, -20, 40);
        transform.eulerAngles = new Vector3(-rotationY, rotationX, 0);
    }
    float rotationX = 0, rotationY = 0;
}
