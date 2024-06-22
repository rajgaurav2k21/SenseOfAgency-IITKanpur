using UnityEngine;
using Leap;
using Leap.Unity;

public class DraggedHandProvider : PostProcessProvider
{
    [SerializeField]
    private Transform headTransform;

    [SerializeField]
    [Range(0.1f, 10.0f)] // Adjust range as needed
    private float dragStrength = 0.5f;

    public override void ProcessFrame(ref Frame inputFrame)
    {
        if (headTransform == null)
        {
            headTransform = Camera.main.transform;
        }

        Vector3 headPos = headTransform.position;

        foreach (var hand in inputFrame.Hands)
        {
            Vector3 targetPosition = Vector3.Lerp(hand.PalmPosition, hand.PalmPosition + hand.PalmVelocity * Time.deltaTime, dragStrength);

            // Apply the drag effect
            hand.SetTransform(targetPosition, hand.Rotation);
        }
    }
}
