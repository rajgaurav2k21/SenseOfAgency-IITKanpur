using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
public class ShowKeyboard_feedback : MonoBehaviour
{
    public TMP_InputField inputfield;
    //public float distance= 0.5f;
    //public float verticalOffset=-0.5f;
    //public Transform positionSource;
     void Start()
    {
        inputfield.onSelect.AddListener(x=> OpenKeyboard());
    }

    // Update is called once per frame
    public void OpenKeyboard()
    {
        NonNativeKeyboard.Instance.InputField=inputfield;
        NonNativeKeyboard.Instance.PresentKeyboard(inputfield.text);
        //Vector3 direction =positionSource.forward;
        //direction.y=0;
        //direction.Normalize();
        //Vector3 targetposition = positionSource.position+ direction * distance + Vector3.up *verticalOffset;
        //NonNativeKeyboard.Instance.RepositionKeyboard(targetposition);
        SetCaretColorAlpha(1);
        NonNativeKeyboard.Instance.OnClosed += Instance_OnClosed;
    }
    private void Instance_OnClosed(object sender,System.EventArgs e)
    {
        SetCaretColorAlpha(0);
        NonNativeKeyboard.Instance.OnClosed -= Instance_OnClosed;
    }
    public void SetCaretColorAlpha(float value)
    {
        inputfield.customCaretColor =true;
        Color caretColor =inputfield.caretColor;
        caretColor.a =value;
        inputfield.caretColor =caretColor;
    }
}
