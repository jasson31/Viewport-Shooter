using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObject : MonoBehaviour
{
    public new Camera camera;

    public bool IsObjectVisible(GameObject gameObject)
    {
        RaycastHit hit;
        if(GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), gameObject.GetComponent<MeshRenderer>().bounds))
        {
            Physics.Raycast(camera.transform.position, gameObject.transform.position - camera.transform.position, out hit, Vector3.Distance(camera.transform.position, gameObject.transform.position));
            if (hit.transform != gameObject.transform) return false;
            else return true;
        }
        return false;
    }

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
