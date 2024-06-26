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
        Vector3 emptyPosition = transform.position;
        Quaternion emptyRotation = transform.rotation;
        Vector3 emptyScale = transform.localScale;
        for (int i = 0; i < numberOfPoints; i++)
        {
            Vector3 pointPosition = initialPositions[i];
            pointPosition = Vector3.Scale(pointPosition, emptyScale);
            pointPosition = emptyRotation * pointPosition;
            lineRenderer.SetPosition(i, pointPosition + emptyPosition);
        }
    }
}
