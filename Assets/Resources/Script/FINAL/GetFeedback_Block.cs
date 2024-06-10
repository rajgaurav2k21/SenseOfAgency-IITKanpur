using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetFeedback_Block : MonoBehaviour
{
    public TMP_InputField feedbackInput;
    public GameObject feedback;

    public void Feedback()
    {
        GameObject experimentManager_block = GameObject.Find("ExperimentManager_Block");
        ProjectManager_Block projectManager_block = experimentManager_block.GetComponent<ProjectManager_Block>();
        string username = projectManager_block.usernameInput.text;
        Debug.Log(projectManager_block.GetcurrentCondition());
        string conditionName = projectManager_block.GetcurrentCondition();
        int response;
        if (int.TryParse(feedbackInput.text, out response))
       {
        projectManager_block.SaveToCSV(username, conditionName,response);
        projectManager_block.buttonPressed = true;
        feedback.SetActive(false);
        feedbackInput.text = "";
       }
    }
}
