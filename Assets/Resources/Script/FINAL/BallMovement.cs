using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public LineRenderer starpathRenderer;
    public LineRenderer spiralpathRenderer;
    public LineRenderer eightpathRenderer;
    public LineRenderer circularpathRenderer;
    public LineRenderer triangularpathRenderer;

    public float speed = 2f;

    private LineRenderer pathRenderer;
    private int currentPointIndex = 0;
    private float distanceToNextPoint = 0f;

    void Start()
    {
        SetPathRenderer();
    }

    void Update()
    {
        if (pathRenderer == null || pathRenderer.positionCount < 2)
        {
            return;
        }

        MoveAlongPath();
    }

    public void SetPathRenderer()
    {
        pathRenderer = null;

        if (starpathRenderer.enabled)
        {
            pathRenderer = starpathRenderer;
        }
        else if (spiralpathRenderer.enabled)
        {
            pathRenderer = spiralpathRenderer;
        }
        else if (eightpathRenderer.enabled)
        {
            pathRenderer = eightpathRenderer;
        }
        else if (circularpathRenderer.enabled)
        {
            pathRenderer = circularpathRenderer;
        }
        else if (triangularpathRenderer.enabled)
        {
            pathRenderer = triangularpathRenderer;
        }

        if (pathRenderer == null || pathRenderer.positionCount < 2)
        {
            return;
        }

        currentPointIndex = 0;
        distanceToNextPoint = 0f;
        RecalculateDistanceToNextPoint();
    }

    void MoveAlongPath()
    {
        distanceToNextPoint -= speed * Time.deltaTime;

        while (distanceToNextPoint <= 0)
        {
            currentPointIndex = (currentPointIndex + 1) % pathRenderer.positionCount;
            RecalculateDistanceToNextPoint();
        }

        Vector3 currentPoint = pathRenderer.GetPosition(currentPointIndex);
        Vector3 nextPoint = pathRenderer.GetPosition((currentPointIndex + 1) % pathRenderer.positionCount);

        float t = 1 - distanceToNextPoint / Vector3.Distance(currentPoint, nextPoint);
        transform.position = Vector3.Lerp(currentPoint, nextPoint, t);
    }

    void RecalculateDistanceToNextPoint()
    {
        Vector3 currentPoint = pathRenderer.GetPosition(currentPointIndex);
        Vector3 nextPoint = pathRenderer.GetPosition((currentPointIndex + 1) % pathRenderer.positionCount);
        distanceToNextPoint += Vector3.Distance(currentPoint, nextPoint);
    }
}
