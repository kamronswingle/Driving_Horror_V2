using UnityEngine;

public class RoadSection : MonoBehaviour
{
    [SerializeField] private Transform entryPoint;
    [SerializeField] private Transform exitPoint;

    public Transform EntryPoint => entryPoint;
    public Transform ExitPoint  => exitPoint;

    // Optional gizmos for debugging
    void OnDrawGizmos()
    {
        if (entryPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(entryPoint.position, 0.3f);
            Gizmos.DrawRay(entryPoint.position, entryPoint.forward * 2f);
        }

        if (exitPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(exitPoint.position, 0.3f);
            Gizmos.DrawRay(exitPoint.position, exitPoint.forward * 2f);
        }
    }
}