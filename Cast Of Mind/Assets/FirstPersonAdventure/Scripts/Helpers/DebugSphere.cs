using UnityEngine;

public class DebugSphere : MonoBehaviour
{
    public Color color;
    public float radius = 1;
    public float yOffset = 0;
    public bool showDirection = true;

    void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z), radius);
        if (showDirection)
        {
            Gizmos.DrawLine(transform.position, transform.position + transform.forward);
        }
    }
}
