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

    }

    public void HandleScore(int num)
    {
        currentScore += num;
		scoreText.text = currentScore.ToString();
        
    }

    public void ResetScore()
	{
		currentScore = 0;
		scoreText.text = currentScore.ToString();
	}
}
