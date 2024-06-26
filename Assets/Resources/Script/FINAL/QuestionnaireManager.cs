using UnityEngine;
using TMPro;
using System.IO;
using System.Text;
using System.Collections.Generic;

public class QuestionnaireManager : MonoBehaviour
{
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
        filePathResponse = Application.dataPath + "/CSV/response.csv";

        questionText1.text = "Question 1: How responsive was the virtual environment to the actions you initiated?";
        questionText2.text = "Question 2: How much ownership did you feel?";
        questionText3.text = "Question 3: How confident did you feel when placing objects in the grid?";
        questionText4.text = "Question 4: How much lag do you think your actions had in VR?";
        questionText5.text = "Question 5: How much leading do you think your actions had compared to your proprioception in VR?";

        Panel2.SetActive(false);

        inputFields = new TMP_InputField[] { inputField1, inputField2, inputField3, inputField4, inputField5 };
        inputFields[currentQuestionIndex].ActivateInputField();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SaveCurrentAnswer();
            currentQuestionIndex++;
            if (currentQuestionIndex < inputFields.Length)
            {
                inputFields[currentQuestionIndex].ActivateInputField();
            }
            else
            {
                SaveAllAnswers();
            }
        }
    }

    private void SaveCurrentAnswer()
    {
        string answer = inputFields[currentQuestionIndex].text;

        if (currentQuestionIndex == 2)
        {
            Panel2.SetActive(true);
            Panel1.SetActive(false);
        }
    }

    private void SaveAllAnswers()
    {
        List<string> existingLines = new List<string>();
        if (File.Exists(filePathResponse))
        {
            using (StreamReader sr = new StreamReader(filePathResponse))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    existingLines.Add(line);
                }
            }
        }

        StringBuilder csvContent = new StringBuilder();

        for (int i = 0; i < inputFields.Length; i++)
        {
            csvContent.AppendLine($"\"{inputFields[i].transform.parent.GetComponentInChildren<TMP_Text>().text}\",\"{inputFields[i].text}\"");
        }

        using (StreamWriter sw = new StreamWriter(filePathResponse, true, Encoding.UTF8))
        {
            foreach (string line in existingLines)
            {
                sw.WriteLine(line);
            }
            sw.Write(csvContent.ToString().TrimEnd());
        }
    }
}
