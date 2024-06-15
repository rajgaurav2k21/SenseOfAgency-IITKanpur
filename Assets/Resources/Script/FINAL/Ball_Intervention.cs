using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Intervention : MonoBehaviour
{
    public Transform handTransform;
    public Transform targetBallTransform;
    public Transform balltransform; 
    void Update()
    {
        Vector3 handPosition = handTransform.position;
        Vector3 targetPosition = targetBallTransform.position;
        Vector3 midpointPosition = Vector3.Lerp(handPosition, targetPosition, 0.5f);
        balltransform.position = midpointPosition;
    }
}