using UnityEngine;
using System.Collections;

public class PathManager : MonoBehaviour
{
    public ProjectManager_Block projectManager_Block;
    public BallMovement ballManager;
    public GameObject starPathObject;
    public GameObject spiralPathObject;
    public GameObject eightPathObject;
    public GameObject circularPathObject;
    public GameObject triangularPathObject;
    public string Trajectory;

    void Start()
    {
        DisableAllPaths();
        StartCoroutine(SwitchPath());
    }

    void DisableAllPaths()
    {
        starPathObject.SetActive(false);
        spiralPathObject.SetActive(false);
        eightPathObject.SetActive(false);
        circularPathObject.SetActive(false);
        triangularPathObject.SetActive(false);

        ballManager.starpathRenderer.enabled = false;
        ballManager.spiralpathRenderer.enabled = false;
        ballManager.eightpathRenderer.enabled = false;
        ballManager.circularpathRenderer.enabled = false;
        ballManager.triangularpathRenderer.enabled = false;
    }

    IEnumerator SwitchPath()
    {
        while (true)
        {
            yield return new WaitUntil(() => projectManager_Block.PathEnabled);
            int newIndex = Random.Range(0, 5);
            DisableAllPaths();
            switch (newIndex)
            {
                case 0:
                    starPathObject.SetActive(true);
                    ballManager.starpathRenderer.enabled = true;
                    Trajectory="Star";
                    break;
                case 1:
                    spiralPathObject.SetActive(true);
                    ballManager.spiralpathRenderer.enabled = true;
                    Trajectory="spiral";
                    break;
                case 2:
                    eightPathObject.SetActive(true);
                    ballManager.eightpathRenderer.enabled = true;
                    Trajectory="Eight";
                    break;
                case 3:
                    circularPathObject.SetActive(true);
                    ballManager.circularpathRenderer.enabled = true;
                    Trajectory="Circle";
                    break;
                case 4:
                    triangularPathObject.SetActive(true);
                    ballManager.triangularpathRenderer.enabled = true;
                    Trajectory="Triangle";
                    break;
            }
            ballManager.SetPathRenderer();
            yield return new WaitForSeconds(50f);
        }
    }
}
