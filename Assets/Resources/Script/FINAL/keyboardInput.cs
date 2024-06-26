// Made by RG

using UnityEngine;
using TMPro;

public class KeyboardInput : MonoBehaviour
{
    public TMP_InputField userTMPInputField;

    private void Start()
    {
        userTMPInputField.text = "";
        userTMPInputField.ActivateInputField();
    }

    private void Update()
    {
        GameObject experimentManager_block = GameObject.Find("ExperimentManager_Block");
        ProjectManager_Block projectManager_block = experimentManager_block.GetComponent<ProjectManager_Block>();
        if (Input.anyKeyDown && !userTMPInputField.isFocused)
        {
            userTMPInputField.ActivateInputField();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
           Debug.Log("Enter key pressed");
           projectManager_block.OpenInfoPanel();
        }
    }
}
