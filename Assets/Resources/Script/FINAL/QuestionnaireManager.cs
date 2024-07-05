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
    public GameObject TextSpace;

    private int currentQuestionIndex = 0;
    private string filePath;

    private void Start()
    {
        ResetQuestionnaire();
        Panel2.SetActive(true);
        TextSpace.SetActive(false);

        filePath = Application.dataPath + "/CSV/UserData.csv";
        Directory.CreateDirectory(Application.dataPath + "/CSV");
        ActivateCurrentInputField();
        if (!File.Exists(filePath))
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.WriteLine("Username,Condition,Question1,Question2");
            }
        }
        questionText1.text = "Question 1: How responsive was the virtual environment to the actions you initiated?";
        questionText2.text = "Question 2: How much ownership did you feel?";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SaveCurrentAnswer();
            currentQuestionIndex++;
            if (currentQuestionIndex < 2)
            {
                ActivateCurrentInputField();
            }
            else if (currentQuestionIndex == 2)
            {
                SaveAllAnswers();
                currentQuestionIndex = 0;
                TextSpace.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            feedback.SetActive(false);
            projectManager_block.buttonPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SwitchToPreviousInputField();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SwitchToNextInputField();
        }
    }

    private void ActivateCurrentInputField()
    {
        inputField1.DeactivateInputField();
        inputField2.DeactivateInputField();

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
        StringBuilder csvContent = new StringBuilder();

        csvContent.AppendLine($"\"{projectManager_block.usernameInput.text}\",\"{projectManager_block.GetcurrentCondition()}\"," +
            $"\"{inputField1.text}\",\"{inputField2.text}\"");

        using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8))
        {
            sw.WriteLine(csvContent.ToString().TrimEnd());
        }
    }

    public void ResetQuestionnaire()
    {
        currentQuestionIndex = 0;

        inputField1.text = "";
        inputField1.DeactivateInputField();

        inputField2.text = "";
        inputField2.DeactivateInputField();

        StartCoroutine(ActivateInputFieldDelayed(inputField1));
    }

    private IEnumerator ActivateInputFieldDelayed(TMP_InputField inputField)
    {
        yield return null;

        inputField.ActivateInputField();
        inputField.Select();
    }

    private void SwitchToPreviousInputField()
    {
        currentQuestionIndex--;
        if (currentQuestionIndex < 0)
        {
            currentQuestionIndex = 0;
        }
        ActivateCurrentInputField();
    }

    private void SwitchToNextInputField()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex > 1)
        {
            currentQuestionIndex = 1;
        }
        ActivateCurrentInputField();
    }
}
