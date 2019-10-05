using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObject : MonoBehaviour
{
    public new Camera camera;

    public bool IsObjectVisible(GameObject _gameObject)
    {
        Vector3 screenPoint = camera.WorldToViewportPoint(_gameObject.transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        if (onScreen)
        {
            RaycastHit hit;
            if(Physics.Raycast(camera.transform.position, _gameObject.transform.position + new Vector3(0, 0.5f, 0) - camera.transform.position, out hit))
                if (hit.transform == _gameObject.transform) return true;
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
