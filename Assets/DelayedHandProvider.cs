using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class DelayedHandProvider : PostProcessProvider
{
    private class HandData
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    private Dictionary<int, Queue<HandData>> leftHandDataHistory = new Dictionary<int, Queue<HandData>>();
    private Dictionary<int, Queue<HandData>> rightHandDataHistory = new Dictionary<int, Queue<HandData>>();

    [SerializeField]
    private float delayTime = 1.0f; // 1 second delay
    private float frameDelay; // Delay in seconds

    void Start()
    {
        frameDelay = delayTime; // Setting frameDelay directly as we handle it in seconds
    }

    public override void ProcessFrame(ref Frame inputFrame)
    {
        // Store current frame data
        foreach (var hand in inputFrame.Hands)
        {
            HandData currentHandData = new HandData
            {
                position = hand.PalmPosition,
                rotation = hand.Rotation
            };

            // Store data in the appropriate history queue
            if (hand.IsLeft)
            {
                if (!leftHandDataHistory.ContainsKey(hand.Id))
                    leftHandDataHistory[hand.Id] = new Queue<HandData>();

                leftHandDataHistory[hand.Id].Enqueue(currentHandData);

                // Remove old data to maintain the delay
                if (leftHandDataHistory[hand.Id].Count > Mathf.RoundToInt(frameDelay * 60)) // Assuming 60 FPS
                {
                    leftHandDataHistory[hand.Id].Dequeue();
                }
            }
            else
            {
                if (!rightHandDataHistory.ContainsKey(hand.Id))
                    rightHandDataHistory[hand.Id] = new Queue<HandData>();

                rightHandDataHistory[hand.Id].Enqueue(currentHandData);

                // Remove old data to maintain the delay
                if (rightHandDataHistory[hand.Id].Count > Mathf.RoundToInt(frameDelay * 60)) // Assuming 60 FPS
                {
                    rightHandDataHistory[hand.Id].Dequeue();
                }
            }
        }

        // Apply delayed data to the hands
        foreach (var hand in inputFrame.Hands)
        {
            HandData delayedHandData = null;

            if (hand.IsLeft)
            {
                if (leftHandDataHistory.ContainsKey(hand.Id) && leftHandDataHistory[hand.Id].Count > 0)
                    delayedHandData = leftHandDataHistory[hand.Id].Peek();
            }
            else
            {
                if (rightHandDataHistory.ContainsKey(hand.Id) && rightHandDataHistory[hand.Id].Count > 0)
                    delayedHandData = rightHandDataHistory[hand.Id].Peek();
            }

            if (delayedHandData != null)
            {
                hand.SetTransform(delayedHandData.position, delayedHandData.rotation);
            }
        }
    }
}
