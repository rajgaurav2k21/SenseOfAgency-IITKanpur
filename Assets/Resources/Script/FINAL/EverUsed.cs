using UnityEngine;
using UnityEngine.UI;

public class EverUsed : MonoBehaviour
{
    public Toggle toggle;
    public int everUsed;
    public GameObject Check;
    public GameObject Yes;
    public GameObject No;
    public GameObject Text;
    public GameObject TextYes;
    public GameObject TextNo;
    public GameObject Dizzy;
    
    public GameObject Image;

    private bool actionPerformed; 

    void Start()
    {
        toggle.isOn = false;
        everUsed = 0;
        Yes.SetActive(false);
        No.SetActive(false);
        Check.SetActive(false);
        Text.SetActive(true);
        Dizzy.SetActive(false);
        actionPerformed = false;
        Image.SetActive(true);
    }

    void Update()
    {
        if (!actionPerformed) 
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                Debug.Log("Y pressed");
                PerformAction(true);
                TextYes.SetActive(true);
                TextNo.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                Debug.Log("N pressed");
                PerformAction(false);
                TextNo.SetActive(true);
                TextYes.SetActive(false);
            }
        }
    }

    void PerformAction(bool state)
    {
        SetToggleState(state);

        if (state) 
        {
            Yes.SetActive(true);
            No.SetActive(false);
            Dizzy.SetActive(true);
            Image.SetActive(false);
        }
        else 
        {
            No.SetActive(true);
            Yes.SetActive(false);
            Dizzy.SetActive(true);
        }

        Check.SetActive(true);
        Text.SetActive(false);
        actionPerformed = true; 
    }

    void SetToggleState(bool state)
    {
        toggle.isOn = state;
        everUsed = state ? 1 : 0;
        Debug.Log("EverUsed Value: " + everUsed);
    }
}
