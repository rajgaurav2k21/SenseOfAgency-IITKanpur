using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class Dizzy : MonoBehaviour
{
    public Toggle toggle;
    private int DizzyState;
    public GameObject Check;
    public GameObject Yes;
    public GameObject No;
    public GameObject Text;
    public GameObject Continue;
    public EverUsed everUsed;
    private bool pressed = false;
    public ProjectManager_Block projectManager_Block;
    private string filePath;

    void Start()
    {
        toggle.isOn = false;
        Yes.SetActive(false);
        No.SetActive(false);
        Check.SetActive(false);
        Text.SetActive(true);
        Continue.SetActive(false);
        filePath = Application.dataPath + "/CSV/PastHistory_data.csv";
        Directory.CreateDirectory(Application.dataPath + "/CSV");
        if (!File.Exists(filePath))
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.WriteLine("Username,DizzyState,EverUsed");
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SetToggleState(true);
            ShowYesNoOptions(true);
            pressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            SetToggleState(false);
            ShowYesNoOptions(false);
            pressed = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && pressed)
        {
            ContinueGame();
        }
    }

    void SetToggleState(bool state)
    {
        toggle.isOn = state;
        DizzyState = state ? 1 : 0;
    }

    void ShowYesNoOptions(bool showYes)
    {
        if (showYes)
        {
            Yes.SetActive(true);
            No.SetActive(false);
        }
        else
        {
            No.SetActive(true);
            Yes.SetActive(false);
        }
        
        Check.SetActive(true);
        Text.SetActive(false);
        Continue.SetActive(true);
    }

    void ContinueGame()
    {
        projectManager_Block.Startboolean = true;
        projectManager_Block.ExperiencePicked=true;
        string username = projectManager_Block.usernameInput.text; 
        int everUsedState = everUsed.everUsed;
        SaveToCSV(username, DizzyState, everUsedState);
        
    }
    void SaveToCSV(string username, int dizzyState, int everUsed)
    {
        using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8))
        {
            string data = string.Format("{0},{1},{2}", username, dizzyState, everUsed);
            sw.WriteLine(data);
        }
        Debug.Log("Data saved - Username: " + username + ", DizzyState: " + dizzyState + ", EverUsed: " + everUsed);
    }
}
