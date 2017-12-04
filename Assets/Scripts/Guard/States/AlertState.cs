using UnityEngine;
using UnityEngine.AI;

public class AlertState : IState
{
    private const float SPEED = 8;

    NavMeshAgent navAgent;
    GuardController guard;

    public AlertState(GuardController guard)
    {
        this.guard = guard;
        navAgent = guard.NavAgent;
    }

    public void Enter()
    {
        navAgent.speed = SPEED;
        navAgent.isStopped = false;
    }

    public void Exit()
    {
        navAgent.isStopped = true;
    }

    public void Update()
    {
        navAgent.destination = guard.SeenTransform.position;
    }
}
