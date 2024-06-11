using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Text;

public class ProjectManager_Block : MonoBehaviour
{
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
    void Start()
    {
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
        yield return new WaitForSeconds(3f);
        Debug.Log("Pick Up The Weight");
        int count = 0;
        string[] taskOrder = new string[] { "b", "c", "a", "c", "c", "a", "b", "b", "a", "c", "b", "a", "a", "c", "b", "c", "b", "a" };
        foreach (string task in taskOrder)
        {
            DefaultCamera.SetActive(false);
            currentCondition = null;
            switch (task)
            {
                case "a":
                    currentCondition = baselineCondition;
                    Debug.Log("Current Condition: baselineCondition");
                    Pickupmessage_Smily.SetActive(true);
                    break;
                case "b":
                    currentCondition = interventionCondition;
                    Debug.Log("Current Condition: interventionCondition");
                    Pickupmessage_Tennis.SetActive(true);
                    break;
                case "c":
                    currentCondition = nonInterventionCondition;
                    Debug.Log("Current Condition: nonInterventionCondition");
                    Pickupmessage_Heavy.SetActive(true);
                    break;
            }
            yield return new WaitUntil(() => BallPicked);   
            path.SetActive(true);
            Target.SetActive(true);
            Debug.Log("Experiment Initialized:]");
            currentCondition.SetActive(true);
            //Active Time of the Condition
            yield return new WaitForSeconds(20f);
            Target.SetActive(false);
            path.SetActive(false);
            currentCondition.SetActive(false);
            Debug.Log(currentCondition + "  is over");
            DefaultCamera.SetActive(true);
            feedback.SetActive(true);
            yield return new WaitUntil(() => buttonPressed);
            rest.SetActive(true);
            Debug.Log("You can Rest");
            yield return new WaitUntil(() => restActive);
            DefaultCamera.SetActive(false);
            //yield return new WaitForSeconds(3f);
            count++;
            Debug.Log("Experiment Number: " + count);
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
