using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class GetFeedback_Block : MonoBehaviour
{
    //Pluto public TMP_InputField feedbackSlider;
    public Slider feedbackSlider; 
    public GameObject feedback;

    public void Feedback()
    {
        GameObject experimentManager_block = GameObject.Find("ExperimentManager_Block");
        ProjectManager_Block projectManager_block = experimentManager_block.GetComponent<ProjectManager_Block>();
        string username = projectManager_block.usernameInput.text;
        Debug.Log(projectManager_block.GetcurrentCondition());
        string conditionName = projectManager_block.GetcurrentCondition();
        float response;
        response=feedbackSlider.value;
        Debug.Log("Your Response is  " + response);
        //projectManager_block.SaveToCSV(username, conditionName,response);
        projectManager_block.buttonPressed = true;
        feedback.SetActive(false);
        feedbackSlider.value = feedbackSlider.minValue;
    }
}
