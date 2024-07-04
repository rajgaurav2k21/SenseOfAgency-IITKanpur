using UnityEngine;

public class TrajectoryOn : MonoBehaviour
{
    [SerializeField]
    private GameObject trajectoryManager;

    private void Start()
    {
        trajectoryManager.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weight"))
        {
            Debug.Log("Entering Trajectory");
            trajectoryManager.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Weight"))
        {
            Debug.Log("Exiting Trajectory");
            trajectoryManager.SetActive(false);
        }
    }
}
