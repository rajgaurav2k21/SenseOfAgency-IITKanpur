using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryOn : MonoBehaviour
{
    public GameObject TrajectoryManager;
    public void OnTriggerEnter(Collider other)
    {
        TrajectoryManager.SetActive(true);
    }
    public void OnTriggerExit(Collider other)
    {
        TrajectoryManager.SetActive(false);
    }
}
