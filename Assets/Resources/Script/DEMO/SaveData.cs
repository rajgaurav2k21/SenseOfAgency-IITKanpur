using System.IO;
using System.Text;
using UnityEngine;
using TMPro;

[System.Serializable]
public class DataToSave
{
    public float CurrentSize;
    public int Response;
    public float Duration;
}

public class SaveData : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public DataToSave Dts;
    private string filePath;
    public TMP_Text loading;
    public GameObject keyboard;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Awake()
    {
        loading.gameObject.SetActive(false);
        filePath = Application.dataPath + "/CSV/data.csv";
        Directory.CreateDirectory(Application.dataPath + "/CSV");
        if (!File.Exists(filePath))
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.WriteLine("Username,CurrentSize,Response,Duration");
            }
        }
    }

    public void SetUsername()
    {
        string username = usernameInput.text;
        usernameInput.gameObject.SetActive(false);
        loading.gameObject.SetActive(true);
        keyboard.gameObject.SetActive(false);
        audioSource.Play();
    }

    public void SaveToCSV(float currentSize, int response, float duration)
    {
        string username = usernameInput.text;
        using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8))
        {
            string data = string.Format("{0},{1},{2},{3}", username, currentSize, response, duration);
            sw.WriteLine(data);
        }
        Debug.Log("Username: " + username + ", CurrentSize: " + currentSize + ", Response: " + response + ", Duration: " + duration);
    }
}
