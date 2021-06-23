using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class ScoreTracker : MonoBehaviour
{

    int currentScore;
	public Text scoreText;
    public static UnityEvent<int> OnScore;

    void Start()
    {
        if (OnScore == null) OnScore = new UnityEvent<int>();
        OnScore.AddListener(HandleScore);
		//GameState.OnEnd.AddListener(ResetScore);

    }

    public void HandleScore(int num)
    {
        currentScore += num;
		scoreText.text = currentScore.ToString();
        //Debug.LogWarning("New Score: " + currentScore.ToString());
        
    }

    public void ResetScore()
	{
        //Debug.LogWarning("Resetting");
		currentScore = 0;
		scoreText.text = currentScore.ToString();
	}


    #region Old BowlStuff
    static List<int> scores = new List<int>();
    public static void ReportScore(int newScore)
    {
        scores.Add(newScore);
    }

    public int GetFinalScore()
    {
        int finalScore = 0;
        for(int i = 0; i < scores.Count; i++)
        {
            finalScore += scores[i];
        }
        return finalScore;
    }

    public int[] GetScores()
    {
        int[] scoreArray = new int[scores.Count];
        scores.CopyTo(scoreArray);

        return scoreArray;
    }

    public int GetScore(int which)
    {
        int score = 0;
        if(scores.Count > which)
        {
            score = scores[which];
        }
        return score;
    }
    #endregion
}
