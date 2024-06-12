using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject experimentManager_block = GameObject.Find("ExperimentManager_Block");
        ProjectManager_Block projectManager_block = experimentManager_block.GetComponent<ProjectManager_Block>();
        projectManager_block.BallPicked = true;
        projectManager_block.Pickupmessage_Smily.SetActive(false);
        projectManager_block.Pickupmessage_Tennis.SetActive(false);
        projectManager_block.Pickupmessage_Heavy.SetActive(false);

    }
}
