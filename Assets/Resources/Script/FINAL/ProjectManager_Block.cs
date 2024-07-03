using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Text;
using Leap.Unity.Interaction;
public class ProjectManager_Block : MonoBehaviour
{
    [Header("Made by RAJ GAURAV")]

    [Header("Ignite")]
    public GameObject Lighter;
    public GameObject UICamera;
    public GameObject ExpCamera;

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

    [Header("Conditions")]
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

    [Header("Path & Weights")]
    public GameObject path;
    public GameObject Target;

    [Header("Weights")]
    public GameObject weight;
    public GameObject lightWeight;
    public GameObject heavyWeight;
    public Light lightComp;
    public Light heavyComp;

    [Header("Script Refs")]
    public QuestionnaireManager questionnaireManager;
    public PickUpLight pickupLight;
    public PickUpHeavy pickupHeavy;

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
    private string filePath;
    [Header("Wait Variables")]
    public bool buttonPressed = false;
    public bool restActive = false;
    public bool BallPicked = false;
    public bool ExperiencePicked = false;
    public bool FeedbackendGiven = false;
    public bool PathEnabled = false;
    void Start()
    {
        lightComp = lightWeight.GetComponent<Light>();
        heavyComp = heavyWeight.GetComponent<Light>();
        feedback.SetActive(false);
        ExpCamera.SetActive(false);
        UICamera.SetActive(true);
        Lighter.SetActive(false);
        PathEnabled = false;
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
        lightComp.enabled = false;
        heavyComp.enabled = false;
    }
    IEnumerator StartExperiment()
    {
        Lighter.SetActive(false);
        int count = 0;
        GameObject weights = GameObject.Find("weights");
        ResetWeight resetWeight = weight.GetComponent<ResetWeight>();
        Debug.Log("Next task Loading");
        Next.SetActive(true);
        yield return new WaitUntil(() => next);
        UICamera.SetActive(false);
        ExpCamera.SetActive(true);
        Next.SetActive(false);
        string[] taskOrder = new string[] { "a", "h", "c", "j", "e", "f", "d", "b", "g", "i", "k", "j", "d", "h", "k", "g", "c", "a", "b", "i", "e", "f", "g", "a", "b", "f", "e", "k", "c", "i", "h", "d", "j", "a", "g", "j", "b", "d", "e", "i", "k", "f", "h", "c" };

        foreach (string task in taskOrder)
        {
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
                    lightComp.enabled = true;
                    pickupLight.currentTransform=0;
                    break;

                case "b":
                    currentCondition = NonInterventionCondition;
                    Pickupmessage_HeavyWeight.SetActive(true);
                    Debug.Log("Current Condition: NonInterventionCondition");
                    heavyComp.enabled = true;
                    pickupHeavy.currentTransform=0;
                    break;

                case "c":
                    currentCondition = InterventionConditionMidpoint;
                    Pickupmessage_HeavyWeight.SetActive(true);
                    Debug.Log("Current Condition: InterventionConditionMidpoint");
                    heavyComp.enabled = true;
                    pickupHeavy.currentTransform=1;
                    break;

                case "d":
                    currentCondition = InterventionConditionLagWeighted;
                    Pickupmessage_HeavyWeight.SetActive(true);
                    Debug.Log("Current Condition: InterventionConditionLagWeighted");
                    heavyComp.enabled = true;
                    pickupHeavy.currentTransform=2;
                    break;

                case "e":
                    currentCondition = InterventionConditionLagNonWeighted;
                    Pickupmessage_LightWeight.SetActive(true);
                    Debug.Log("Current Condition: InterventionConditionLagNonWeighted");
                    lightComp.enabled = true;
                    pickupLight.currentTransform=1;
                    break;

                case "f":
                    currentCondition = InterventionConditionSpatialOffsetWeighted;
                    Pickupmessage_HeavyWeight.SetActive(true);
                    Debug.Log("Current Condition: InterventionConditionSpatialOffsetWeighted");
                    heavyComp.enabled = true;
                    pickupHeavy.currentTransform=3;
                    break;

                case "g":
                    currentCondition = InterventionConditionSpatialOffsetNonWeighted;
                    Pickupmessage_LightWeight.SetActive(true);
                    Debug.Log("Current Condition: InterventionConditionSpatialOffsetNonWeighted");
                    lightComp.enabled = true;
                    pickupLight.currentTransform=2;
                    break;

                case "h":
                    currentCondition = InterventionConditionDynamicSpatialOffsetWeighted;
                    Pickupmessage_HeavyWeight.SetActive(true);
                    Debug.Log("Current Condition: InterventionConditionDynamicSpatialOffsetWeighted");
                    heavyComp.enabled = true;
                    pickupHeavy.currentTransform=4;
                    break;

                case "i":
                    currentCondition = InterventionConditionDynamicSpatialOffsetNonWeighted;
                    Pickupmessage_LightWeight.SetActive(true);
                    Debug.Log("Current Condition: InterventionConditionDynamicSpatialOffsetNonWeighted");
                    lightComp.enabled = true;
                    pickupLight.currentTransform=3;
                    break;

                case "j":
                    currentCondition = InterventionConditionWindNoiseWeighted;
                    Pickupmessage_HeavyWeight.SetActive(true);
                    Debug.Log("Current Condition: InterventionConditionWindNoiseWeighted");
                    heavyComp.enabled = true;
                    pickupHeavy.currentTransform=5;
                    break;

                case "k":
                    currentCondition = InterventionConditionWindNoiseNonWeighted;
                    Pickupmessage_LightWeight.SetActive(true);
                    Debug.Log("Current Condition: InterventionConditionWindNoiseNonWeighted");
                    lightComp.enabled = true;
                    pickupLight.currentTransform=4;
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
            yield return new WaitForSeconds(2f);

            TaskComplete.SetActive(true);
            Target.SetActive(false);
            path.SetActive(false);
            currentCondition.SetActive(false);
            UltraLeapManager.SetActive(false);
            Debug.Log("Detached");
            pickupLight.Detach();
            pickupHeavy.Detach();
            Debug.Log(currentCondition.name + " is over");
            yield return new WaitForSeconds(5f);
            TaskComplete.SetActive(false);
            UICamera.SetActive(true);
            ExpCamera.SetActive(false);
            feedback.SetActive(true);
            questionnaireManager.ResetQuestionnaire();
            Debug.Log("Feedback done");
            yield return new WaitUntil(() => buttonPressed);
            RestText.SetActive(true);
            rest.SetActive(true);
            BaselineConditionNoWeight.SetActive(true);
            Debug.Log("You can Rest");
            yield return new WaitUntil(() => restActive);
            BaselineConditionNoWeight.SetActive(false);
            Debug.Log("Rest Up");
            Debug.Log("Restting feedback");
            count++;
            BallPicked = false;
            restActive = false;
            rest.SetActive(false);
            buttonPressed = false;
            UICamera.SetActive(false);
            ExpCamera.SetActive(true);
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
        Lighter.SetActive(true);
    }
    public string GetcurrentCondition()
    {
        return currentCondition.name;
    }
}
