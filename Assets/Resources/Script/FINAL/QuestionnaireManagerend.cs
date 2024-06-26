using UnityEngine;
using TMPro;
using System.IO;
using System.Text;

public class QuestionnaireManagerend : MonoBehaviour
{
    public TMP_InputField inputField1;
    public TMP_InputField inputField2;
    public GameObject EndFeedback;
    public TMP_Text questionText1;
    public TMP_Text questionText2;

    private string filePath;

    private void Start()
    {
        filePath = Application.dataPath + "/CSV/response.csv";

        questionText1.text = "Question 1: How did you find the overall experience?";
        questionText2.text = "Question 2: What improvements would you suggest?";
        EndFeedback.SetActive(true);
        inputField1.ActivateInputField();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SaveAnswers();
        }
    }

    private void SaveAnswers()
    {
        string answer1 = inputField1.text;
        string answer2 = inputField2.text;

        StringBuilder csvContent = new StringBuilder();
        csvContent.AppendLine($"\"{questionText1.text}\",\"{answer1}\"");
        csvContent.AppendLine($"\"{questionText2.text}\",\"{answer2}\"");

        File.AppendAllText(filePath, csvContent.ToString());

        Debug.Log("Answers appended to " + filePath);
        GameObject experimentManager_block = GameObject.Find("ExperimentManager_Block");
        ProjectManager_Block projectManager_block = experimentManager_block.GetComponent<ProjectManager_Block>();
        projectManager_block.FeedbackendGiven=true; 

    }
}
