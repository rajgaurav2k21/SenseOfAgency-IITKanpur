using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Text;

public class ProjectManager : MonoBehaviour
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
    private GameObject currentConditionGameObject;
    private string filePath;
    public bool buttonPressed = false;

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
    //yield return StartCoroutine(StartCountdown(30));
    yield return new WaitForSeconds(5f);
    path.SetActive(true);
    Target.SetActive(true);
    Debug.Log("Experiment Initialized:]");
    System.Random random = new System.Random();
    int count = 0;

    // Repeat the loop until all conditions have occurred 3 times
    while (remainingConditions.Count > 0)
    {
        int index = random.Next(remainingConditions.Count);
        currentConditionGameObject = remainingConditions[index];
        DefaultCamera.SetActive(false);
        currentConditionGameObject.SetActive(true);
        Target.SetActive(true);
        path.SetActive(true);
        yield return new WaitForSeconds(24f);
        Target.SetActive(false);
        path.SetActive(false);
        currentConditionGameObject.SetActive(false);
        DefaultCamera.SetActive(true);
        feedback.SetActive(true);
        yield return new WaitUntil(() => buttonPressed);
        int conditionIndex = System.Array.IndexOf(Conditions, currentConditionGameObject);
        conditionCounts[conditionIndex]++;
        if (conditionCounts[conditionIndex] >= 3)
        {
            remainingConditions.Remove(currentConditionGameObject);
        }
        DefaultCamera.SetActive(false);
        yield return new WaitForSeconds(3f);
        count++;
        Debug.Log("Next condition: " + currentConditionGameObject.name);
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
        IEnumerator StartCountdown(int seconds)
    {
        for (int i = seconds; i >= 0; i--)
        {
            if (i <= 5)
            {
                timer.text = "Ready";
            }
            else
            {
                timer.text = i.ToString();
            }
            yield return new WaitForSeconds(1f);
        }
        timer.gameObject.SetActive(false);
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
        return currentConditionGameObject.name;
    }
}