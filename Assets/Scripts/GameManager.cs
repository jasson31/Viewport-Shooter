using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    public GameObject PlayUI;
    public GameObject ResultUI;

    public bool isGameOver = false;

    public void GameOver()
    {
        isGameOver = true;
        ResultUI.SetActive(true);
        ReplayController.inst.Begin(Time.time);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha0) && !isGameOver)
        {
            GameOver();
        }
    }
}
