using UnityEngine;
using Michsky.MUIP;

public class Next : MonoBehaviour
{
    public ProgressBar progressBar;
    private ProjectManager_Block projectManager_block;

    void Start()
    {
        GameObject experimentManager_block = GameObject.Find("ExperimentManager_Block");
        projectManager_block = experimentManager_block.GetComponent<ProjectManager_Block>();
    }

    public void NextLoading()
    {
        
        if (progressBar.currentPercent == 100f)
        {
            Debug.Log("Loading");
            projectManager_block.next = true;
            projectManager_block.Next.SetActive(false);
        }
    }
}
