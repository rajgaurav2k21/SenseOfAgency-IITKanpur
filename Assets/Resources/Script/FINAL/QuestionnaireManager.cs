using UnityEngine;
using TMPro;
using System.IO;
using System.Text;
using System.Collections;

public class QuestionnaireManager : MonoBehaviour
{
    public GameObject feedback;
    public ProjectManager_Block projectManager_block;
    public TMP_InputField inputField1;
    public TMP_InputField inputField2;

    public TMP_Text questionText1;
    public TMP_Text questionText2;

    public GameObject Panel2;

    private int currentQuestionIndex = 0;
    private string filePathResponse;

    private void Start()
    {
        ResetQuestionnaire();
        Panel2.SetActive(true);

        // Set the file path for saving responses
        filePathResponse = Application.persistentDataPath + "/UserResponse.csv";

        // Create directory if it doesn't exist
        Directory.CreateDirectory(Application.persistentDataPath);

        // Set initial UI state
        ActivateCurrentInputField();

        // Set question texts
        questionText1.text = "Question 1: How responsive was the virtual environment to the actions you initiated?";
        questionText2.text = "Question 2: How much ownership did you feel?";
    }

    private void Update()
    {
        // Check for Enter key press to move to the next question
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (currentQuestionIndex < 2) // Adjusted to the number of input fields
            {
                SaveCurrentAnswer();
                currentQuestionIndex++;
                if (currentQuestionIndex < 2) // Adjusted to the number of input fields
                {
                    ActivateCurrentInputField();
                }
                else
                {
                    SaveAllAnswers();
                    currentQuestionIndex = 0;
                    
                    Debug.Log("All answers saved to CSV.");
                }
            }
        }

        // Check for Space key press to reset questionnaire
        if (Input.GetKeyDown(KeyCode.Space))
        {
            feedback.SetActive(false);
            projectManager_block.buttonPressed = true;
        }
    }

    private void ActivateCurrentInputField()
    {
        // Deactivate all input fields first
        inputField1.DeactivateInputField();
        inputField2.DeactivateInputField();

        // Activate the current input field
        if (currentQuestionIndex == 0)
        {
            inputField1.ActivateInputField();
            inputField1.Select();
        }
        else if (currentQuestionIndex == 1)
        {
            inputField2.ActivateInputField();
            inputField2.Select();
        }
    }

    private void SaveCurrentAnswer()
    {
        // Save current answer based on current question index
        if (currentQuestionIndex == 0)
        {
            string answer = inputField1.text;
            Debug.Log($"Saved answer for question {currentQuestionIndex + 1}: {answer}");
        }
        else if (currentQuestionIndex == 1)
        {
            string answer = inputField2.text;
            Debug.Log($"Saved answer for question {currentQuestionIndex + 1}: {answer}");
        }
    }

    private void SaveAllAnswers()
    {
        // Prepare CSV content
        StringBuilder csvContent = new StringBuilder();
        csvContent.AppendLine("Username,Condition,Question1,Question2");

        // Append user responses
        csvContent.AppendLine($"\"{projectManager_block.usernameInput.text}\",\"{projectManager_block.GetcurrentCondition()}\"," +
            $"\"{inputField1.text}\",\"{inputField2.text}\"");

        // Write to CSV file
        using (StreamWriter sw = new StreamWriter(filePathResponse, true, Encoding.UTF8))
        {
            sw.WriteLine(csvContent.ToString().TrimEnd());
        }

        Debug.Log("All answers saved to CSV.");
    }

    public void ResetQuestionnaire()
    {
        currentQuestionIndex = 0;
        Debug.Log("ResetQuestionnaire method called.");

        // Clear text and deactivate both input fields
        inputField1.text = "";
        inputField1.DeactivateInputField();

        inputField2.text = "";
        inputField2.DeactivateInputField();

        // Activate and select the first input field after a frame delay
        StartCoroutine(ActivateInputFieldDelayed(inputField1));

        Debug.Log("Questionnaire has been reset.");
    }

    private IEnumerator ActivateInputFieldDelayed(TMP_InputField inputField)
    {
        yield return null; // Wait for the end of the frame

        inputField.ActivateInputField();
        inputField.Select();
    }
}
