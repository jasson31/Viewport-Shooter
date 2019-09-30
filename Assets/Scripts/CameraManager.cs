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
        for(int i = 0; i < 2; i++) cameraObjects[i].camera.targetTexture = renderTextures[i];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentCamera = 0;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentCamera = 1;
        }
        broadcastScreen.texture = cameraObjects[currentCamera].camera.targetTexture;
    }
}
