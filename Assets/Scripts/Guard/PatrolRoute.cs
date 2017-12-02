using System.Collections.Generic;
using UnityEngine;

public class PatrolRoute : MonoBehaviour
{
    private List<PatrolPoint> route = new List<PatrolPoint>();

    private static List<PatrolRoute> routes = new List<PatrolRoute>();

    public static PatrolRoute GetRoute()
    {
        return routes[0];
    }

    private void Start()
    {
        routes.Add(this);
    }

}
