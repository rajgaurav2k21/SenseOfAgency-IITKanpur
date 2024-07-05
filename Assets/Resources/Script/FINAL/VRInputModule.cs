using UnityEngine;
using UnityEngine.EventSystems;
using TMPro; // if you're using TextMeshPro for Input Field

public class VRInputHandle : MonoBehaviour, IPointerClickHandler
{
    public TMP_InputField inputField; 

    void Start()
    {
        if (inputField == null)
        {
            inputField = GetComponent<TMP_InputField>();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
            inputField.ActivateInputField();
    }
}
