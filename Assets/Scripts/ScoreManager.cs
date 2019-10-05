﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class ScoreManager : MonoBehaviour
{
    public RectTransform graphRect;
    public UILineRenderer scoreLine;
    public RectTransform[] goalLine;
    public RectTransform bestLine;
    public Color enabledStar;
    public Color disabledStar;

    public int ceilingScore;

    public CameraManager cameraManager;
    public GameObject player;
    public GameObject playerHead;
    public int score;
    int bestScore;

    public int initScore = 500;
    public int decBySec = 10;
    public int decBySecEnd = 5;
    public int decBySecEepty = 50;
    public int bodyScore = 100;
    public int playerScore = 100;
    public int faceScore = 300;

    public int[] goalScore;

    float timer = 0f;
    float playTime = 0f;
    public float missionTime = 60f;

    List<int> scoreHistory;

    private void Start()
    {
        scoreHistory = new List<int>();
        ScoreInit();
    }
    void Update()
    {
        playTime += Time.deltaTime;
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            timer -= 1f;
            int decrease = 0;
            if (playTime >= missionTime) decBySec += decBySecEnd;
            decrease += decBySec;
            if (cameraManager.currentCamera != 0 && !cameraManager.cameraObjects[cameraManager.currentCamera].IsObjectVisible(player))
            {
                decrease += decBySecEepty;
                Debug.Log("Player Off");
            }
            if (decrease >= 100) ScoreNotice(-decrease);
            score = Mathf.Max(0, score - decrease);
            scoreHistory.Add(score);
            while (scoreHistory.Count > 31) scoreHistory.RemoveAt(0);
            bestScore = Mathf.Max(bestScore, score);
            SetGraph();
        }
    }

    public void ScoreInit()
    {
        score = initScore;
        playTime = 0f;
        scoreHistory = new List<int>();
        for (int i = 0; i <= 30; i++) scoreHistory.Add(score);
        for(int i = 0; i<3; i++)
        {
            goalLine[i].anchoredPosition = new Vector2(0, GetYByScore(goalScore[i]));
        }
        bestScore = score;
        SetGraph();
    }

    public void SetGraph()
    {
        float width = graphRect.rect.width;
        List<Vector2> list = new List<Vector2>();

        for(int i = 0; i <= 30; i++)
        {
            list.Add(new Vector2(width / 30 * i, GetYByScore(scoreHistory[i])));
        }
        for (int i = 0; i < 3; i++)
        {
            goalLine[i].GetComponent<UILineRenderer>().color = (goalScore[i] <= bestScore) ? disabledStar : enabledStar;
        }
        bestLine.anchoredPosition = new Vector2(0, GetYByScore(bestScore));
        scoreLine.Points = list.ToArray();
    }

    float GetYByScore(int score)
    {
        return graphRect.rect.height * score / ceilingScore;
    }

    public void KillScore(GameObject enemy)
    {
        CameraObject currentCamera = cameraManager.cameraObjects[cameraManager.currentCamera];
        int increase = 0;
        if (currentCamera.IsObjectVisible(enemy))
        {
            Debug.Log("Enemy On");
            increase += bodyScore;
        }
        if (currentCamera.IsObjectVisible(player))
        {
            Debug.Log("Player On");
            increase += playerScore;
        }
        if (currentCamera.IsObjectVisible(playerHead))
        {
            Debug.Log("Head On");
            increase += faceScore;
        }

        if (increase > 0) ScoreNotice(increase);
        score += increase;
    }

    public void ScoreNotice(int scoreDelta)
    {
        Debug.Log("Notice : " + scoreDelta);
    }
}
