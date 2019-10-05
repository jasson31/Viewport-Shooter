using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public CameraManager cameraManager;
    public GameObject player;
    public GameObject playerHead;
    public int score;

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

    private void Start()
    {
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
            Debug.Log("Score : " + score);
        }
    }

    public void ScoreInit()
    {
        score = initScore;
        playTime = 0f;
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
