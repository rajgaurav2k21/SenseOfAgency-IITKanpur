using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Text;
using Leap.Unity.Interaction;
using System.Collections.Generic;
public class ProjectManager_Block : MonoBehaviour
{
    [Header("Made by RAJ GAURAV")]

    [Header("UltraLeap Manager")]
    public GameObject UltraLeapManager;

    [Header("UI Elements")]
    public GameObject RestText;
    public GameObject EndingFeedback;

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

    public GameObject Experience;

    [Header("Experiment Settings")]
    public GameObject BaselineConditionNoWeight;
    public GameObject NonInterventionCondition;
    public GameObject InterventionConditionMidpoint;
    public GameObject InterventionConditionLagWeighted;
    public GameObject InterventionConditionLagNonWeighted;
    public GameObject InterventionConditionSpatialOffsetWeighted;
    public GameObject InterventionConditionSpatialOffsetNonWeighted;
    public GameObject InterventionConditionDynamicSpatialOffsetWeighted;
    public GameObject InterventionConditionDynamicSpatialOffsetNonWeighted;
    public GameObject InterventionConditionWindNoiseWeighted;
    public GameObject InterventionConditionWindNoiseNonWeighted;


    public GameObject path;
    public GameObject Target;
    public GameObject weight;
    public QuestionnaireManager questionnaireManager;

    [Header("Pickup Messages")]
    public GameObject Pickupmessage_LightWeight;
    public GameObject Pickupmessage_HeavyWeight;

    [Header("Control Variables")]
    public int response;
    public bool next = false;

