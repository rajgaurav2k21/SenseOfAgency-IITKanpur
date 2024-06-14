using UnityEngine;

public class SwitchOffTV : MonoBehaviour
{
    public GameObject display;
    public GameObject led;
    private bool isTVOn = true;

    void Start()
    {
        Debug.Log("hemlo");
        display.SetActive(true);
        led.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entering");

            ToggleTV();

    }

    private void ToggleTV()
    {

        isTVOn = !isTVOn;

        if (isTVOn)
        {
            display.SetActive(true);
            led.SetActive(true);
            
        }
        else
        {
            display.SetActive(false);
            led.SetActive(false);
        }
    }
}
