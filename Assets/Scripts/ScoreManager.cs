using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class ScoreManager : MonoBehaviour
{
    public RectTransform graphRect;
    public UILineRenderer scoreLine;
    public RectTransform[] goalLine;
    public RectTransform bestLine;
    public Color enabledStar;
    public Color disabledStar;
    public Image[] starImage;
    public GameObject starParticle;

    public Sprite[] starSprite;
    public AudioClip starSound;
    public AudioSource playerAS;

    public int ceilingScore;

    public CameraManager cameraManager;
    public PlayerController player;
    public int score;
    int bestScore;

    public Text timeLimitText;
    public Text timeCounterText;
    public Color befTimeColor;
    public Color aftTimeColor;

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

    public GameObject noticePrefab;
    public RectTransform noticeField;
    public Color posNoticeColor;
    public Color negNoticeColor;

    List<int> scoreHistory;

    private void Start()
    {
        scoreHistory = new List<int>();
        ScoreInit();
        timeLimitText.text = "제한시간\n" + StringByTime(missionTime);
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
            if (cameraManager.currentCamera != 0 && !cameraManager.cameraObjects[cameraManager.currentCamera].IsVisible(player.bodyMesh, player.gameObject, 0.7f))
            {
                decrease += decBySecEepty;
                //Debug.Log("Player Off");
            }
            if (decrease >= 50) ScoreNotice(-decrease);
            score = Mathf.Max(0, score - decrease);
            scoreHistory.Add(score);
            while (scoreHistory.Count > 31) scoreHistory.RemoveAt(0);
            bestScore = Mathf.Max(bestScore, score);
            SetGraph();
        }
        timeCounterText.text = StringByTime(playTime);
        timeCounterText.color = (playTime < missionTime) ? befTimeColor : aftTimeColor;
    }

    public void ScoreInit()
    {
        score = initScore;
        playTime = 0f;
        scoreHistory = new List<int>();
        for (int i = 0; i <= 30; i++) scoreHistory.Add(score);
        for(int i = 0; i<3; i++)
        {
            starImage[i].sprite = starSprite[0];
            goalLine[i].GetComponentInChildren<Text>().text = goalScore[i].ToString();
        }
        bestScore = score;
        SetGraph();
    }

    public void SetGraph()
    {
        float width = graphRect.rect.width;
        List<Vector2> list = new List<Vector2>();

        ceilingScore = bestScore + 1000;
        for(int i = 0; i <= 30; i++)
        {
            list.Add(new Vector2(width / 30 * i, GetYByScore(scoreHistory[i])));
        }
        for (int i = 0; i < 3; i++)
        {
            goalLine[i].gameObject.SetActive(goalScore[i] <= ceilingScore);
            goalLine[i].anchoredPosition = new Vector2(0, GetYByScore(goalScore[i]));
            goalLine[i].GetComponent<UILineRenderer>().color = (goalScore[i] <= bestScore) ? disabledStar : enabledStar;
            goalLine[i].GetComponentInChildren<Text>().color = (goalScore[i] <= bestScore) ? disabledStar : enabledStar;
            if(starImage[i].sprite == starSprite[0] && goalScore[i] <= bestScore)
            {
                starImage[i].sprite = starSprite[1];
                Instantiate(starParticle, starImage[i].transform.position, Quaternion.identity, starImage[i].transform);
                playerAS.PlayOneShot(starSound);
            }
        }
        bestLine.anchoredPosition = new Vector2(0, GetYByScore(bestScore));
        bestLine.GetComponentInChildren<Text>().text = bestScore.ToString();
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
        if (currentCamera.IsVisible(enemy.GetComponent<Enemy>().mesh, enemy, 1.5f))
        {
            increase += bodyScore;
        }
        if (currentCamera.IsVisible(player.bodyMesh, player.gameObject, 1.0f))
        {
            increase += playerScore;
        }
        if (currentCamera.IsFaceVisible())
        {
            increase += faceScore;
        }

        if (increase > 0) ScoreNotice(increase);
        score += increase;
    }

    public void ScoreNotice(int scoreDelta)
    {
        GameObject obj = Instantiate(noticePrefab, noticeField);
        Text text = obj.GetComponent<Text>();

        obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(0, noticeField.rect.width), Random.Range(0, noticeField.rect.height));

        if (scoreDelta > 0)
        {
            text.text = "흥미진진\n+" + scoreDelta;
            text.color = posNoticeColor;
        }
        else
        {
            text.text = "지루함\n" + scoreDelta;
            text.color = negNoticeColor;
        }
    }

    public string StringByTime(float time)
    {
        int _time = Mathf.FloorToInt(time);
        return (_time / 60) + ":" + string.Format("{0:D2}", (_time % 60)); 
    }
}
