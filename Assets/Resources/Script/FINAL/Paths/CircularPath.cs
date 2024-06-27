using UnityEngine;

public class CircularPath : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int numberOfPoints = 200; // Number of points in the path
    public float radius = 5f; // Radius of the circle

    private Vector3[] positions;

    void Start()
    {
        // Initialize the LineRenderer component
        lineRenderer.positionCount = numberOfPoints;
        positions = new Vector3[numberOfPoints];

        // Generate the circular path
        for (int i = 0; i < numberOfPoints; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfPoints;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            positions[i] = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, positions[i]);
        }
    }

    void Update()
    {
        // Update the path to follow the object's transform
        Vector3 pathPosition = transform.position;
        Quaternion pathRotation = transform.rotation;
        Vector3 pathScale = transform.localScale;

        for (int i = 0; i < numberOfPoints; i++)
        {
            Vector3 pointPosition = positions[i];
            // Apply scale
            pointPosition = Vector3.Scale(pointPosition, pathScale);
            // Apply rotation
            pointPosition = pathRotation * pointPosition;
            // Update LineRenderer positions
            lineRenderer.SetPosition(i, pointPosition + pathPosition);
        }
    }
}
