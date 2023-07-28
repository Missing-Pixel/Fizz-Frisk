using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCR_Score : MonoBehaviour
{
    public int gameScore = 0;
    public TMP_Text scoreText;

    private void Start()
    {
        scoreText.text = "SCORE: " + gameScore.ToString();
    }

    public void ScoreUpdate(int score)
    {
        Debug.Log(gameScore);
        gameScore += score;
        scoreText.text = "SCORE: " + gameScore.ToString();
    }
}
