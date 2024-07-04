using UnityEngine;

public class AttachToPalm : MonoBehaviour
{
    // Reference to the empty GameObject that defines the desired position and rotation
    public Transform positionMarker;

    private bool isAttached = false; // Tracks if the cube is attached
    private Transform palmTransform; // Stores the transform of the palm

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("HandColliderLag");

        // Ensure the cube is not already attached and the position marker is set
        
            // Get the transform of the collider (should be the palm)
            palmTransform = other.transform;

            // Set the parent of the cube to be the palm
            this.transform.SetParent(palmTransform);

            // Initially set the cube's position and rotation relative to the palm
            UpdateCubePositionAndRotation();

            // Mark the cube as attached
            isAttached = true;
        
    }

    void Update()
    {
        // Continuously update the cube's position and rotation relative to the positionMarker
        if (isAttached && palmTransform != null)
        {
            UpdateCubePositionAndRotation();
        }
    }

    void UpdateCubePositionAndRotation()
    {
        if (palmTransform == null || positionMarker == null)
            return;

        // Calculate the local position and rotation of the cube relative to the palm
        Vector3 localPosition = palmTransform.InverseTransformPoint(positionMarker.position);
        Quaternion localRotation = Quaternion.Inverse(palmTransform.rotation) * positionMarker.rotation;

        // Set the cube's local position and rotation to match the position marker
        this.transform.localPosition = localPosition;
        this.transform.localRotation = localRotation;
    }

    public void Detach()
    {
        // Detach the cube from the palm
        this.transform.SetParent(null);
        isAttached = false; // Mark the cube as not attached
    }
}
