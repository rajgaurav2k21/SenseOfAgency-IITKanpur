using UnityEngine;
using TMPro;

public class keyboardAge : MonoBehaviour
{
    public TMP_InputField ageTMPInputField;
    public KeyboardInput nameScript;
    private ProjectManager_Block projectManagerBlock;

    private void Start()
    {
        nameScript.enabled = false;
        ageTMPInputField.text = "";
        
        // Cache reference to ProjectManager_Block to avoid repeated calls
        GameObject experimentManagerBlock = GameObject.Find("ExperimentManager_Block");
        projectManagerBlock = experimentManagerBlock.GetComponent<ProjectManager_Block>();
    }

    public void ActivateAgeInputField()
    {
        ageTMPInputField.ActivateInputField();
    }

    private void Update()
    {
        if (Input.anyKeyDown && !ageTMPInputField.isFocused)
        {
            ageTMPInputField.ActivateInputField();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            projectManagerBlock.OpenInfoPanel();
        }
    }
}
