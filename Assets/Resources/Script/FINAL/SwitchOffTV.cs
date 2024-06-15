using UnityEngine;

public class SwitchOffTV : MonoBehaviour
{
    public GameObject display;
    public GameObject led;
    private bool isTVOn = true;

    void Start()
    {
        display.SetActive(true);
        led.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("TV is getting focus by"+ other.name);
            ToggleTV();
        }
        
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
