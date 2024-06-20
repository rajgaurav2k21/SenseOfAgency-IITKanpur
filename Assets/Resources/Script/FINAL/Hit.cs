using UnityEngine;

public class Hit : MonoBehaviour
{
    public GameObject vfxPrefab;

    private void OnTriggerEnter(Collider other)
    {
        Vector3 closestPoint = other.ClosestPointOnBounds(transform.position);
        Instantiate(vfxPrefab, closestPoint, Quaternion.identity);
    }
}
