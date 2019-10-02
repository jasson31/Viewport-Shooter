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
        for (int i = 0; i < 6; i++) renderTextures[i] = new RenderTexture(renderTexture);
        for(int i = 0; i < 6; i++) cameraObjects[i].camera.targetTexture = renderTextures[i];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentCamera = 0;
        else if(Input.GetKeyDown(KeyCode.Alpha2)) currentCamera = 1;
        else if(Input.GetKeyDown(KeyCode.Alpha3)) currentCamera = 2;
        else if(Input.GetKeyDown(KeyCode.Alpha4)) currentCamera = 3;
        else if(Input.GetKeyDown(KeyCode.Alpha5)) currentCamera = 4;
        else if(Input.GetKeyDown(KeyCode.Alpha6)) currentCamera = 5;
        broadcastScreen.texture = cameraObjects[currentCamera].camera.targetTexture;
    }
}
