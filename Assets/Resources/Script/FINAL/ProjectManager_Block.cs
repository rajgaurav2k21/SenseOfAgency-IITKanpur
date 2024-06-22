using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Text;
using Leap.Unity.Interaction;

public class ProjectManager_Block : MonoBehaviour
{
    [Header("UltraLeap Manager")]
    public GameObject UltraLeapManager;

    [Header("UI Elements")]
    public GameObject RestText;
    public GameObject DefaultCamera;
    public GameObject COMPLETE;
    public GameObject InfoPanel;
    public GameObject Name;
    public TMP_Text timer;
    public GameObject userPanel;
    public TMP_InputField usernameInput;
    public GameObject feedback;
    public GameObject rest;
    public GameObject TaskComplete;
    public GameObject Next;

    [Header("Experiment Settings")]
    public GameObject baselineCondition;
    public GameObject interventionCondition;
    public GameObject nonInterventionCondition;
    public GameObject path;
    public GameObject Target;
    public GameObject weight;

    [Header("Pickup Messages")]
    public GameObject Pickupmessage_Tennis;
    public GameObject Pickupmessage_Smily;
    public GameObject Pickupmessage_Heavy;

    [Header("Control Variables")]
    public int response;
    public bool next = false;

    // Private variables
    private GameObject[] Conditions;
    private List<GameObject> remainingConditions;
    private int[] conditionCounts;
    private GameObject currentCondition;
    private string filePath;
    [Header("Wait Variables")]
    public bool buttonPressed = false;
    public bool restActive = false;
    public bool BallPicked = false;
    void Start()
    {
        baselineCondition.SetActive(false);
        interventionCondition.SetActive(false);
        nonInterventionCondition.SetActive(false);
        RestText.SetActive(false);
        TaskComplete.SetActive(false);
        Pickupmessage_Tennis.SetActive(false);
        Pickupmessage_Smily.SetActive(false);
        Pickupmessage_Heavy.SetActive(false);
        userPanel.SetActive(true);
        Target.SetActive(false);
        path.SetActive(false);
        Name.SetActive(true);
        DefaultCamera.SetActive(true);
        InfoPanel.SetActive(false);
        COMPLETE.SetActive(false);
        Conditions = new GameObject[] { baselineCondition, interventionCondition, nonInterventionCondition };
        remainingConditions = new List<GameObject>(Conditions);
        conditionCounts = new int[Conditions.Length];
        weight.SetActive(true);
        BallPicked = false;
        Next.SetActive(false);
    }

    private void Awake()
    {
        filePath = Application.dataPath + "/CSV/response.csv";
        Directory.CreateDirectory(Application.dataPath + "/CSV");
        if (!File.Exists(filePath))
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.WriteLine("Username,currentCondition,Response");
            }
        }
    }

    IEnumerator StartExperiment()
    {   GameObject interventionBall = GameObject.Find("InterventionBall");
        GameObject nonInterventionBall = GameObject.Find("NonInterventionBall");
        GameObject baseLineBall = GameObject.Find("BaseLineBall");
        InteractionBehaviour interactionBehaviour= interventionBall.GetComponent<InteractionBehaviour>();
        InteractionBehaviour interactionBehaviour2= nonInterventionBall.GetComponent<InteractionBehaviour>();
        InteractionBehaviour interactionBehaviour3= baseLineBall.GetComponent<InteractionBehaviour>();
        int count = 0;
        GameObject weight = GameObject.Find("weights");
        ResetWeight resetWeight= weight.GetComponent<ResetWeight>();
        string[] taskOrder = new string[] { "b", "c", "a", "c", "c", "a", "b", "b", "a", "c", "b", "a", "a", "c", "b", "c", "b", "a" };
        foreach (string task in taskOrder)
        {   Debug.Log("Next task Loading");
            Next.SetActive(true);
            yield return new WaitUntil(() => next);
            Next.SetActive(false);
            Debug.Log("Pick Up The Weight");
            Debug.Log("Experiment Number: " + count);
            DefaultCamera.SetActive(false);
            currentCondition = null;
            Pickupmessage_Tennis.SetActive(false);
            Pickupmessage_Smily.SetActive(false);
            Pickupmessage_Heavy.SetActive(false);
            interactionBehaviour2.enabled=false;
            interactionBehaviour.enabled=false;
            interactionBehaviour3.enabled=false;
            switch (task)
            {
                case "a":
                    currentCondition = baselineCondition;
                    Debug.Log("Current Condition: baselineCondition");
                    Pickupmessage_Smily.SetActive(true);
                    interactionBehaviour3.enabled=true;
                    break;
                case "b":
                    currentCondition = interventionCondition;
                    Debug.Log("Current Condition: interventionCondition");
                    Pickupmessage_Heavy.SetActive(true);
                    interactionBehaviour.enabled=true;
                    break;
                case "c":
                    currentCondition = nonInterventionCondition;
                    Debug.Log("Current Condition: nonInterventionCondition");
                    Pickupmessage_Tennis.SetActive(true);
                    interactionBehaviour2.enabled=true;
                    break;
            }
            DefaultCamera.SetActive(false);
            currentCondition.SetActive(true);
            UltraLeapManager.SetActive(true);
            //weight.SetActive(true);
            BallPicked = false;
            resetWeight.reset= true;
            Debug.Log("Picking Ball for iteration "+ count);
            yield return new WaitUntil(() => BallPicked);
            Debug.Log("Weight is been picked");   
            path.SetActive(true);
            Target.SetActive(true);
            Debug.Log("Experiment Initialized:]");
            //Active Time of the Condition
            yield return new WaitForSeconds(20f);
            TaskComplete.SetActive(true);
            Target.SetActive(false);
            path.SetActive(false);
            currentCondition.SetActive(false);
            UltraLeapManager.SetActive(false);
            Debug.Log(currentCondition + "  is over");
            TaskComplete.SetActive(true);
            yield return new WaitForSeconds(2f);
            TaskComplete.SetActive(false);
            feedback.SetActive(true);
            DefaultCamera.SetActive(true);
            Debug.Log("fedback done");
            yield return new WaitUntil(() => buttonPressed);
            TaskComplete.SetActive(false);
            RestText.SetActive(true);
            rest.SetActive(true);
            Debug.Log("You can Rest");
            yield return new WaitUntil(() => restActive);
            Debug.Log("Rest Up");
            count++;
            BallPicked = false;
            restActive = false;
            rest.SetActive(false);
            buttonPressed=false;
        }
        Debug.Log("Experiment Complete.");
        COMPLETE.SetActive(true);
    }

    public void StartExp()
    {
        StartCoroutine(StartExperiment());
        InfoPanel.SetActive(false);
        userPanel.SetActive(false);
    }

    public void OpenInfoPanel()
    {
        Name.SetActive(false);
        InfoPanel.SetActive(true);
    }
    public void SaveToCSV(string username, string conditionName, float response)
    {
        using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8))
        {
            string data = string.Format("{0},{1},{2}", username, conditionName, response);
            sw.WriteLine(data);
        }
        Debug.Log("Data saved - Username: " + username + ", ConditionName: " + conditionName + ", Response: " + response);
    }

    public string GetcurrentCondition()
    {
        return currentCondition.name;
    }
}
