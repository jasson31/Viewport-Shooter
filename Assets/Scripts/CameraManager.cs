using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : SingletonBehaviour<CameraManager>
{
    public int cameraInput;

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

    public void ChangeCamera(int i)
    {
        cameraObjects[currentCamera].enabled = false;
        currentCamera = i;
        broadcastScreen.texture = cameraObjects[currentCamera].camera.targetTexture;
        cameraObjects[currentCamera].enabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cameraInput != currentCamera)
            ChangeCamera(cameraInput);
    }
}
