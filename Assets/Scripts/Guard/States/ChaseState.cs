using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IState
{
    private const float SPEED = 19;

    NavMeshAgent navAgent;
    GuardController guard;

    public ChaseState(GuardController guard)
    {
        this.guard = guard;
        navAgent = guard.NavAgent;
    }

    public void Enter()
    {
        navAgent.speed = SPEED;
        navAgent.isStopped = false;

        SoundController.Instance.PlayAudio(SoundController.AudioKey.Spotted);
    }

    public void Exit()
    {
        navAgent.isStopped = true;
    }

    public void Update()
    {
        navAgent.destination = guard.SeenTransform.position;

        if (Vector3.Distance(navAgent.transform.position, guard.SeenTransform.position) < 3f)
        {
            GameManager.Instance.EndGame();
        }
    }
}
