﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager>
{
    public GameObject PlayUI;
    public GameObject ResultUI;
    public GameObject countDownUI;

    private float gameStartTime;
    public bool isGameOver = false;

    public IEnumerator CountDown()
    {
        Instantiate(countDownUI, GameObject.Find("Canvas").transform);
        yield return new WaitForSeconds(2.9f);
        gameStartTime = Time.time;
        SceneManager.LoadScene("PlayScene");
    }
    public void Restart()
    {
        SceneManager.LoadScene("StartScene");
        Time.timeScale = 1;
    }
    public void PlayGame()
    {
        StartCoroutine(CountDown());
    }
    public void GameOver()
    {
        isGameOver = true;
        ResultUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        ReplayController.inst.Begin(Time.time - gameStartTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }
}
