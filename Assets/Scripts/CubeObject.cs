using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObject : MonoBehaviour
{
    bool isVisibleForCamera1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //https://stackoverflow.com/questions/46364719/unity-how-do-i-check-if-an-object-is-seen-by-the-main-camera
        isVisibleForCamera1 = GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(GameObject.Find("CameraManager").GetComponent<CameraManager>().cameras[GameObject.Find("CameraManager").GetComponent<CameraManager>().currentCamera].GetComponent<Camera>()), GetComponent<MeshRenderer>().bounds);
        Debug.Log(isVisibleForCamera1);
    }
}
