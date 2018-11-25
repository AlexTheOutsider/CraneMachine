using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text scoreNowText;
    public Text scoreHistoryText;

    private GameObject[] crates;
    private int score = 0;
    private int scoreHistory = 0;

    void FixedUpdate()
    {
        crates = GameObject.FindGameObjectsWithTag("Crate");
        int scoreTemp = 0;
        foreach (GameObject crate in crates)
        {
            if (crate.GetComponent<Scorer>().isHooked || !crate.GetComponent<Scorer>().isActivated)
            {
                continue;
            }

            int scoreNew = Mathf.RoundToInt(crate.transform.position.y * 10f);
            if (scoreTemp < scoreNew)
            {
                scoreTemp = scoreNew;
            }
        }
        score = scoreTemp;
        if (score > scoreHistory)
        {
            scoreHistory = score;
        }
        UpdateUI();
    }

/*    public void ScoreUpdate(int scoreObj)
    {
        if (score < scoreObj)
        {
            score = scoreObj;
        }
        UpdateUI();
    }*/

    void UpdateUI()
    {
        scoreNowText.text = score.ToString();
        scoreHistoryText.text = scoreHistory.ToString();
    }
}