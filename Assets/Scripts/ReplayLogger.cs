using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayLogger : MonoBehaviour
{
    public Queue<PlayerInput> inputQueue;
    public InputController inputController;
    public bool isLogging = true;

    float replayStartTime;

    // Start is called before the first frame update
    void Start()
    {
        inputQueue = new Queue<PlayerInput>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isLogging)
        {
            inputQueue.Enqueue(inputController.input);
        }
    }
}
