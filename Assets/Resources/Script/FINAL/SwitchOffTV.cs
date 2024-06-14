using UnityEngine;

public class SwitchOffTV : MonoBehaviour
{
    public GameObject led;
    private bool isTVOn = true;

    void Start()
    {
        Debug.Log("hemlo");
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
            led.SetActive(true);
        }
        else
        {
            led.SetActive(false);
        }
    }
}
