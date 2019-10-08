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

    public GameObject cameraIconPrefab;
    public Transform canvas;
    private GameObject[] cameraIcons;
    public Sprite[] cameraIconSprite;
    public Sprite[] cameraIconOnSprite;
    public float iconMinSize;
    public float iconMaxSize;
    public float iconMaxDist;
    public Camera mainCam;

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

        cameraIcons = new GameObject[6];
        for(int i = 0; i < 6; i++)
        {
            cameraIcons[i] = Instantiate(cameraIconPrefab, canvas);
            cameraIcons[i].GetComponent<Image>().sprite = cameraIconSprite[i];
        }
        ChangeCamera(0);
    }

    void ChangeCamera(int i)
    {
        cameraObjects[currentCamera].ActivateCamera(false);
        cameraIcons[currentCamera].GetComponent<Image>().sprite = cameraIconSprite[currentCamera];
        currentCamera = i;
        broadcastScreen.texture = cameraObjects[currentCamera].camera.targetTexture;
        cameraObjects[currentCamera].ActivateCamera(true);
        cameraIcons[currentCamera].GetComponent<Image>().sprite = cameraIconOnSprite[i];
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

        Camera current = cameraObjects[1].camera;
        Gizmos.DrawFrustum(current.transform.position, current.fieldOfView, current.farClipPlane, current.nearClipPlane, current.aspect);
        DrawCameraIcon();
    }

    void DrawCameraIcon()
    {
        for(int i = 1; i < 6; i++)
        {
            float dist = Vector3.Distance(cameraObjects[i].transform.position, mainCam.transform.position);
            Vector3 screenPoint = mainCam.WorldToViewportPoint(cameraObjects[i].transform.position);
            bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
            if (dist <= iconMaxDist && onScreen)
            {
                float size = Mathf.Lerp(iconMinSize, iconMaxSize, 1 - dist / iconMaxDist);
                cameraIcons[i].GetComponent<Image>().color = new Color(1, 1, 1, 1 - dist / iconMaxDist);
                cameraIcons[i].GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
                cameraIcons[i].GetComponent<RectTransform>().position = mainCam.WorldToScreenPoint(cameraObjects[i].transform.position);
            }
            else
            {
                cameraIcons[i].GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
        }
    }
}
