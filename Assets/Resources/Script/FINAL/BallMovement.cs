using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public LineRenderer pathRenderer;
    public float speed = 2f;

    private int currentPointIndex = 0;
    private float distanceToNextPoint = 0f;

    void Start()
    {
        if (pathRenderer.positionCount < 2)
            return;

        RecalculateDistanceToNextPoint();
    }

    void Update()
    {
        if (pathRenderer.positionCount < 2)
            return;

        MoveAlongPath();
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
