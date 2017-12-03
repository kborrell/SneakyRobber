using UnityEngine.AI;

public class PatrolState : IState
{
    private const float SPEED = 6;

    NavMeshAgent navAgent;
    PatrolRoute route;
    PatrolPoint point;

    public PatrolState(GuardController guard)
    {
        navAgent = guard.NavAgent;
    }

    public void Enter()
    {
        PatrolRoute newRoute = PatrolRoute.GetNewRoute();
        navAgent.isStopped = false;
        navAgent.speed = SPEED;

        if(newRoute != route)
        {
            route = newRoute;
            point = null;
        }

        GotoPoint(point);
    }

    public void Exit()
    {
        if(route != null)
        {
            route.DismissGuard();
        }

        navAgent.isStopped = true;
    }

    public void Update()
    {
        // Navigate
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
        {
                GotoPoint(route.GetNextPoint(point));
        }

        // Detect

    }

    private void GotoPoint(PatrolPoint targetPoint)
    {
        if (targetPoint != null)
        {
            navAgent.destination = targetPoint.transform.position;
            point = targetPoint;
        }
        else
        {
            route = PatrolRoute.GetNewRoute(route);
        }
    }
}
