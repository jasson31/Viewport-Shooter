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

    public GameObject cameraIconPrefab;
    public Transform canvas;
    private GameObject[] cameraIcons;
    public Sprite[] cameraIconSprite;
    public Sprite[] cameraIconOnSprite;
    public float iconMinSize;
    public float iconMaxSize;
    public float iconMaxDist;
    public Camera mainCam;

    public AudioSource playerAS;

    // Start is called before the first frame update
    void Start()
    {
        cameraIcons = new GameObject[6];
        for(int i = 0; i < 6; i++)
        {
            cameraObjects[i].camera.targetTexture = renderTexture;
            cameraIcons[i] = Instantiate(cameraIconPrefab, canvas.Find("PlayUI"));
            cameraIcons[i].GetComponent<Image>().sprite = cameraIconSprite[i];
        }
        ChangeCamera(0);
    }

    public void ChangeCamera(int i)
    {
        cameraObjects[currentCamera].ActivateCamera(false);
        cameraIcons[currentCamera].GetComponent<Image>().sprite = cameraIconSprite[currentCamera];
        currentCamera = i;
        cameraObjects[currentCamera].ActivateCamera(true);
        cameraIcons[currentCamera].GetComponent<Image>().sprite = cameraIconOnSprite[i];
        playerAS.PlayOneShot(playerAS.clip);

        AudioSource camAS = cameraObjects[currentCamera].GetComponent<AudioSource>();
        if (currentCamera > 0) camAS.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cameraInput != currentCamera)
            ChangeCamera(cameraInput);
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
