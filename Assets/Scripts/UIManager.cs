using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text timer;
    [SerializeField]
    private Text winner;
    [SerializeField]
    private GameObject gameOver;

    private GameController gameController;

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();

        gameController.onGameEnd += SetWinnerText;
        gameController.onGameEnd += SetGameOver;
    }

    private void Start()
    {
        if (timer == null)
            enabled = false;
    }

    private void Update()
    {
        timer.text = TimerFormat(gameController.currentTime);
    }


    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    private void SetWinnerText()
    {
        winner.text = gameController.GetWinner();
    }

    private void SetGameOver()
    {
        gameOver.SetActive(true);
    }

    private string TimerFormat(float elapsed)
    {
        int d = (int)(elapsed * 100.0f);

        int minutes = d / (60 * 100);
        minutes = (minutes < 0) ? 0 : minutes;

        int seconds = (d % (60 * 100)) / 100;
        seconds = (seconds < 0) ? 0 : seconds;

        int hundredths = d % 100;
        hundredths = (hundredths < 0) ? 0 : hundredths;

        return String.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, hundredths);
    }
}