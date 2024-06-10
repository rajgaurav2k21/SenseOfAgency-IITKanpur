using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : MonoBehaviour
{
    private void OntriggerExit(Collider other)
    {
        GameObject experimentManager_block = GameObject.Find("ExperimentManager_Block");
        ProjectManager_Block projectManager_block = experimentManager_block.GetComponent<ProjectManager_Block>();
        projectManager_block.restActive = true;
        Debug.Log("Rest is Active");
        Debug.Log("rest"+ other.gameObject.name);
    }
}
