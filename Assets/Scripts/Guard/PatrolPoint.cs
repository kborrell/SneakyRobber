using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    [SerializeField]
    Color groupColor;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = groupColor;
        Gizmos.DrawSphere(transform.position, 1);
    }

    public void SetColor(Color color)
    {
        groupColor = color;
    }
}
