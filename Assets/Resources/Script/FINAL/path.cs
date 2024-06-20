using UnityEngine;

public class Path : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int numberOfPoints = 200;
    public float radius = 2f;

    private Vector3[] initialPositions;

    void Start()
    {
        lineRenderer.positionCount = numberOfPoints;
        initialPositions = new Vector3[numberOfPoints];

        // Store the initial positions of the points
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
        // Get the position, rotation, and scale of the empty GameObject
        Vector3 emptyPosition = transform.position;
        Quaternion emptyRotation = transform.rotation;
        Vector3 emptyScale = transform.localScale;

        // Offset the positions of the points based on the difference between their initial positions and the empty object's position, rotation, and scale
        for (int i = 0; i < numberOfPoints; i++)
        {
            Vector3 pointPosition = initialPositions[i];
            pointPosition = Vector3.Scale(pointPosition, emptyScale); // Scale the point based on the empty object's scale
            pointPosition = emptyRotation * pointPosition; // Rotate the point based on the empty object's rotation
            lineRenderer.SetPosition(i, pointPosition + emptyPosition);
        }
    }
}
