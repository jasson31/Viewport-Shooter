using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public RawImage broadcastScreen;
    public RenderTexture[] renderTextures;
    public Camera[] cameras;
    public int currentCamera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            broadcastScreen.texture = renderTextures[0];
            currentCamera = 0;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            broadcastScreen.texture = renderTextures[1];
            currentCamera = 1;
        }
    }
}
