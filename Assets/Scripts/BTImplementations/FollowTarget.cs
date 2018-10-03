using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Task that instructs ControlledAI to follow its designated 'target'
/// </summary>
public class FollowTarget : Task
{
    public override bool Execute()
    {
        GameController gameController = FindObjectOfType<GameController>();

        if (GetComponent<ActorController>().IsTagged)
        {
            int index = 0;

            do
            {
                index = Random.Range(0, gameController.players.Count);

            } while (gameController.players[index] != GetComponent<ActorController>() && gameController.players[index] != gameController.lastPlayerTouched);

            GetComponent<NavMeshAgent>().SetDestination(gameController.players[index].transform.position);
        }

        return base.Execute();
    }
}