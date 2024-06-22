using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class ExtendedHandProvider : PostProcessProvider
{
    // Serialized fields to be set in Unity Inspector
    [SerializeField]
    private Transform headTransform;

    [SerializeField]
    private float projectionFactor = 2.0f; // Multiplier to project hand further away

    private void Start()
    {
        if (headTransform == null)
        {
            headTransform = Camera.main.transform; // Default to the main camera
        }
    }

    public override void ProcessFrame(ref Frame inputFrame)
    {
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

            // Set the new hand position
            hand.SetTransform(extendedHandPos, hand.Rotation);
        }
    }
}
