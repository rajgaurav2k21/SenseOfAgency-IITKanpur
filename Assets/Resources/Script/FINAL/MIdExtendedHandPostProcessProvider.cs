using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class MIdExtendedHandPostProcessProvider : PostProcessProvider
{
    // Serialized fields to be set in Unity Inspector
    [SerializeField]
    private Transform headTransform;

    [SerializeField]
    private Transform targetBallTransform; // Reference to the target ball transform

    [SerializeField]
    private float projectionFactor = 2.0f; // Multiplier to project hand further away

    [SerializeField]
    private float maxMoveDistance = 0.1f; // Maximum movement per frame towards midpoint

    private void Start()
    {
        if (headTransform == null)
        {
            headTransform = Camera.main.transform; // Default to the main camera
        }
    }

    public override void ProcessFrame(ref Frame inputFrame)
    {
        if (targetBallTransform == null) return;

        // Get the head position
        Vector3 headPos = headTransform.position;

        // Calculate the shoulder basis
        var shoulderBasis = Quaternion.LookRotation(
            Vector3.ProjectOnPlane(headTransform.forward, Vector3.up),
            Vector3.up);

        // Iterate through each hand in the frame
        foreach (var hand in inputFrame.Hands)
        {
            // Calculate the shoulder position
            Vector3 shoulderPos = headPos
                + (shoulderBasis * (new Vector3(0f, -0.13f, -0.1f)
                + Vector3.left * 0.15f * (hand.IsLeft ? 1f : -1f)));

            // Calculate the vector from shoulder to hand
            Vector3 shoulderToHand = hand.PalmPosition - shoulderPos;

            // Multiply the vector to extend the hand position
            Vector3 extendedHandPos = shoulderPos + shoulderToHand * projectionFactor;

            // Calculate the midpoint between hand and target ball
            Vector3 targetBallPos = targetBallTransform.position;
            Vector3 midpoint = (extendedHandPos + targetBallPos) / 2.0f;

            // Calculate the movement vector towards the midpoint
            Vector3 moveVector = midpoint - extendedHandPos;

            // Limit the movement distance towards the midpoint
            if (moveVector.magnitude > maxMoveDistance)
            {
                moveVector = moveVector.normalized * maxMoveDistance;
            }
            else if (moveVector.magnitude < 0.001f) // Adjust this threshold as needed
            {
                // If already close to the midpoint, do not move further
                moveVector = Vector3.zero;
            }

            // Set the new hand position
            hand.SetTransform(extendedHandPos + moveVector, hand.Rotation);
        }
    }
}
