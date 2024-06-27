using UnityEngine;

public class TriangularPath : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float radius = 5f;

    private Vector3[] positions;

    void Start()
    {
        // Initialize the LineRenderer component
        lineRenderer.positionCount = 4; // 3 vertices + 1 to close the triangle
        positions = new Vector3[4]; // Array to store the vertices of the triangle

        // Generate the triangular path
        for (int i = 0; i < 3; i++)
        {
            float angle = i * Mathf.PI * 2 / 3; // Calculate angle for each vertex
            float x = Mathf.Cos(angle) * radius; // X position
            float y = Mathf.Sin(angle) * radius; // Y position
            positions[i] = new Vector3(x, y, 0); // Store vertex position
            lineRenderer.SetPosition(i, positions[i]); // Set vertex position in LineRenderer
        }

        // Close the triangle by setting the last point to the first vertex
        positions[3] = positions[0];
        lineRenderer.SetPosition(3, positions[0]);
    }

    void Update()
    {
        // Update the path to follow the object's transform
        Vector3 pathPosition = transform.position; // Current position of the object
        Quaternion pathRotation = transform.rotation; // Current rotation of the object
        Vector3 pathScale = transform.localScale; // Current scale of the object

        for (int i = 0; i <= 3; i++) // Iterate through all points including the closing point
        {
            Vector3 pointPosition = positions[i]; // Original position of the vertex
            pointPosition = Vector3.Scale(pointPosition, pathScale); // Scale the vertex position
            pointPosition = pathRotation * pointPosition; // Rotate the vertex position
            lineRenderer.SetPosition(i, pointPosition + pathPosition); // Update the vertex position in LineRenderer
        }
    }
}
