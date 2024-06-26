using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
        Debug.Log("Weight is Up");
        Debug.Log(" Weight Disturbed by" + other.name);
        GameObject experimentManager_block = GameObject.Find("ExperimentManager_Block");
        ProjectManager_Block projectManager_block = experimentManager_block.GetComponent<ProjectManager_Block>();
        projectManager_block.BallPicked = true;
        projectManager_block.Pickupmessage_LightWeight.SetActive(false);
        projectManager_block.Pickupmessage_HeavyWeight.SetActive(false);
        
        }
    }
}
