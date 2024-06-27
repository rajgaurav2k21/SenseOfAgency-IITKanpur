using UnityEngine;

public class StarPath : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int numberOfPoints = 10;
    public float outerRadius = 5f;
    public float innerRadius = 2.5f;

    private Vector3[] positions;

    void Start()
    {
        lineRenderer.positionCount = numberOfPoints + 1; 
        positions = new Vector3[numberOfPoints + 1];

        // Initial positions for the star points
        for (int i = 0; i < numberOfPoints; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfPoints;
            float radius = (i % 2 == 0) ? outerRadius : innerRadius;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            positions[i] = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, positions[i]);
        }
        positions[numberOfPoints] = positions[0];
        lineRenderer.SetPosition(numberOfPoints, positions[0]);
    }

    void Update()
    {
        Vector3 pathPosition = transform.position;
        Quaternion pathRotation = transform.rotation;
        Vector3 pathScale = transform.localScale;

        for (int i = 0; i <= numberOfPoints; i++)
        {
            Vector3 pointPosition = positions[i];
            pointPosition = Vector3.Scale(pointPosition, pathScale);
            pointPosition = pathRotation * pointPosition;
            lineRenderer.SetPosition(i, pointPosition + pathPosition);
        }
    }
}
