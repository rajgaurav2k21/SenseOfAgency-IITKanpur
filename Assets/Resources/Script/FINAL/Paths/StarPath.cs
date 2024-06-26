using UnityEngine;

public class StarPath : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int numberOfPoints = 10; // Number of points in the star (should be an even number for a symmetrical star)
    public float outerRadius = 5f; // Outer radius of the star
    public float innerRadius = 2.5f; // Inner radius of the star

    private Vector3[] positions;

    void Start()
    {
        // Initialize the LineRenderer component
        lineRenderer.positionCount = numberOfPoints + 1; // Include the first point again at the end to close the star
        positions = new Vector3[numberOfPoints + 1];

        // Generate the star path
        for (int i = 0; i < numberOfPoints; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfPoints;
            float radius = (i % 2 == 0) ? outerRadius : innerRadius;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            positions[i] = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, positions[i]);
        }
        // Close the star path by setting the last point to the first point
        positions[numberOfPoints] = positions[0];
        lineRenderer.SetPosition(numberOfPoints, positions[0]);
    }

    void Update()
    {
        // Update the path to follow the object's transform
        Vector3 pathPosition = transform.position;
        Quaternion pathRotation = transform.rotation;
        Vector3 pathScale = transform.localScale;

        for (int i = 0; i <= numberOfPoints; i++) // Iterate through all points including the closing point
        {
            Vector3 pointPosition = positions[i];
            pointPosition = Vector3.Scale(pointPosition, pathScale);
            pointPosition = pathRotation * pointPosition;
            lineRenderer.SetPosition(i, pointPosition + pathPosition);
        }
    }
}
