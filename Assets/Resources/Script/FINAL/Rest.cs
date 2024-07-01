using UnityEngine;

public class Rest : MonoBehaviour
{
    public ProjectManager_Block projectManager_block;
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            projectManager_block.restActive = true;
            projectManager_block.RestText.SetActive(false);
            Debug.Log("Rest Up");
        }
    }
}
