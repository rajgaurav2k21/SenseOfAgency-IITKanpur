using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHand : MonoBehaviour
{
    public Transform Handcollider;
    public Transform hand;

    void Update()
    {
        // Get the current position of the hand
        Vector3 handPosition = hand.position;

        // Assign the position of the collider to match the hand position
        Handcollider.position = handPosition;
    }
}
 