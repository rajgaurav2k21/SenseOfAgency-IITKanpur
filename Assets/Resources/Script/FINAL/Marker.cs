using UnityEngine;

public class Marker : MonoBehaviour
{
    public Transform handTransform;
    public Transform targetBallTransform;
    public Transform markerTransform; 

    void Update()
    {
        Vector3 handPosition = handTransform.position;
        Vector3 targetPosition = targetBallTransform.position;
        Vector3 midpointPosition = Vector3.Lerp(handPosition, targetPosition, 0.5f);
        markerTransform.position = midpointPosition;
        Vector3 direction = targetPosition - handPosition;
        markerTransform.rotation = Quaternion.LookRotation(direction);
        markerTransform.Rotate(0, 90, 0);
    }
}
