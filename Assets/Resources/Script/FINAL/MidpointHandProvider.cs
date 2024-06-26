using UnityEngine;
using Leap;
using Leap.Unity;

public class MidpointHandProvider : PostProcessProvider
{
    [SerializeField]
    private Transform headTransform;

    [SerializeField]
    private Transform targetBallTransform; // Reference to the target ball transform

    [SerializeField]
    private float maxMoveDistance = 0.1f; // Maximum movement per frame

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

        Vector3 targetBallPos = targetBallTransform.position;

        foreach (var hand in inputFrame.Hands)
        {
            Vector3 handPosition = hand.PalmPosition;
            Vector3 midpoint = (handPosition + targetBallPos) / 2.0f;

            // Calculate the movement vector
            Vector3 moveVector = midpoint - handPosition;

            // Limit the movement distance
            if (moveVector.magnitude > maxMoveDistance)
            {
                moveVector = moveVector.normalized * maxMoveDistance;
            }

            // Set the new hand position
            hand.SetTransform(handPosition + moveVector, hand.Rotation);
        }
    }
}
