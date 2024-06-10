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
    public GameObject rest;
    void Start()
    {
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
        yield return new WaitForSeconds(5f);
        path.SetActive(true);
        Target.SetActive(true);
        Debug.Log("Experiment Initialized:]");
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
                    break;
                case "b":
                    currentCondition = interventionCondition;
                    Debug.Log("Current Condition: interventionCondition");
                    break;
                case "c":
                    currentCondition = nonInterventionCondition;
                    Debug.Log("Current Condition: nonInterventionCondition");
                    break;
            }
            currentCondition.SetActive(true);
            //Active Time of the Condition
            yield return new WaitForSeconds(20f);
            Target.SetActive(false);
            path.SetActive(false);
            currentCondition.SetActive(false);
            DefaultCamera.SetActive(true);
            feedback.SetActive(true);
            yield return new WaitUntil(() => buttonPressed);
            rest.SetActive(true);
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

    public void SaveToCSV(string username, string conditionName, int response)
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
