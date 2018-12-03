using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int timerPreset = 180;

    private Canvas uiCanvas;
    private Text scoreCurrentText;
    private Text scoreHistoryText;
    private GameObject gameOverPanel;
    private GameObject startPanel;
    private Text timerText;
    private int timerCount = 0;

    private int scoreCurrent = 0;
    private int scoreHistory = 0;
    private GameObject[] crates;

    private bool isStarted = false;
    private bool isOver = false;
    private GameObject crane;
    private BlockSpawner blockSpawner;
    private Transform spawnPoint;

    private Vector3 boneInitialPos;
    private Quaternion boneInitialRos;

    private void Start()
    {
        uiCanvas = GameObject.Find("MainUI").GetComponent<Canvas>();
        scoreCurrentText = uiCanvas.transform.Find("ScoreCurrent").GetComponent<Text>();
        scoreHistoryText = uiCanvas.transform.Find("ScoreHistory").GetComponent<Text>();
        gameOverPanel = uiCanvas.transform.Find("GameOver").gameObject;
        startPanel = uiCanvas.transform.Find("Welcome").gameObject;
        timerText = uiCanvas.transform.Find("Timer").GetComponent<Text>();
        crane = GameObject.Find("Crane");
        blockSpawner = GameObject.Find("PlatformTrigger").GetComponent<BlockSpawner>();
        spawnPoint = GameObject.Find("SpawnPoints").transform.GetChild(0);

        gameOverPanel.SetActive(false);
        startPanel.SetActive(true);

        boneInitialPos = crane.transform.Find("Bone1").position;
        boneInitialRos = crane.transform.Find("Bone1").rotation;
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
        if (!isStarted || isOver)
            return;

        crates = GameObject.FindGameObjectsWithTag("Crate");
        int scoreTemp = 0;
        foreach (GameObject crate in crates)
        {
            if (crate.GetComponent<Scorer>().isHooked || !crate.GetComponent<Scorer>().isPlaced
                                                      || !(crate.GetComponent<Rigidbody2D>().velocity.magnitude <
                                                           0.1f))
                continue;
            int scoreNew = Mathf.RoundToInt((crate.transform.position.y + 3f) * 10f);
            scoreTemp = (scoreTemp < scoreNew) ? scoreNew : scoreTemp;
        }

        scoreCurrent = scoreTemp;
        scoreHistory = (scoreCurrent > scoreHistory) ? scoreCurrent : scoreHistory;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreCurrentText.text = scoreCurrent.ToString();
        scoreHistoryText.text = scoreHistory.ToString();
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
        StopAllCoroutines();
        gameOverPanel.transform.Find("Score").GetComponent<Text>().text = scoreCurrent.ToString();
        gameOverPanel.SetActive(true);

        crane.GetComponent<CraneController>().enabled = false;
        crane.GetComponent<CraneMovement>().enabled = false;
    }

    public void Replay()
    {
        crane.transform.Find("Bone1").position = boneInitialPos;
        crane.transform.Find("Bone1").rotation = boneInitialRos;
        crane.GetComponent<CraneController>().enabled = true;
        crane.GetComponent<CraneMovement>().enabled = true;
        gameOverPanel.SetActive(false);
        blockSpawner.blockOnPlatform.Clear();
        foreach (Transform child in spawnPoint.transform)
        {
            Destroy(child.gameObject);
        }
        StartCoroutine(Timer());
        isOver = false;
    }
}