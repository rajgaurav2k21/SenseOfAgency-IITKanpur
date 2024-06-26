using UnityEngine;
using Leap;
using Leap.Unity;

public class MidpointHandProvider : PostProcessProvider
{
    [SerializeField]
    private Transform headTransform;

    [SerializeField]
    private Transform targetBallTransform; // Reference to the target ball transform

    private void Start()
    {
        if (headTransform == null)
        {
            headTransform = Camera.main.transform; // Default to the main camera
        }

        if (targetBallTransform == null)
        {
            Debug.LogError("Target ball transform not set.");
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

            // Calculate the midpoint between the hand and the target ball
            Vector3 targetBallPos = targetBallTransform.position;
            Vector3 handPosition = hand.PalmPosition.ToVector3();
            Vector3 midpoint = (handPosition + targetBallPos) / 2.0f;

            // Set the new hand position to the midpoint
            hand.SetTransform(midpoint, hand.Rotation);
        }
    }
}
