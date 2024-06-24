using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public Slider slider;
    public float increment = 1f;
    public GetFeedback_Block getFeedback_Block; 

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            IncreaseSlider();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            DecreaseSlider();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
           getFeedback_Block.Feedback();
        }
    }

    void IncreaseSlider()
    {
        if (slider != null)
        {
            slider.value += increment * Time.deltaTime;
            if (slider.value > slider.maxValue)
            {
                slider.value = slider.maxValue;
            }
        }
    }

    void DecreaseSlider()
    {
        if (slider != null)
        {
            slider.value -= increment * Time.deltaTime;
            if (slider.value < slider.minValue)
            {
                slider.value = slider.minValue;
            }
        }
    }
}