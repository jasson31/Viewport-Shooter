using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayController : SingletonBehaviour<ReplayController>
{

    [SerializeField]
    ReplayLogger logger;

    [SerializeField]
    PlayerController playerController;

    private bool replaying = false;

    private float replayStartTime;
    private float gameDuration;

    public void Begin(float _gameDuration)
    {
        replaying = true;
        gameDuration = _gameDuration;
        replayStartTime = Time.time;

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            Debug.Log("d");
            playerController.ResetPlayer();
            Begin(Time.time);
        }
    }

    void FixedUpdate()
    {
        if (replaying)
        {
            if(Time.time - replayStartTime < gameDuration)
            {
                if (logger.inputQueue.Count > 0)
                {
                    playerController.input = logger.inputQueue.Dequeue();
                }
            }
        }
    }
}
