using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : SingletonBehaviour<PlayerController>
{
    public PlayerInput input;

    public GameObject firePoint, fpsGun, tpsGun;
    public GameObject bodyMesh, headMesh;
    public ParticleSystem fpsMuzzleFlash, tpsMuzzleFlash;
    public RawImage hitUI;
    public float speed = 10, yVelocity = 0, gravity = -20;
    CharacterController characterController = null;
    Animator playerAnimator, tpsGunAnimator, fpsGunAnimator;
    Camera mainCamera = null;
    float rotationX = 0, rotationY = 0, sensitivity = 100, keyInputX, keyInputZ;

    public AudioSource gunShotAS;
    public void ResetPlayer()
    {
        characterController.enabled = false;
        rotationX = rotationY = 0;
        gameObject.transform.position = Vector3.zero;
        characterController.enabled = true;
    }

    public IEnumerator TurnHitUIOn()
    {
        float duration = 0.05f, smoothness = 0.01f;
        float progress = 0, diff = smoothness / duration;
        Color curCol = hitUI.color;
        curCol.a = 0.4f;
        while (progress < 1)
        {
            hitUI.color = Color.Lerp(hitUI.color, curCol, progress);
            progress += diff;
            yield return new WaitForSeconds(smoothness);
        }
        progress = 0;
        curCol.a = 0;
        while (progress < 1)
        {
            hitUI.color = Color.Lerp(hitUI.color, curCol, progress);
            progress += diff;
            yield return new WaitForSeconds(smoothness);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
        fpsGunAnimator = fpsGun.GetComponent<Animator>();
        tpsGunAnimator = tpsGun.GetComponent<Animator>();
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Player move
        Vector3 direction = new Vector3(input.horizontalInput * 0.5f, 0, input.verticalInput);
        playerAnimator.SetBool("isWalking", direction.magnitude != 0);
        bool forward = false, backward = false, left = false, right = false;
        if (input.verticalInput != 0 || input.horizontalInput != 0)
        {
            if (Mathf.Abs(input.verticalInput) > Mathf.Abs(input.horizontalInput))
            {
                if (input.verticalInput > 0) forward = true;
                else backward = true;
            }
            else
            {
                if (input.horizontalInput > 0) right = true;
                else left = true;
            }
        }
        playerAnimator.SetBool("isWalkingForward", forward);
        playerAnimator.SetBool("isWalkingBackward", backward);
        playerAnimator.SetBool("isWalkingLeft", left);
        playerAnimator.SetBool("isWalkingRight", right);
        characterController.Move(transform.TransformDirection(direction) * speed * Time.deltaTime);
        yVelocity += gravity * Time.deltaTime;
        direction.y = yVelocity;
        characterController.Move(direction * Time.deltaTime);

        //Camera rotate
        rotationX += input.mouseXInput * Time.deltaTime * sensitivity;
        rotationY += input.mouseYInput * Time.deltaTime * sensitivity;
        rotationY = Mathf.Clamp(rotationY, -50, 30);
        transform.eulerAngles = new Vector3(0, rotationX, 0);
        mainCamera.transform.eulerAngles = new Vector3(-rotationY, mainCamera.transform.eulerAngles.y, 0);
        playerAnimator.SetFloat("LookAngle", (rotationY + 50) / 80);

        //Shoot
        if(input.fireInput)
        {
            BulletFactory.inst.MakeBullet(firePoint.transform.position, firePoint.transform.rotation, firePoint.transform.up);
            tpsMuzzleFlash.Play();
            fpsMuzzleFlash.Play();
            fpsGunAnimator.SetTrigger("Fire");
            tpsGunAnimator.SetTrigger("Fire");
            gunShotAS.PlayOneShot(gunShotAS.clip);
        }
    }
}
