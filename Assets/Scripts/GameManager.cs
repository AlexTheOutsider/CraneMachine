using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public int timerPreset = 180;

    private Canvas uiCanvas;
    private Text scoreCurrentText;

    private Text scoreCurrentText2;

    //private Text scoreHistoryText;
    private Text timerText;
    private GameObject startPanel;
    private GameObject gameOverPanel;

    private int timerCount = 0;
    private int scoreCurrent = 0;

    private int scoreCurrent2 = 0;
    //private int scoreHistory = 0;

    private GameObject[] crates;
    private bool isStarted = false;
    private bool isOver = false;

    private GameObject crane;
    private GameObject crane2;
    private BlockSpawner blockSpawner;

    private Vector3 baseInitialPos;
    private Vector3 boneInitialPos;
    private Quaternion boneInitialRos;

    private Vector3 baseInitialPos2;
    private Vector3 boneInitialPos2;
    private Quaternion boneInitialRos2;

    private void Start()
    {
        uiCanvas = GameObject.Find("MainUI").GetComponent<Canvas>();
        scoreCurrentText = uiCanvas.transform.Find("ScoreCurrent").GetComponent<Text>();
        scoreCurrentText2 = uiCanvas.transform.Find("ScoreCurrent2").GetComponent<Text>();
        //scoreHistoryText = uiCanvas.transform.Find("ScoreHistory").GetComponent<Text>();
        timerText = uiCanvas.transform.Find("Timer").GetComponent<Text>();
        startPanel = uiCanvas.transform.Find("Welcome").gameObject;
        gameOverPanel = uiCanvas.transform.Find("GameOver").gameObject;

        crane = GameObject.Find("Crane");
        crane2 = GameObject.Find("Crane1");
        blockSpawner = GameObject.Find("PlatformTrigger").GetComponent<BlockSpawner>();

        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);

        baseInitialPos = crane.transform.Find("Base").position;
        boneInitialPos = crane.transform.Find("Bone1").position;
        boneInitialRos = crane.transform.Find("Bone1").rotation;

        baseInitialPos2 = crane2.transform.Find("Base").position;
        boneInitialPos2 = crane2.transform.Find("Bone1").position;
        boneInitialRos2 = crane2.transform.Find("Bone1").rotation;
    }

    private void Update()
    {
        if (Input.anyKey && !isStarted)
        {
            startPanel.SetActive(false);
            StartCoroutine(Timer());
            isStarted = true;
        }
    }

    private void FixedUpdate()
    {
        UpdateScore("Crate");
        UpdateScore("Crate2");
    }

    private void UpdateScore(String tag)
    {
        if (!isStarted || isOver)
            return;

        crates = GameObject.FindGameObjectsWithTag(tag);
        int scoreTemp = 0;
        foreach (GameObject crate in crates)
        {
            if (crate.GetComponent<Scorer>().isHooked || !crate.GetComponent<Scorer>().isPlaced
                                                      || !(crate.GetComponent<Rigidbody2D>().velocity.magnitude <
                                                           0.1f))
                continue;
            int scoreNew = Mathf.RoundToInt((crate.transform.position.y + 3.5f) * 10f);
            scoreTemp = (scoreTemp < scoreNew) ? scoreNew : scoreTemp;
        }

        if (tag.Equals("Crate"))
        {
            scoreCurrent = scoreTemp;
            //scoreHistory = (scoreCurrent > scoreHistory) ? scoreCurrent : scoreHistory;
            scoreCurrentText.text = scoreCurrent.ToString();
        }
        else if (tag.Equals("Crate2"))
        {
            scoreCurrent2 = scoreTemp;
            //scoreHistory = (scoreCurrent2 > scoreHistory) ? scoreCurrent2 : scoreHistory;
            scoreCurrentText2.text = scoreCurrent2.ToString();
        }

        //scoreHistoryText.text = scoreHistory.ToString();
    }

    IEnumerator Timer()
    {
        for (timerCount = timerPreset; timerCount > 0; timerCount--)
        {
            timerText.text = timerCount + "s";
            yield return new WaitForSeconds(1f);
        }

        GameOver();
    }

    public void TimerPunishment()
    {
        if (timerCount > 5)
        {
            timerCount -= 5;
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isOver = true;
        StopAllCoroutines();
        int winnerScore = (scoreCurrent > scoreCurrent2) ? scoreCurrent : scoreCurrent2;
        gameOverPanel.transform.Find("Score").GetComponent<Text>().text = winnerScore.ToString();
        gameOverPanel.transform.Find("ScoreHistory").GetComponent<Text>().text = PlayerPrefs.GetInt("Score").ToString();
        gameOverPanel.SetActive(true);

        if (winnerScore > PlayerPrefs.GetInt("Score"))
        {
            PlayerPrefs.SetInt("Score", winnerScore);
            gameOverPanel.transform.Find("NewRecord").gameObject.SetActive(true);
        }
        else
        {
            gameOverPanel.transform.Find("NewRecord").gameObject.SetActive(false);
        }

        if (scoreCurrent > scoreCurrent2)
        {
            gameOverPanel.transform.Find("Text").GetComponent<Text>().text = "PLAYER 1 WINS!\nTHE SCORE IS";
        }
        else if (scoreCurrent < scoreCurrent2)
        {
            gameOverPanel.transform.Find("Text").GetComponent<Text>().text = "PLAYER 2 WINS!\nTHE SCORE IS";
        }
        else
        {
            gameOverPanel.transform.Find("Text").GetComponent<Text>().text = "DRAW!\nTHE SCORE IS";
        }

        crane.GetComponent<CraneController>().enabled = false;
        crane.GetComponent<CraneMovement>().enabled = false;

        crane2.GetComponent<CraneController>().enabled = false;
        crane2.GetComponent<CraneMovement>().enabled = false;
    }

    public void Replay()
    {
        crane.transform.Find("Base").position = baseInitialPos;
        crane.transform.Find("Bone1").position = boneInitialPos;
        crane.transform.Find("Bone1").rotation = boneInitialRos;
        crane.GetComponent<CraneController>().enabled = true;
        crane.GetComponent<CraneMovement>().enabled = true;

        crane2.transform.Find("Base").position = baseInitialPos2;
        crane2.transform.Find("Bone1").position = boneInitialPos2;
        crane2.transform.Find("Bone1").rotation = boneInitialRos2;
        crane2.GetComponent<CraneController>().enabled = true;
        crane2.GetComponent<CraneMovement>().enabled = true;

        blockSpawner.blockOnPlatform.Clear();
        foreach (Transform child in blockSpawner.spawnPointLeft.transform)
        {
            Destroy(child.gameObject);
        }
        Instantiate(blockSpawner.blockLibrary[Random.Range(0, blockSpawner.blockLibrary.Length)],
            blockSpawner.spawnPointLeft);
        
        foreach (Transform child in blockSpawner.spawnPointRight.transform)
        {
            Destroy(child.gameObject);
        }
        Instantiate(blockSpawner.blockLibrary2[Random.Range(0, blockSpawner.blockLibrary2.Length)],
            blockSpawner.spawnPointRight);
        
        StartCoroutine(Timer());
        isOver = false;
        gameOverPanel.SetActive(false);
    }
}