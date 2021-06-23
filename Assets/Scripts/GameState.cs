using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameState : MonoBehaviour
{
    public GameObject rootMenu, gameUI, gameOver;
    public Controller controller;
    public ScoreTracker scoreTrack;
    public static UnityEvent OnEnd;

	private void Start()
    {
        if (OnEnd == null) OnEnd = new UnityEvent();
        OnEnd.AddListener(EndGame);

        rootMenu.SetActive(true);
        gameUI.SetActive(false);
        gameOver.SetActive(false);
    }

    public void StartGame()
    {
        rootMenu.SetActive(false);
        gameUI.SetActive(true);
        gameOver.SetActive(false);
        controller.StartGame();
        scoreTrack.ResetScore();
    }

    public void EndGame()
    {
        rootMenu.SetActive(false);
        gameUI.SetActive(false);
        gameOver.SetActive(true);
        controller.EndGame();
    }
}
