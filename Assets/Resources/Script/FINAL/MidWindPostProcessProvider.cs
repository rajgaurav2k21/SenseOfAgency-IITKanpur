using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class MidWindPostProcessProvider : PostProcessProvider
{
    [SerializeField]
    private Transform headTransform;

    [SerializeField]
    private float projectionScale = 10f;

    [SerializeField]
    private float handMergeDistance = 0.35f;

    [SerializeField]
    private float windNoiseIntensity = 0.05f;

    [SerializeField]
    private float windNoiseFrequency = 2f;

    [SerializeField]
    private Transform targetBallTransform; // Reference to the target ball transform

    [SerializeField]
    private float maxMoveDistance = 0.1f; // Maximum movement per frame

    public override void ProcessFrame(ref Frame inputFrame)
    {
        if (headTransform == null)
        {
            headTransform = Camera.main.transform;
        }

        if (targetBallTransform == null)
        {
            Debug.LogError("Target ball transform not set.");
            return;
        }

        Vector3 headPos = headTransform.position;
        var shoulderBasis = Quaternion.LookRotation(
            Vector3.ProjectOnPlane(headTransform.forward, Vector3.up),
            Vector3.up);

        foreach (var hand in inputFrame.Hands)
        {
            Vector3 shoulderPos = headPos
                + (shoulderBasis * (new Vector3(0f, -0.13f, -0.1f)
                + Vector3.left * 0.15f * (hand.IsLeft ? 1f : -1f)));

            // Calculate midpoint between hand and target ball
            Vector3 targetBallPos = targetBallTransform.position;
            Vector3 handPosition = hand.PalmPosition;
            Vector3 midpoint = (handPosition + targetBallPos) / 2.0f;

            // Calculate the movement vector towards midpoint
            Vector3 moveVector = midpoint - handPosition;

            // Limit the movement distance
            if (moveVector.magnitude > maxMoveDistance)
            {
                moveVector = moveVector.normalized * maxMoveDistance;
            }

            // Adjust hand position towards the midpoint
            handPosition += moveVector;

            Vector3 shoulderToHand = handPosition - shoulderPos;
            float handShoulderDist = shoulderToHand.magnitude;
            float projectionDistance = Mathf.Max(0f, handShoulderDist - handMergeDistance);
            float projectionAmount = 1f + (projectionDistance * projectionScale);

            Vector3 projectedHandPos = shoulderPos + shoulderToHand * projectionAmount;

            // Apply wind noise to the projected hand position
            Vector3 windAffectedHandPos = GenerateWindNoise(projectedHandPos);

            hand.SetTransform(windAffectedHandPos, hand.Rotation);
        }
    }

    private Vector3 GenerateWindNoise(Vector3 originalPosition)
    {
        float noiseX = Mathf.PerlinNoise(Time.time * windNoiseFrequency, 0f) - 0.5f;
        float noiseY = Mathf.PerlinNoise(Time.time * windNoiseFrequency, 1f) - 0.5f;
        float noiseZ = Mathf.PerlinNoise(Time.time * windNoiseFrequency, 2f) - 0.5f;

        Vector3 windEffect = new Vector3(noiseX, noiseY, noiseZ) * windNoiseIntensity;
        return originalPosition + windEffect;
    }
}
