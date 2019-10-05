using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObject : MonoBehaviour
{
    public new Camera camera;

    public bool IsObjectVisible(GameObject _gameObject)
    {
        RaycastHit hit;
        if (_gameObject.GetComponent<Renderer>())
        {
            if (GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), _gameObject.GetComponent<Renderer>().bounds))
            {
                Physics.Raycast(camera.transform.position, _gameObject.transform.position - camera.transform.position, out hit, Vector3.Distance(camera.transform.position, _gameObject.transform.position));
                if (hit.transform != _gameObject.transform) return false;
                else return true;
            }
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
