using Leap.Unity;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

namespace Leap.Examples
{
    public class DelayedHandMovement : PostProcessProvider
    {
        [Header("Delay Settings")]
        [Tooltip("Delay in seconds to apply to the hand tracking data.")]
        [Range(0f, 100f)]
        public float delayInSeconds = 2f;

        private Queue<HandData> _leftHandDataBuffer = new Queue<HandData>();
        private Queue<HandData> _rightHandDataBuffer = new Queue<HandData>();

        private struct HandData
        {
            public Pose Pose { get; set; }
            public float Timestamp { get; set; }
        }

        private Timer _leftHandDelayTimer;
        private Timer _rightHandDelayTimer;

        // Reference to the LeftHand and RightHand objects (assuming they exist in the scene)
        public GameObject leftHand;
        public GameObject rightHand;

        public override void ProcessFrame(ref Frame inputFrame)
        {
            var currentTime = Time.time;

            // Process left hand
            var leftHandData = inputFrame.Hands.Find(h => h.IsLeft);
            ProcessHand(leftHandData, ref _leftHandDataBuffer, currentTime);

            // Process right hand
            var rightHandData = inputFrame.Hands.Find(h => !h.IsLeft);
            ProcessHand(rightHandData, ref _rightHandDataBuffer, currentTime);
        }

        private void ProcessHand(Hand hand, ref Queue<HandData> handDataBuffer, float currentTime)
        {
            if (hand == null)
            {
                return;
            }

            var handPose = hand.GetPalmPose();

            // Add new hand data to the buffer with current timestamp
            handDataBuffer.Enqueue(new HandData { Pose = handPose, Timestamp = currentTime });
            Debug.Log("Added hand data for " + (hand.IsLeft ? "Left Hand" : "Right Hand") + " at: " + currentTime);

            // Start timer for delayed pose update (if not already running)
            if (hand.IsLeft && _leftHandDelayTimer == null)
            {
                _leftHandDelayTimer = new Timer(delayInSeconds * 1000); // Convert seconds to milliseconds
                _leftHandDelayTimer.Elapsed += OnLeftHandDelayElapsed;
                _leftHandDelayTimer.AutoReset = false; // Reset timer after firing
                _leftHandDelayTimer.Start();
                Debug.Log("Started timer for Left Hand delay at: " + currentTime);
            }
            else if (!hand.IsLeft && _rightHandDelayTimer == null)
            {
                _rightHandDelayTimer = new Timer(delayInSeconds * 1000); // Convert seconds to milliseconds
                _rightHandDelayTimer.Elapsed += OnRightHandDelayElapsed;
                _rightHandDelayTimer.AutoReset = false; // Reset timer after firing
                _rightHandDelayTimer.Start();
                Debug.Log("Started timer for Right Hand delay at: " + currentTime);
            }
        }

        private HandData? GetDelayedHandData(Queue<HandData> handDataBuffer, float currentTime)
        {
            // Calculate the latest acceptable timestamp (current time minus delay)
            var latestAcceptableTime = currentTime - delayInSeconds;

            if (handDataBuffer.Count > 0)
            {
                // Get the oldest data point
                var oldestData = handDataBuffer.Peek();

                // Check if the oldest data is within the delay window
                if (oldestData.Timestamp <= latestAcceptableTime)
                {
                    // Remove data older than the latest acceptable time
                    while (handDataBuffer.Count > 0 && handDataBuffer.Peek().Timestamp < latestAcceptableTime)
                    {
                        handDataBuffer.Dequeue();
                    }
                    return handDataBuffer.Count > 0 ? (HandData?)handDataBuffer.Peek() : null; // Return the most recent data within the delay window
                }
            }
            return null;
        }

        private void OnLeftHandDelayElapsed(object sender, ElapsedEventArgs e)
        {
            // Apply delayed pose for left hand
            var delayedHandData = GetDelayedHandData(_leftHandDataBuffer, Time.time);
            if (delayedHandData.HasValue)
            {
                leftHand.transform.localPosition = delayedHandData.Value.Pose.position;
                leftHand.transform.localRotation = delayedHandData.Value.Pose.rotation;
                Debug.Log("Applying delayed pose for Left Hand at: " + Time.time);
            }
            else
            {
                Debug.Log("No valid delayed hand data found for Left Hand at: " + Time.time);
            }
            _leftHandDelayTimer.Stop();
            _leftHandDelayTimer = null;
        }

        private void OnRightHandDelayElapsed(object sender, ElapsedEventArgs e)
        {
            // Apply delayed pose for right hand
            var delayedHandData = GetDelayedHandData(_rightHandDataBuffer, Time.time);
            if (delayedHandData.HasValue)
            {
                rightHand.transform.localPosition = delayedHandData.Value.Pose.position;
                rightHand.transform.localRotation = delayedHandData.Value.Pose.rotation;
                Debug.Log("Applying delayed pose for Right Hand at: " + Time.time);
            }
            else
            {
                Debug.Log("No valid delayed hand data found for Right Hand at: " + Time.time);
            }
            _rightHandDelayTimer.Stop();
            _rightHandDelayTimer = null;
        }
    }
}
