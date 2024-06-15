using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Rest is Active");
        Debug.Log("Exiting trigger: " + other.gameObject.name);
        GameObject experimentManager_block = GameObject.Find("ExperimentManager_Block");
        ProjectManager_Block projectManager_block = experimentManager_block.GetComponent<ProjectManager_Block>();
        projectManager_block.restActive = true;
        projectManager_block.RestText.SetActive(false);
    }
}
