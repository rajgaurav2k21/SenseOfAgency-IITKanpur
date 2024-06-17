using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Text;

public class ProjectManager_Block : MonoBehaviour
{
    public GameObject RestText;
    public GameObject DefaultCamera;
    public GameObject COMPLETE;
    public GameObject InfoPanel;
    public GameObject Name;
    public GameObject baselineCondition;
    public GameObject interventionCondition;
    public GameObject nonInterventionCondition;
    public TMP_Text timer;
    public GameObject path;
    public GameObject Target;
    public GameObject userPanel;
    public TMP_InputField usernameInput;
    public GameObject feedback;
    public int response;
    private GameObject[] Conditions;
    private List<GameObject> remainingConditions;
    private int[] conditionCounts;
    private GameObject currentCondition;
    private string filePath;
    public bool buttonPressed = false;
    public bool restActive = false;
    public bool BallPicked = false;
    public GameObject rest;
    public GameObject Pickupmessage_Tennis;
    public GameObject Pickupmessage_Smily;
    public GameObject Pickupmessage_Heavy;
    public Animator AniCondition1;
    public Animator AniCondition2;
    public Animator AniCondition3;
    public GameObject TaskComplete;
    public GameObject weight;
    public GameObject Next;
    public bool next=false;
    void Start()
    {
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
    {
        GameObject baseLineBall = GameObject.Find("BaseLineBall");
        LEDNode lEDNode= baseLineBall.GetComponentInChildren<LEDNode>();
        GameObject nonInterventionBall = GameObject.Find("NonInterventionBall");
        LEDNode lEDNode1= nonInterventionBall.GetComponentInChildren<LEDNode>();
        GameObject interventionBall = GameObject.Find("InterventionBall");
        LEDNode lEDNode2= interventionBall.GetComponentInChildren<LEDNode>();
        int count = 0;
        GameObject weight = GameObject.Find("weights");
        ResetWeight resetWeight= weight.GetComponent<ResetWeight>();
        string[] taskOrder = new string[] { "b", "c", "a", "c", "c", "a", "b", "b", "a", "c", "b", "a", "a", "c", "b", "c", "b", "a" };
        foreach (string task in taskOrder)
        {   Debug.Log("Next task Loading");
            Next.SetActive(true);
            yield return new WaitUntil(() => next);
            //yield return new WaitForSeconds(2f);
            //yield return new WaitForSeconds(1f);
            Debug.Log("Pick Up The Weight");
            Debug.Log("Experiment Number: " + count);
            DefaultCamera.SetActive(false);
            currentCondition = null;
            switch (task)
            {
                case "a":
                    currentCondition = baselineCondition;
                    Debug.Log("Current Condition: baselineCondition");
                    Pickupmessage_Smily.SetActive(true);
                    lEDNode1.enabled = false;
                    lEDNode2.enabled = false;
                    lEDNode.enabled = true;
                    break;
                case "b":
                    currentCondition = interventionCondition;
                    Debug.Log("Current Condition: interventionCondition");
                    Pickupmessage_Tennis.SetActive(true);
                    lEDNode.enabled = false;
                    lEDNode2.enabled = false;
                    lEDNode1.enabled = true;
                    break;
                case "c":
                    currentCondition = nonInterventionCondition;
                    Debug.Log("Current Condition: nonInterventionCondition");
                    Pickupmessage_Heavy.SetActive(true);
                    lEDNode.enabled = false;
                    lEDNode1.enabled = false;
                    lEDNode2.enabled = true;
                    break;
            }
            DefaultCamera.SetActive(false);
            currentCondition.SetActive(true);
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
            yield return new WaitForSeconds(2f);
            TaskComplete.SetActive(true);
            Target.SetActive(false);
            path.SetActive(false);
            currentCondition.SetActive(false);
            Debug.Log(currentCondition + "  is over");
            TaskComplete.SetActive(true);
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
            Next.SetActive(true);
            yield return new WaitUntil(() => next);
            buttonPressed=false;
        }
            AniCondition1.Play("cubef");
            AniCondition2.Play("cubef");
            AniCondition3.Play("cubef");

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
