using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObject : MonoBehaviour
{
    public new Camera camera;
    public GameObject sightVisualization;

    public bool IsVisible(GameObject meshObject, GameObject colliderObject, float yOffset = 0)
    {
        if (CameraManager.inst.currentCamera == 0 && colliderObject == PlayerController.inst.gameObject) return false;
        bool onScreen = GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), meshObject.GetComponent<Renderer>().bounds);
        if (onScreen)
        {
            RaycastHit hit;
            LayerMask layerMask = CameraManager.inst.currentCamera == 0 ? 
                ~(1 << LayerMask.NameToLayer("Head") | 1 << LayerMask.NameToLayer("TPS Player") | 1 << LayerMask.NameToLayer("FPS Player") | 1 << LayerMask.NameToLayer("Ignore Raycast")) :
                ~(1 << LayerMask.NameToLayer("Ignore Raycast"));
            float distance = Vector3.Distance(colliderObject.transform.position + new Vector3(0, yOffset, 0), camera.transform.position);
            if (Physics.Raycast(camera.transform.position, colliderObject.transform.position + new Vector3(0, yOffset, 0) - camera.transform.position, out hit, distance, layerMask))
            {
                if (hit.transform == colliderObject.transform) return true;
            }
            else return true;
        }
        return false;
    }

    public bool IsFaceVisible()
    {
        return (CameraManager.inst.currentCamera != 0 && 
            Vector3.Angle(GameObject.Find("Player").transform.forward, camera.transform.position - GameObject.Find("Player").transform.position) < 45) ? true : false;
    }

    public void ActivateCamera(bool active)
    {
        camera.gameObject.SetActive(active);
    }

}
