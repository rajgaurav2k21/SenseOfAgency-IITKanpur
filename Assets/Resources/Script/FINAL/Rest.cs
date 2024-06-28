using UnityEngine;

public class Rest : MonoBehaviour
{
    public ProjectManager_Block projectManager_block;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Rest Up");
            projectManager_block.restActive = true;
            
            projectManager_block.RestText.SetActive(false);
        }
    }
}
