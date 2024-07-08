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
    public GameObject Misconception;

    [Header("Conditions")]
    public GameObject BaselineConditionNoWeight;
    public GameObject NonInterventionCondition;
    public GameObject InterventionConditionMidpoint;
    public GameObject InterventionConditionLagWeighted;
    public GameObject InterventionConditionLagNonWeighted;
    public GameObject InterventionConditionLagWeightedMarker;
    public GameObject InterventionConditionSpatialOffsetWeighted;
    public GameObject InterventionConditionSpatialOffsetNonWeighted;
    public GameObject InterventionConditionSpatialOffsetWeightedMarker;
    public GameObject InterventionConditionDynamicSpatialOffsetWeighted;
    public GameObject InterventionConditionDynamicSpatialOffsetNonWeighted;
    public GameObject InterventionConditionDynamicSpatialOffsetWeightedMarker;
    public GameObject InterventionConditionWindNoiseWeighted;
    public GameObject InterventionConditionWindNoiseNonWeighted;
    public GameObject InterventionConditionWindNoiseWeightedMarker;

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

    [Header("Duration Of Experiment")]
    public float ExperimentDuration = 70f;

    [Header("Trajectory Tracker")]
    public GameObject TrajectoryTracker;
    public TrajectoryBaseline trajectoryBaselineScript;
    public TrajectoryInterventionDynamicSpatialOffsetNW trajectoryInterventionDynamicSpatialOffsetNWScript;
    public TrajectoryInterventionDynamicSpatialOffsetW trajectoryInterventionDynamicSpatialOffsetWScript;
    public TrajectoryInterventionDynamicSpatialOffsetWM trajectoryInterventionDynamicSpatialOffsetWMScript;
    public TrajectoryInterventionLagNW trajectoryInterventionLagNWScript;
    public TrajectoryInterventionLagW trajectoryInterventionLagWScript;
    public TrajectoryInterventionLagWM trajectoryInterventionLagWMScript;
    public TrajectoryInterventionMidpoint trajectoryInterventionMidpointScript;
    public TrajectoryInterventionSpatialOffsetNW trajectoryInterventionSpatialOffsetNWScript;
    public TrajectoryInterventionSpatialOffsetW trajectoryInterventionSpatialOffsetWScript;
    public TrajectoryInterventionSpatialOffsetWM trajectoryInterventionSpatialOffsetWMScript;
    public TrajectoryInterventionWindNoiceNW trajectoryInterventionWindNoiceNWScript;
    public TrajectoryInterventionWindNoiceW trajectoryInterventionWindNoiceWScript;
    public TrajectoryInterventionWindNoiceWM trajectoryInterventionWindNoiceWMScript;
    public TrajectoryNonIntervention trajectoryNonInterventionScript;
    [Header("Video Player")]
    public VideoPlayer videoPlayer;
    void Start()
    {
        TrajectoryTracker.SetActive(false);
        trajectoryBaselineScript.enabled = false;
        trajectoryNonInterventionScript.enabled = false;
        trajectoryInterventionMidpointScript.enabled = false;
        trajectoryInterventionLagWScript.enabled = false;
        trajectoryInterventionLagNWScript.enabled = false;
        trajectoryInterventionSpatialOffsetWScript.enabled = false;
        trajectoryInterventionSpatialOffsetNWScript.enabled = false;
        trajectoryInterventionDynamicSpatialOffsetWScript.enabled = false;
        trajectoryInterventionDynamicSpatialOffsetNWScript.enabled = false;
        trajectoryInterventionWindNoiceWScript.enabled = false;
        trajectoryInterventionWindNoiceNWScript.enabled = false;
        trajectoryInterventionLagWMScript.enabled = false;
        trajectoryInterventionDynamicSpatialOffsetWMScript.enabled = false;
        trajectoryInterventionSpatialOffsetWMScript.enabled = false;
        trajectoryInterventionWindNoiceWMScript.enabled = false;


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
        InterventionConditionWindNoiseWeightedMarker.SetActive(false);
        InterventionConditionDynamicSpatialOffsetWeightedMarker.SetActive(false);
        InterventionConditionLagWeightedMarker.SetActive(false);
        InterventionConditionSpatialOffsetWeightedMarker.SetActive(false);
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
        InterventionConditionLagWeightedMarker,
        InterventionConditionSpatialOffsetWeighted,
        InterventionConditionSpatialOffsetNonWeighted,
        InterventionConditionSpatialOffsetWeightedMarker,
        InterventionConditionDynamicSpatialOffsetWeighted,
        InterventionConditionDynamicSpatialOffsetNonWeighted,
        InterventionConditionDynamicSpatialOffsetWeightedMarker,
        InterventionConditionWindNoiseWeighted,
        InterventionConditionWindNoiseNonWeighted,
        InterventionConditionWindNoiseWeightedMarker
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
        Next.SetActive(true);
        yield return new WaitUntil(() => next);
        UICamera.SetActive(false);
        ExpCamera.SetActive(true);
        Next.SetActive(false);
        string[] taskOrder = new string[] {"m","n","o"};

        //string[] taskOrder = new string[] {"a", "k", "b", "d", "j", "o", "c", "m", "n", "h", "e", "f", "b", "a", "i", "o", "m", "k", "e", "d", "l", "c", "j", "i", "h", "m", "n", "k", "a", "b", "e", "o", "f", "i", "l", "h", "d", "g", "j", "m", "c", "a", "e", "k", "l", "b", "i", "o", "n", "d", "g", "f", "j", "h", "c", "a", "l", "n", "o", "m", "g", "k", "b", "f", "i", "d", "e", "h", "l", "j", "c", "g", "a", "o", "f", "b", "m", "n", "i", "d", "k", "l", "e", "h", "g", "c", "j", "f", "n", "i", "b", "o", "m", "a", "k", "d", "g"};

        foreach (string task in taskOrder)
        {

            // Deactivate the current condition
            if (currentCondition != null)
            {
                currentCondition.SetActive(false);
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
                    pickupLight.currentTransform = 0;
                    trajectoryBaselineScript.enabled = true;
                    break;

                case "b":
                    currentCondition = NonInterventionCondition;
                    Pickupmessage_HeavyWeight.SetActive(true);
                    Debug.Log("Current Condition: NonInterventionCondition");
                    heavyComp.enabled = true;
                    pickupHeavy.currentTransform = 0;
                    trajectoryNonInterventionScript.enabled = true;
                    break;

                case "c":
                    currentCondition = InterventionConditionMidpoint;
                    Pickupmessage_HeavyWeight.SetActive(true);
                    Debug.Log("Current Condition: InterventionConditionMidpoint");
                    heavyComp.enabled = true;
                    pickupHeavy.currentTransform = 1;
                    trajectoryInterventionMidpointScript.enabled = true;
                    break;

                case "d":
                    currentCondition = InterventionConditionLagWeighted;
                    Pickupmessage_HeavyWeight.SetActive(true);
                    Debug.Log("Current Condition: InterventionConditionLagWeighted");
                    heavyComp.enabled = true;
                    pickupHeavy.currentTransform = 2;
                    trajectoryInterventionLagWScript.enabled = true;
                    break;

                case "e":
                    currentCondition = InterventionConditionLagNonWeighted;
                    Pickupmessage_LightWeight.SetActive(true);
                    Debug.Log("Current Condition: InterventionConditionLagNonWeighted");
                    lightComp.enabled = true;
                    pickupLight.currentTransform = 1;
                    trajectoryInterventionLagNWScript.enabled = true;
                    break;

                case "f":
                    currentCondition = InterventionConditionLagWeightedMarker;
                    Pickupmessage_HeavyWeight.SetActive(true);
                    Debug.Log("Current Condition: InterventionConditionLagWeightedMarker");
                    heavyComp.enabled = true;
                    pickupHeavy.currentTransform = 3;
                    trajectoryInterventionLagWMScript.enabled = true;
                    break;

                case "g":
                    currentCondition = InterventionConditionSpatialOffsetWeighted;
                    Pickupmessage_HeavyWeight.SetActive(true);
                    Debug.Log("Current Condition: InterventionConditionSpatialOffsetWeighted");
                    heavyComp.enabled = true;
                    pickupHeavy.currentTransform = 4;
                    trajectoryInterventionSpatialOffsetWScript.enabled = true;
                    break;

                case "h":
                    currentCondition = InterventionConditionSpatialOffsetNonWeighted;
                    Pickupmessage_LightWeight.SetActive(true);
                    Debug.Log("Current Condition: InterventionConditionSpatialOffsetNonWeighted");
                    lightComp.enabled = true;
                    pickupLight.currentTransform = 2;
                    trajectoryInterventionSpatialOffsetNWScript.enabled = true;
                    break;

                case "i":
                    currentCondition = InterventionConditionSpatialOffsetWeightedMarker;
                    Pickupmessage_HeavyWeight.SetActive(true);
                    Debug.Log("Current Condition: InterventionConditionSpatialOffsetWeightedMarker");
                    heavyComp.enabled = true;
                    pickupHeavy.currentTransform = 5;
                    trajectoryInterventionSpatialOffsetWMScript.enabled = true;
                    break;

                case "j":
                    currentCondition = InterventionConditionDynamicSpatialOffsetWeighted;
                    Pickupmessage_HeavyWeight.SetActive(true);
                    Debug.Log("Current Condition: InterventionConditionDynamicSpatialOffsetWeighted");
                    heavyComp.enabled = true;
                    pickupHeavy.currentTransform = 6;
                    trajectoryInterventionDynamicSpatialOffsetWScript.enabled = true;
                    break;

                case "k":
                    currentCondition = InterventionConditionDynamicSpatialOffsetNonWeighted;
                    Pickupmessage_LightWeight.SetActive(true);
                    Debug.Log("Current Condition: InterventionConditionDynamicSpatialOffsetNonWeighted");
                    lightComp.enabled = true;
                    pickupLight.currentTransform = 3;
                    trajectoryInterventionDynamicSpatialOffsetNWScript.enabled = true;
                    break;

                case "l":
                    currentCondition = InterventionConditionDynamicSpatialOffsetWeightedMarker;
                    Pickupmessage_HeavyWeight.SetActive(true);
                    Debug.Log("Current Condition: InterventionConditionDynamicSpatialOffsetWeightedMarker");
                    heavyComp.enabled = true;
                    pickupHeavy.currentTransform = 7;
                    trajectoryInterventionDynamicSpatialOffsetWMScript.enabled = true;
                    break;

                case "m":
                    currentCondition = InterventionConditionWindNoiseWeighted;
                    Pickupmessage_HeavyWeight.SetActive(true);
                    Debug.Log("Current Condition: InterventionConditionWindNoiseWeighted");
                    heavyComp.enabled = true;
                    pickupHeavy.currentTransform = 8;
                    trajectoryInterventionWindNoiceWScript.enabled = true;
                    break;

                case "n":
                    currentCondition = InterventionConditionWindNoiseNonWeighted;
                    Pickupmessage_LightWeight.SetActive(true);
                    Debug.Log("Current Condition: InterventionConditionWindNoiseNonWeighted");
                    lightComp.enabled = true;
                    pickupLight.currentTransform = 4;
                    trajectoryInterventionWindNoiceNWScript.enabled = true;
                    break;

                case "o":
                    currentCondition = InterventionConditionWindNoiseWeightedMarker;
                    Pickupmessage_HeavyWeight.SetActive(true);
                    Debug.Log("Current Condition: InterventionConditionWindNoiseWeightedMarker");
                    heavyComp.enabled = true;
                    pickupHeavy.currentTransform = 9;
                    trajectoryInterventionWindNoiceWMScript.enabled = true;
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
            yield return new WaitForSeconds(ExperimentDuration);
            Debug.Log("path over");
            PathEnabled = false;

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
            videoPlayer.PlayVideo();
            BaselineConditionNoWeight.SetActive(true);
            Debug.Log("You can Rest");
            yield return new WaitUntil(() => restActive);
            BaselineConditionNoWeight.SetActive(false);
            count++;
            BallPicked = false;
            restActive = false;
            rest.SetActive(false);
            videoPlayer.PauseVideo();
            buttonPressed = false;
            UICamera.SetActive(false);
            ExpCamera.SetActive(true);
            trajectoryBaselineScript.enabled = false;
            trajectoryNonInterventionScript.enabled = false;
            trajectoryInterventionMidpointScript.enabled = false;
            trajectoryInterventionLagWScript.enabled = false;
            trajectoryInterventionLagNWScript.enabled = false;
            trajectoryInterventionSpatialOffsetWScript.enabled = false;
            trajectoryInterventionSpatialOffsetNWScript.enabled = false;
            trajectoryInterventionDynamicSpatialOffsetWScript.enabled = false;
            trajectoryInterventionDynamicSpatialOffsetNWScript.enabled = false;
            trajectoryInterventionWindNoiceWScript.enabled = false;
            trajectoryInterventionWindNoiceNWScript.enabled = false;
            trajectoryInterventionLagWMScript.enabled = false;
            trajectoryInterventionDynamicSpatialOffsetWMScript.enabled = false;
            trajectoryInterventionSpatialOffsetWMScript.enabled = false;
            trajectoryInterventionWindNoiceWMScript.enabled = false;
        }
        EndingFeedback.SetActive(true);
        yield return new WaitUntil(() => FeedbackendGiven);
        EndingFeedback.SetActive(false);
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
    public void HideMisconceptions()
    {
        Misconception.SetActive(false);
    }
}
