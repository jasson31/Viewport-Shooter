using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : SingletonBehaviour<CameraManager>
{
    public RawImage broadcastScreen;
    public RenderTexture renderTexture;
    public CameraObject[] cameraObjects;
    public int currentCamera;

    RenderTexture[] renderTextures;

    // Start is called before the first frame update
    void Start()
    {
        renderTextures = new RenderTexture[6];
        for (int i = 0; i < 6; i++)
        {
            renderTextures[i] = new RenderTexture(renderTexture);
            cameraObjects[i].camera.targetTexture = renderTextures[i];
            //cameraObjects[i].enabled = false;
        }
        ChangeCamera(0);
    }

    void ChangeCamera(int i)
    {
        cameraObjects[currentCamera].enabled = false;
        currentCamera = i;
        broadcastScreen.texture = cameraObjects[currentCamera].camera.targetTexture;
        cameraObjects[currentCamera].enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0) ChangeCamera((currentCamera + (scrollInput > 0 ? 1 : 5)) % 6);

        if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeCamera(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeCamera(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeCamera(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) ChangeCamera(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5)) ChangeCamera(4);
        else if (Input.GetKeyDown(KeyCode.Alpha6)) ChangeCamera(5);

        Camera camera = cameraObjects[currentCamera].camera;
        //Gizmos.DrawFrustum(camera.transform.position, camera.fieldOfView, camera.farClipPlane, camera.nearClipPlane, camera.aspect);

        Vector3[] frustumCorners = new Vector3[6];
        camera.CalculateFrustumCorners(new Rect(0, 0, 1, 1), camera.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, frustumCorners);
        frustumCorners[4] = frustumCorners[0] + frustumCorners[1];
        frustumCorners[5] = frustumCorners[2] + frustumCorners[3];
        Debug.Log(frustumCorners[4]);
        Debug.Log(frustumCorners[5]);

        for (int i = 4; i < 6; i++)
        {
            var worldSpaceCorner = camera.transform.TransformVector(frustumCorners[i]);
            Debug.DrawRay(camera.transform.position, worldSpaceCorner, Color.blue);
        }
    }
}
