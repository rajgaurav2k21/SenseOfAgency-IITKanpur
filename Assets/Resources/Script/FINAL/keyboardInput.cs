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
        // Activate the input field if any key is pressed and it's not already focused
        if (Input.anyKeyDown && !userTMPInputField.isFocused)
        {
            userTMPInputField.ActivateInputField();
        }

        // Check for Enter key press when the input field is focused
        if (Input.GetKeyDown(KeyCode.Return))
        {
           Debug.Log("Enter key pressed");
           projectManager_block.OpenInfoPanel();
        }
    }
}
