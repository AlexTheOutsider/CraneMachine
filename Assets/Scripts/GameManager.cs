using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int timerPreset = 60;

    private Canvas uiCanvas;
    private Text scoreCurrentText;
    private Text scoreHistoryText;
    private Text timerText;

    private int scoreCurrent = 0;
    private int scoreHistory = 0;
    private GameObject[] crates;

    void Start()
    {
        uiCanvas = GameObject.Find("MainUI").GetComponent<Canvas>();
        scoreCurrentText = uiCanvas.transform.Find("ScoreCurrent").GetComponent<Text>();
        scoreHistoryText = uiCanvas.transform.Find("ScoreHistory").GetComponent<Text>();
        timerText = uiCanvas.transform.Find("Timer").GetComponent<Text>();

        StartCoroutine(Timer());
    }

    void FixedUpdate()
    {
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

    void UpdateScore()
    {
        scoreCurrentText.text = scoreCurrent.ToString();
        scoreHistoryText.text = scoreHistory.ToString();
    }

    IEnumerator Timer()
    {
        for (int i = timerPreset; i > 0; i--)
        {
            timerText.text = i + "s";
            yield return new WaitForSeconds(1f);
        }
    }
}