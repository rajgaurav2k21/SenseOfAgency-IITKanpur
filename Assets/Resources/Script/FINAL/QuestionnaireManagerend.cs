using UnityEngine;
using TMPro;
using System.IO;
using System.Text;
using System.Collections;

public class QuestionnaireManagerend : MonoBehaviour
{
    public GameObject feedback;
    public ProjectManager_Block projectManager_block;
    public TMP_InputField inputField1;
    public TMP_InputField inputField2;

    public TMP_Text questionText1;
    public TMP_Text questionText2;

    public GameObject Panel;

    private int currentQuestionIndex = 0;
    private string filePath;

    private void Start()
    {
        Panel.SetActive(true);
        filePath = Application.dataPath + "/CSV/Experience.csv";
        Directory.CreateDirectory(Application.dataPath + "/CSV");
        ActivateCurrentInputField();
        if (!File.Exists(filePath))
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.WriteLine("Username,Question1,Question2");
            }
        }
        questionText1.text = "Question 1: How did you find the overall experience?";
        questionText2.text = "Question 2: What improvements would you suggest?";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (currentQuestionIndex < 2)
            {
                SaveCurrentAnswer();
                currentQuestionIndex++;
                if (currentQuestionIndex < 2)
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            feedback.SetActive(false);
            projectManager_block.buttonPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentQuestionIndex == 0)
            {
                currentQuestionIndex = 1;
            }
            else if (currentQuestionIndex == 1)
            {
                currentQuestionIndex = 0;
            }
            ActivateCurrentInputField();
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
        csvContent.AppendLine($"\"{projectManager_block.usernameInput.text}\",\"{inputField1.text}\",\"{inputField2.text}\"");
        using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8))
        {
            sw.WriteLine(csvContent.ToString().TrimEnd());
        }

        Debug.Log("All answers saved to CSV.");
        projectManager_block.FeedbackendGiven = true;
    }
    private IEnumerator ActivateInputFieldDelayed(TMP_InputField inputField)
    {
        yield return null;

        inputField.ActivateInputField();
        inputField.Select();
    }
}
