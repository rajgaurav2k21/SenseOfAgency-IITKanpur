using UnityEngine;
using Leap;
using Leap.Unity;

public class DynamicExtendedHandProvider : PostProcessProvider
{
    [SerializeField]
    private Transform headTransform;

    [SerializeField]
    private float projectionFactor = 2.0f; // Multiplier to project hand further away

    [SerializeField]
    private float animationDuration = 2.0f; // Duration of the animation from normal to extended and back

    private float animationTime = 0f; // Current animation time

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

            // Determine the target position based on animationTime
            float t = Mathf.Sin(animationTime / animationDuration * Mathf.PI * 2); // Sine wave between -1 and 1
            float currentProjectionFactor = Mathf.Lerp(1.0f, projectionFactor, (t + 1f) / 2f); // Normalize t to [0, 1]

            // Multiply the vector to extend the hand position
            Vector3 extendedHandPos = shoulderPos + shoulderToHand * currentProjectionFactor;

            // Set the new hand position
            hand.SetTransform(extendedHandPos, hand.Rotation);
        }

        // Update animation time based on delta time
        animationTime += Time.deltaTime;

        // Ensure animationTime stays within [0, animationDuration]
        if (animationTime > animationDuration)
        {
            animationTime -= animationDuration; // Loop animation
        }
    }
}
