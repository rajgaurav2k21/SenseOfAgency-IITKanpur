using UnityEngine;
using TMPro;

public class name : MonoBehaviour
{
    public TMP_Text tmpText;
    public ProjectManager_Block projectManager_Block;

    void Start()
    {
        string username = projectManager_Block.usernameInput.text;
        tmpText.text = username+",";
    }
    public void SetText(string newText)
    {
        tmpText.text = newText;
    }
}
