using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next : MonoBehaviour
{
    private ProjectManager_Block projectManagerBlock;

    void Start()
    {
        GameObject experimentManagerBlock = GameObject.Find("ExperimentManager_Block");
        projectManagerBlock = experimentManagerBlock.GetComponent<ProjectManager_Block>();
    }

    public void NextLoading()
    {
        projectManagerBlock.next = true;
        projectManagerBlock.Next.SetActive(false);
        projectManagerBlock.next = false;
    }
}