    // Private variables
    private GameObject[] Conditions;
    private List<GameObject> remainingConditions;
    private int[] conditionCounts;
    private GameObject currentCondition;
    //public bool RestActiveBool=true;
    private string filePath;
    [Header("Wait Variables")]
    public bool buttonPressed = false;
    public bool restActive = false;
    public bool BallPicked = false;
    public bool Startboolean = false;
    public bool ExperiencePicked = false;
    public bool FeedbackendGiven = false;
    public bool PathEnabled=false;
    void Start()
    {
        PathEnabled=false;
        EndingFeedback.SetActive(false);
        BaselineConditionNoWeight.SetActive(false);
        NonInterventionCondition.SetActive(false);
        InterventionConditionMidpoint.SetActive(false);
        InterventionConditionLagWeighted.SetActive(false);
        InterventionConditionLagNonWeighted.SetActive(false);
        InterventionConditionSpatialOffsetWeighted.SetActive(false);
        InterventionConditionSpatialOffsetNonWeighted.SetActive(false);
        InterventionConditionDynamicSpatialOffsetWeighted.SetActive(false);
        InterventionConditionDynamicSpatialOffsetNonWeighted.SetActive(false);
        InterventionConditionWindNoiseWeighted.SetActive(false);
        InterventionConditionWindNoiseNonWeighted.SetActive(false);
        RestText.SetActive(false);
        TaskComplete.SetActive(false);
        Pickupmessage_LightWeight.SetActive(false);
        Pickupmessage_HeavyWeight.SetActive(false);
        userPanel.SetActive(true);
        Target.SetActive(false);
        path.SetActive(false);
        Name.SetActive(true);
        InfoPanel.SetActive(false);
        COMPLETE.SetActive(false);
        GameObject[] Conditions = new GameObject[] {
        BaselineConditionNoWeight,
        NonInterventionCondition,
        InterventionConditionMidpoint,
        InterventionConditionLagWeighted,
        InterventionConditionLagNonWeighted,
        InterventionConditionSpatialOffsetWeighted,
        InterventionConditionSpatialOffsetNonWeighted,
        InterventionConditionDynamicSpatialOffsetWeighted,
        InterventionConditionDynamicSpatialOffsetNonWeighted,
        InterventionConditionWindNoiseWeighted,
        InterventionConditionWindNoiseNonWeighted
        };
        remainingConditions = new List<GameObject>(Conditions);
        conditionCounts = new int[Conditions.Length];
        weight.SetActive(true);
        BallPicked = false;
        Next.SetActive(false);
        Experience.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Startboolean)
        {
            Debug.Log("Exteriment Triggerered Again by Pressing Space");
            StartExp();
        }
    }
    IEnumerator StartExperiment()
{
    int count = 0;
    GameObject weights = GameObject.Find("weights");
    ResetWeight resetWeight = weight.GetComponent<ResetWeight>();
    string[] taskOrder = new string[] { "a", "h", "c", "j", "e", "f", "d", "b", "g", "i", "k", "j", "d", "h", "k", "g", "c", "a", "b", "i", "e", "f", "g", "a", "b", "f", "e", "k", "c", "i", "h", "d", "j", "a", "g", "j", "b", "d", "e", "i", "k", "f", "h", "c" };

    foreach (string task in taskOrder)
    {
        Debug.Log("Next task Loading");
        Next.SetActive(true);
        yield return new WaitUntil(() => next);
        Next.SetActive(false);

        Debug.Log("Pick Up The Weight");
        Debug.Log("Experiment Number: " + count);

        // Deactivate the current condition
        if (currentCondition != null)
        {
            currentCondition.SetActive(false);
            Debug.Log(currentCondition.name + " has been deactivated.");
        }

        Pickupmessage_LightWeight.SetActive(false);
        Pickupmessage_HeavyWeight.SetActive(false);

        switch (task)
        {
            case "a":
                currentCondition = BaselineConditionNoWeight;
                Pickupmessage_LightWeight.SetActive(true);
                Debug.Log("Current Condition: BaselineConditionNoWeight");
                break;

            case "b":
                currentCondition = NonInterventionCondition;
                Pickupmessage_HeavyWeight.SetActive(true);
                Debug.Log("Current Condition: NonInterventionCondition");
                break;

            case "c":
                currentCondition = InterventionConditionMidpoint;
                Pickupmessage_LightWeight.SetActive(true);
                Debug.Log("Current Condition: InterventionConditionMidpoint");
                break;

            case "d":
                currentCondition = InterventionConditionLagWeighted;
                Pickupmessage_HeavyWeight.SetActive(true);
                Debug.Log("Current Condition: InterventionConditionLagWeighted");
                break;

            case "e":
                currentCondition = InterventionConditionLagNonWeighted;
                Pickupmessage_LightWeight.SetActive(true);
                Debug.Log("Current Condition: InterventionConditionLagNonWeighted");
                break;

            case "f":
                currentCondition = InterventionConditionSpatialOffsetWeighted;
                Pickupmessage_HeavyWeight.SetActive(true);
                Debug.Log("Current Condition: InterventionConditionSpatialOffsetWeighted");
                break;

            case "g":
                currentCondition = InterventionConditionSpatialOffsetNonWeighted;
                Pickupmessage_LightWeight.SetActive(true);
                Debug.Log("Current Condition: InterventionConditionSpatialOffsetNonWeighted");
                break;

            case "h":
                currentCondition = InterventionConditionDynamicSpatialOffsetWeighted;
                Pickupmessage_HeavyWeight.SetActive(true);
                Debug.Log("Current Condition: InterventionConditionDynamicSpatialOffsetWeighted");
                break;

            case "i":
                currentCondition = InterventionConditionDynamicSpatialOffsetNonWeighted;
                Pickupmessage_LightWeight.SetActive(true);
                Debug.Log("Current Condition: InterventionConditionDynamicSpatialOffsetNonWeighted");
                break;

            case "j":
                currentCondition = InterventionConditionWindNoiseWeighted;
                Pickupmessage_HeavyWeight.SetActive(true);
                Debug.Log("Current Condition: InterventionConditionWindNoiseWeighted");
                break;

            case "k":
                currentCondition = InterventionConditionWindNoiseNonWeighted;
                Pickupmessage_LightWeight.SetActive(true);
                Debug.Log("Current Condition: InterventionConditionWindNoiseNonWeighted");
                break;
        }

        currentCondition.SetActive(true);
        UltraLeapManager.SetActive(true);
        weight.SetActive(true);
        BallPicked = false;
        Debug.Log("Reseting the Weight position");
        resetWeight.reset = true;
        Debug.Log("Picking Ball for iteration " + count);

        yield return new WaitUntil(() => BallPicked);
        Debug.Log("Weight is been picked");

        PathEnabled = true;
        path.SetActive(true);
        Target.SetActive(true);
        Debug.Log("Experiment Initialized:");

        // Active Time of the Condition
        yield return new WaitForSeconds(5f);

        TaskComplete.SetActive(true);
        Target.SetActive(false);
        path.SetActive(false);
        currentCondition.SetActive(false);
        UltraLeapManager.SetActive(false);
        Debug.Log(currentCondition.name + " is over");
        yield return new WaitForSeconds(2f);
        TaskComplete.SetActive(false);
        feedback.SetActive(true);
        Debug.Log("Feedback done");
        yield return new WaitUntil(() => buttonPressed);
        questionnaireManager.ResetFB = true;
        RestText.SetActive(true);
        rest.SetActive(true);
        Debug.Log("You can Rest");
        yield return new WaitForSeconds(30f);
        yield return new WaitUntil(() => restActive);
        Debug.Log("Rest Up");
        count++;
        BallPicked = false;
        restActive = false;
        rest.SetActive(false);
        buttonPressed = false;
        yield return new WaitForSeconds(1f);
    }

    Debug.Log("Experiment Complete.");
    EndingFeedback.SetActive(true);
    yield return new WaitUntil(() => FeedbackendGiven);
    EndingFeedback.SetActive(false);
    COMPLETE.SetActive(true);
    PathEnabled = false;
}
    public void StartExp()
    {
        StartCoroutine(StartExperiment());
        InfoPanel.SetActive(false);
        userPanel.SetActive(false);
    }

    public void OpenInfoPanel()
    {
        Debug.Log("OpenInfoPanel called");
        Name.SetActive(false);
        userPanel.SetActive(false);
        StartCoroutine(ExperienceCoroutine());
    }
    IEnumerator ExperienceCoroutine()
    {
        Experience.SetActive(true);
        yield return new WaitUntil(() => ExperiencePicked);
        Experience.SetActive(false);
        userPanel.SetActive(true);
        InfoPanel.SetActive(true);
        Startboolean = true;
    }
    public string GetcurrentCondition()
    {
        return currentCondition.name;
    }
}
