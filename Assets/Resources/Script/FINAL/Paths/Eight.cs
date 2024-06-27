using UnityEngine;

public class Eight : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int numberOfPoints = 200; // Number of points in the figure-eight path
    public float radius = 2f; // Radius that determines the size of the figure-eight

    private Vector3[] initialPositions;

    void Start()
    {
        // Initialize the LineRenderer component
        lineRenderer.positionCount = numberOfPoints;
        initialPositions = new Vector3[numberOfPoints];

        // Generate the figure-eight path
        for (int i = 0; i < numberOfPoints; i++)
        {
            float t = Mathf.PI * 2 * i / (numberOfPoints - 1);
            float x = Mathf.Sin(t) * radius;
            float y = Mathf.Sin(t) * Mathf.Cos(t) * radius;
            initialPositions[i] = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, initialPositions[i]);
        }
    }

    void Update()
    {
        // Update the path to follow the object's transform
        Vector3 emptyPosition = transform.position; // Current position of the object
        Quaternion emptyRotation = transform.rotation; // Current rotation of the object
        Vector3 emptyScale = transform.localScale; // Current scale of the object

        for (int i = 0; i < numberOfPoints; i++)
        {
            Vector3 pointPosition = initialPositions[i];
            // Apply scale to the vertex position
            pointPosition = Vector3.Scale(pointPosition, emptyScale);
            // Apply rotation to the vertex position
            pointPosition = emptyRotation * pointPosition;
            // Update the vertex position in the LineRenderer
            lineRenderer.SetPosition(i, pointPosition + emptyPosition);
        }
    }
}
