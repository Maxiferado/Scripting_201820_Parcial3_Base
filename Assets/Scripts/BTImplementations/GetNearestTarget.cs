using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetNearestTarget : Task {

    public override bool Execute()
    {
        GameController gameController = FindObjectOfType<GameController>();

        if (GetComponent<ActorController>().IsTagged)
        {
            int playerIndex = 0;

            float currentDistance = 0;

            float distance = 0;

            for (int i = 0; i < gameController.players.Count; i++)
            {
                if (gameController.players[i] != GetComponent<ActorController>())
                {
                    currentDistance = Vector3.Distance(transform.position, gameController.players[i].transform.position);

                    if (currentDistance < distance)
                    {
                        distance = currentDistance;

                        playerIndex = i;
                    }
                }
            }
        }

        return base.Execute();
    }
}
