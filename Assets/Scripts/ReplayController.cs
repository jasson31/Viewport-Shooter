using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayController : SingletonBehaviour<ReplayController>
{

    [SerializeField]
    ReplayLogger logger;

    public bool replaying = false;

    private float replayStartTime;
    private float gameDuration;

    public void Begin(float _gameDuration)
    {
        PlayerController.inst.ResetPlayer();
        CameraManager.inst.ChangeCamera(0);
        foreach (Transform child in GameObject.Find("Enemies").transform)
        {
            Destroy(child.gameObject);
        }
        GameObject.Find("InitiateSpawn").GetComponent<InitiateEnemySpawn>().EnemySpawn();
        foreach(Transform child in GameObject.Find("EnemyTriggers").transform)
        {
            child.GetComponent<Collider>().enabled = true;
        }
        replaying = true;
        gameDuration = _gameDuration;

        CameraManager.inst.broadcastScreen.rectTransform.anchoredPosition = new Vector3(-960, 0);
        CameraManager.inst.broadcastScreen.rectTransform.sizeDelta = new Vector3(1920, 1080);

        replayStartTime = Time.time;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0) && !replaying)
        {
            Begin(Time.time);
        }
        if (replaying)
        {
            if (Time.time - replayStartTime < gameDuration && logger.inputQueue.Count > 0)
            {
                PlayerInput temp = logger.inputQueue.Dequeue();
                PlayerController.inst.input = temp;
                CameraManager.inst.cameraInput = temp.cameraInput;
            }
            else
            {
                replaying = false;
            }
        }
    }
}
