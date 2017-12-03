using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class PatrolRoute : MonoBehaviour
{
    enum RouteType
    {
        Circular,
        Line,
        Single
    }

    [SerializeField]
    private RouteType type;

    private bool forward = true;
    private int guardsOnPatrol = 0;

    [SerializeField]
    private Color groupColor = Color.yellow;

    [SerializeField]
    private List<PatrolPoint> route = new List<PatrolPoint>();

    private static List<PatrolRoute> routes = new List<PatrolRoute>();
    private static int totalGuardsOnPatrol = 0;

    public static PatrolRoute GetNewRoute(PatrolRoute previousRoute = null)
    {
        if(previousRoute != null)
        {
            previousRoute.DismissGuard();
        }

        PatrolRoute avilableRoute = routes.RandomWeighted(x => totalGuardsOnPatrol - x.guardsOnPatrol);
        avilableRoute.RegisterGuard();
        Debug.Log(avilableRoute);
        return avilableRoute;
    }

    

    public PatrolPoint GetNextPoint(PatrolPoint point)
    {
        if (route.Count == 0)
        {
            return null;
        }

        if(point == null)
        {
            return route.First();
        }

        switch (type)
        {
            case RouteType.Circular:
                return route.NextOrFirst(point);
            case RouteType.Line:
                int index = route.IndexOf(point);

                if (forward)
                {
                    if ((index + 1) < route.Count)
                    {
                        index++;
                    }
                    else
                    {
                        forward = false;
                        index--;
                    }
                }
                else
                {
                    if (index > 0)
                    {
                        index--;
                    }
                    else
                    {
                        forward = true;
                        index++;
                    }
                }

                return route[index];
            case RouteType.Single:
                int singleIndex = route.IndexOf(point);
                if ((singleIndex + 1) < route.Count)
                {
                    return route[singleIndex + 1];
                }
                else
                {
                    return null;
                }
        }

        return null;
    }

    private void Awake()
    {
        RegisterPoints();
    }

    private void Start()
    {
        if (Application.isPlaying)
        {
            routes.Add(this);
        }
    }

    public void RegisterGuard()
    {
        totalGuardsOnPatrol++;
        guardsOnPatrol++;
    }

    public void DismissGuard()
    {
        totalGuardsOnPatrol--;
        guardsOnPatrol--;
    }

    public void RegisterPoints()
    {
        PatrolPoint[] points = transform.GetComponentsInChildren<PatrolPoint>();
        route.Clear();
        route.AddRange(points);
    }

    public void AddPoint()
    {
        GameObject pointGameObject = new GameObject();
        pointGameObject.transform.SetParent(transform, false);

        PatrolPoint newPoint = pointGameObject.AddComponent<PatrolPoint>();
        newPoint.SetColor(groupColor);
        route.Add(newPoint);

        pointGameObject.name = "Point " + route.Count;
    }

    public void RemovePoint()
    {
        if(route.Count >= 1)
        {
            GameObject pointGameObject = route[route.Count - 1].gameObject;
            route.RemoveAt(route.Count - 1);
            DestroyImmediate(pointGameObject);
        }
    }

}
