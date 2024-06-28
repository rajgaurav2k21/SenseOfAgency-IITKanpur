using UnityEngine;
using TMPro;
using System.IO;
using System.Text;

public class QuestionnaireManagerend : MonoBehaviour
{
    public ProjectManager_Block projectManager_block;
    public TMP_InputField inputField1;
    public TMP_InputField inputField2;
    public TMP_Text questionText1;
    public TMP_Text questionText2;

    private string filePath;

    private void Start()
    {
        filePath = Path.Combine(Application.dataPath, "CSV", "UserExperience.csv");

        questionText1.text = "Question 1: How did you find the overall experience?";
        questionText2.text = "Question 2: What improvements would you suggest?";
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
        if (projectManager_block == null)
        {
            Debug.LogError("ProjectManager_Block is not assigned.");
            return;
        }

        string answer1 = inputField1.text;
        string answer2 = inputField2.text;

        StringBuilder csvContent = new StringBuilder();
        csvContent.AppendLine("Username,Question1,Question2");
        csvContent.AppendLine($"\"{projectManager_block.usernameInput.text}\",\"{answer1}\",\"{answer2}\"");

        try
        {
            File.AppendAllText(filePath, csvContent.ToString());
            Debug.Log("All answers saved to CSV.");

            projectManager_block.FeedbackendGiven = true;
            inputField1.text = string.Empty;
            inputField2.text = string.Empty;

            // Activate the next input field if applicable
            inputField1.ActivateInputField();
        }
        catch (IOException e)
        {
            Debug.LogError($"Failed to save answers: {e.Message}");
        }
    }
}
