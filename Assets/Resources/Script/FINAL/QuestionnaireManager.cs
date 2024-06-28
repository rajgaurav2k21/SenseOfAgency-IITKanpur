using UnityEngine;
using TMPro;
using System.IO;
using System.Text;

public class QuestionnaireManager : MonoBehaviour
{
    public GameObject feedback;
    public bool ResetFB = false;
    public ProjectManager_Block projectManager_block;
    public TMP_InputField inputField1;
    public TMP_InputField inputField2;
    public TMP_InputField inputField3;
    public TMP_InputField inputField4;
    public TMP_InputField inputField5;

    public TMP_Text questionText1;
    public TMP_Text questionText2;
    public TMP_Text questionText3;
    public TMP_Text questionText4;
    public TMP_Text questionText5;

    public GameObject Panel1;
    public GameObject Panel2;

    private TMP_InputField[] inputFields;
    private int currentQuestionIndex = 0;
    private string filePathResponse;

    private void Start()
    {
        // Set the file path for saving responses
        filePathResponse = Application.persistentDataPath + "/UserResponse.csv";

        // Create directory if it doesn't exist
        Directory.CreateDirectory(Application.persistentDataPath);

        // Initialize inputFields array
        inputFields = new TMP_InputField[] { inputField1, inputField2, inputField3, inputField4, inputField5 };

        // Set initial UI state
        Panel2.SetActive(false);
        ActivateCurrentInputField();

        // Set question texts
        questionText1.text = "Question 1: How responsive was the virtual environment to the actions you initiated?";
        questionText2.text = "Question 2: How much ownership did you feel?";
        questionText3.text = "Question 3: How confident did you feel when placing objects in the grid?";
        questionText4.text = "Question 4: How much lag do you think your actions had in VR?";
        questionText5.text = "Question 5: How much leading do you think your actions had compared to your proprioception in VR?";
    }

    private void Update()
    {
        // Check for Enter key press to move to the next question
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (currentQuestionIndex < inputFields.Length)
            {
                SaveCurrentAnswer();
                currentQuestionIndex++;
                if (currentQuestionIndex < inputFields.Length)
                {
                    ActivateCurrentInputField();
                }
                else
                {
                    SaveAllAnswers();
                    ResetQuestionnaire();
                }
            }
        }
        //projectManager_block.RestActiveBool=false;
        // Check for Space key press to hide feedback panel
        if (Input.GetKeyDown(KeyCode.Space))
        {
            feedback.SetActive(false);
            projectManager_block.buttonPressed = true;
        }
    }

    private void ActivateCurrentInputField()
    {
        // Deactivate all input fields first
        foreach (var inputField in inputFields)
        {
            inputField.DeactivateInputField();
        }

        // Activate the current input field
        if (currentQuestionIndex < inputFields.Length)
        {
            inputFields[currentQuestionIndex].ActivateInputField();
            inputFields[currentQuestionIndex].Select();
        }
    }

    private void SaveCurrentAnswer()
    {
        // Ensure currentQuestionIndex is within bounds
        if (currentQuestionIndex < inputFields.Length)
        {
            string answer = inputFields[currentQuestionIndex].text;
            Debug.Log($"Saved answer for question {currentQuestionIndex + 1}: {answer}");

            // Switch panels after saving answer for question 3
            if (currentQuestionIndex == 2)
            {
                Panel2.SetActive(true);
                Panel1.SetActive(false);
            }
        }
    }

    private void SaveAllAnswers()
    {
        // Prepare CSV content
        StringBuilder csvContent = new StringBuilder();
        csvContent.AppendLine("Username,Condition,Question1,Question2,Question3,Question4,Question5");

        // Append user responses
        csvContent.AppendLine($"\"{projectManager_block.usernameInput.text}\",\"{projectManager_block.GetcurrentCondition()}\"," +
            $"\"{inputFields[0].text}\",\"{inputFields[1].text}\",\"{inputFields[2].text}\",\"{inputFields[3].text}\",\"{inputFields[4].text}\"");

        // Write to CSV file
        using (StreamWriter sw = new StreamWriter(filePathResponse, true, Encoding.UTF8))
        {
            sw.WriteLine(csvContent.ToString().TrimEnd());
        }

        Debug.Log("All answers saved to CSV.");
    }

    private void ResetQuestionnaire()
    {
        if(ResetFB)
        {
        // Reset currentQuestionIndex and clear input fields
        currentQuestionIndex = 0;
        foreach (var inputField in inputFields)
        {
            inputField.text = "";
            inputField.DeactivateInputField();
        }

        // Switch panels back to the initial state
        Panel1.SetActive(true);
        Panel2.SetActive(false);

        // Activate the first input field
        ActivateCurrentInputField();

        Debug.Log("Questionnaire has been reset.");
        }
    }
}
