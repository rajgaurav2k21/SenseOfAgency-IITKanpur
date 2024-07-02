using UnityEngine;

public class Rest : MonoBehaviour
{
    public ProjectManager_Block projectManager_block;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!projectManager_block.restActive) // Check if rest is not already active
            {
                projectManager_block.restActive = true;
                projectManager_block.RestText.SetActive(false);
                Debug.Log("Rest Up");
            }
        }
    }
}
