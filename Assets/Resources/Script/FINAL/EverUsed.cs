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
                PerformAction(true);
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                PerformAction(false);
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
