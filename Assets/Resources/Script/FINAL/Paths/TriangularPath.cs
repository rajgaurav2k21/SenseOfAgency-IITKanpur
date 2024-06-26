using UnityEngine;

public class TriangularPath : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float radius = 5f;

    private Vector3[] positions;

    void Start()
    {
        lineRenderer.positionCount = 4;
        positions = new Vector3[4];

        for (int i = 0; i < 3; i++)
        {
            float angle = i * Mathf.PI * 2 / 3;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            positions[i] = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, positions[i]);
        }

        positions[3] = positions[0];
        lineRenderer.SetPosition(3, positions[0]);
    }

    void Update()
    {
        Vector3 pathPosition = transform.position;
        Quaternion pathRotation = transform.rotation;
        Vector3 pathScale = transform.localScale;

        for (int i = 0; i <= 3; i++)
        {
            Vector3 pointPosition = positions[i];
            pointPosition = Vector3.Scale(pointPosition, pathScale);
            pointPosition = pathRotation * pointPosition;
            lineRenderer.SetPosition(i, pointPosition + pathPosition);
        }
    }
}
