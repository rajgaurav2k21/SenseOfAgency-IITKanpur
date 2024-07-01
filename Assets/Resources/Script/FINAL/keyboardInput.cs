using UnityEngine;
using TMPro;
using System.Collections;

public class KeyboardInput : MonoBehaviour
{
    public TMP_InputField userTMPInputField;
    public keyboardAge ageScript;

    private void Start()
    {
        userTMPInputField.text = "";
        userTMPInputField.ActivateInputField();
        ageScript.enabled = false;
    }

    private void Update()
    {
        if (Input.anyKeyDown && !userTMPInputField.isFocused && !ageScript.enabled)
        {
            userTMPInputField.ActivateInputField();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(ActivateAgeInputFieldWithDelay());
        }
    }

    private IEnumerator ActivateAgeInputFieldWithDelay()
    {
        userTMPInputField.DeactivateInputField();
        yield return new WaitForEndOfFrame();  // Wait for the end of the frame to ensure the input field is properly deactivated
        ageScript.enabled = true;
        ageScript.ActivateAgeInputField();
    }
}
