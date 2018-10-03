using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public List<ActorController> players = new List<ActorController>();

    [SerializeField]
    private float time = 25F;

    private bool startCountdown = false;

    public float currentTime { get; private set; }

    public delegate void OnGameStart();
    public OnGameStart onGameStart;

    public delegate void OnGameEnd();
    public OnGameEnd onGameEnd;

    [HideInInspector]
    public ActorController lastPlayerTouched;
    [HideInInspector]
    public ActorController currentPlayerTouched;

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    private IEnumerator Start()
    {
        currentTime = time;

        yield return new WaitForSeconds(0.5F);

        CheckPlayers();
    }

    private void Update()
    {
        if (startCountdown)
            currentTime -= Time.deltaTime;

        if (currentTime <= 0F)
        {
            if (onGameEnd != null)
                onGameEnd();

            startCountdown = false;
        }
    }

    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    public void StartGame()
    {
        if (onGameStart != null)
            onGameStart();

        startCountdown = true;
    }

    public void SetCurrentPlayerTouched(ActorController actorController)
    {
        if (currentPlayerTouched != null)
            lastPlayerTouched = currentPlayerTouched;

        currentPlayerTouched = actorController;
    }

    public string GetWinner()
    {
        int currentPlayerWinnerIndex = 0;

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].touchCount < players[currentPlayerWinnerIndex].touchCount)
            {
                currentPlayerWinnerIndex = i;
            }
        }

        return string.Format("{0}: {1}",
            players[currentPlayerWinnerIndex].name,
            players[currentPlayerWinnerIndex].touchCount);
    }

    private void CheckPlayers()
    {
        ActorController[] actors = FindObjectsOfType<ActorController>();

        for (int i = 0; i < actors.Length; i++)
        {
            if (i <= 4)
                players.Add(actors[i]);
            else
                DestroyImmediate(actors[i]);
        }

        // Mark a player as tagged
        players[UnityEngine.Random.Range(0, players.Count)].onActorTagged(true);
    }
}
