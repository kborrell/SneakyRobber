using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    [SerializeField]
    Color groupColor = Color.yellow;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = groupColor;
        Gizmos.DrawSphere(transform.position, 1);
    }
}
