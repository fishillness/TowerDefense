using UnityEngine;
using SpaceShooter;
 
public class Path : MonoBehaviour
{
    [SerializeField] private CircleArea startArea;
    public CircleArea StartArea => startArea;
    [SerializeField] private AIPointPatrol[] points;
    public int Length => points.Length; 
    public AIPointPatrol this[int i] => points[i];
    private static readonly Color GizmoColor = new Color(0, 0, 1, 0.3f);
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = GizmoColor;
        foreach (var point in points)
            Gizmos.DrawSphere(point.transform.position, point.Radius);
    }
}


