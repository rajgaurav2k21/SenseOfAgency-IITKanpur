using UnityEngine;

public class SpiralPath : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int numberOfPoints = 200; // Number of points in the spiral
    public float initialRadius = 0.1f; // Starting radius of the spiral
    public float radiusIncrement = 0.05f; // Increment of the radius per point
    public float turns = 5f; // Number of turns in the spiral

    private Vector3[] positions;

    void Start()
    {
        // Initialize the LineRenderer component
        lineRenderer.positionCount = numberOfPoints;
        positions = new Vector3[numberOfPoints];

        // Generate the spiral path
        float angleStep = 360f * turns / numberOfPoints;
        for (int i = 0; i < numberOfPoints; i++)
        {
            float angle = i * angleStep;
            float radius = initialRadius + radiusIncrement * i;
            float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
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
            pointPosition = Vector3.Scale(pointPosition, pathScale);
            pointPosition = pathRotation * pointPosition;
            lineRenderer.SetPosition(i, pointPosition + pathPosition);
        }
    }
}
