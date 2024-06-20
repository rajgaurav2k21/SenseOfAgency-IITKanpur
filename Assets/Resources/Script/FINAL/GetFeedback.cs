using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetFeedback : MonoBehaviour
{
    public TMP_InputField feedbackInput;
    public GameObject feedback;

    public void Feedback()
    {
        GameObject experimentManager_random = GameObject.Find("ExperimentManager_Random");
        ProjectManager projectManager= experimentManager_random .GetComponent<ProjectManager>();
        string username = projectManager.usernameInput.text;
        string conditionName = projectManager.GetcurrentCondition();
        int response;
        if (int.TryParse(feedbackInput.text, out response))
       {
        projectManager.SaveToCSV(username, conditionName,response);
        projectManager.buttonPressed = true;
        feedback.SetActive(false);
        feedbackInput.text = "";
       }
    }
}
