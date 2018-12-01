using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Canvas scoreBoard;
    private Text scoreCurrentText;
    private Text scoreHistoryText;

    private int scoreCurrent = 0;
    private int scoreHistory = 0;
    private GameObject[] crates;

    void Start()
    {
        scoreBoard = GameObject.Find("ScoreBoard").GetComponent<Canvas>();
        scoreCurrentText = scoreBoard.transform.Find("ScoreCurrent").GetComponent<Text>();
        scoreHistoryText = scoreBoard.transform.Find("ScoreHistory").GetComponent<Text>();
    }

    void FixedUpdate()
    {
        crates = GameObject.FindGameObjectsWithTag("Crate");
        int scoreTemp = 0;
        foreach (GameObject crate in crates)
        {
            if (crate.GetComponent<Scorer>().isHooked || !crate.GetComponent<Scorer>().isPlaced
                || !(crate.GetComponent<Rigidbody2D>().velocity.magnitude < 0.01f))
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
}