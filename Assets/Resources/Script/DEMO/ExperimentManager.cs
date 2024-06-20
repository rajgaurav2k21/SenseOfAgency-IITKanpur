using System.Collections;
using UnityEngine;
using TMPro;

public class ExperimentManager : MonoBehaviour
{
    public GameObject instructionsPanel;
    public GameObject COMPLETE;
    public GameObject userInteractionPanel;
    public Animator animator;
    private float[] sizes = { 0.0143f, 0.0145f, 0.0147f, 0.0149f, 0.0151f, 0.0153f, 0.0154f, 0.01545f, 0.01548f, 0.0155f, 0.01552f, 0.01555f, 0.01557f, 0.0156f, 0.01565f, 0.0157f, 0.0158f, 0.0159f, 0.0160f, 0.0161f, 0.0163f, 0.0165f, 0.0167f, 0.0169f, 0.0171f };
    private float currentSize;
    public TMP_Text timer;
    private bool buttonPressed = false; // Flag to indicate if a button is pressed
    private float buttonActiveTime; // Time when the button becomes active
    private float pressDuration; // Time it took to press the button

    void Start()
    {
        instructionsPanel.SetActive(false);
        COMPLETE.SetActive(false);
        StartCoroutine(Resize());
    }

    IEnumerator Resize()
    {
        yield return StartCoroutine(StartCountdown(40));

        System.Random random = new System.Random();
        int count = 0;
        for (int i = 0; i < 25; i++)
        {
            animator.Play("cube", 0, 0);
            yield return new WaitForSeconds(3f);
            currentSize = sizes[random.Next(sizes.Length)];
            GameObject cube = GameObject.Find("RearCube");
            cube.transform.localScale = new Vector3(currentSize, currentSize, currentSize);

            instructionsPanel.SetActive(true);
            buttonActiveTime = Time.time; // Record the time when the button becomes active

            // Wait indefinitely until a button is pressed
            yield return new WaitUntil(() => buttonPressed);

            pressDuration = Time.time - buttonActiveTime; // Calculate the duration
            Debug.Log("Button press duration: " + pressDuration);

            instructionsPanel.SetActive(false);
            count++;
            buttonPressed = false; // Reset the flag
        }

        Debug.Log("Experiment Complete.");
        COMPLETE.SetActive(true);
    }

    IEnumerator StartCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            timer.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        timer.text = "Ready";
        timer.gameObject.SetActive(false);
        userInteractionPanel.SetActive(false);
    }

    public float GetCurrentSize()
    {
        return currentSize;
    }

    // Method to set the button pressed flag and record the duration
    public void SetButtonPressed()
    {
        buttonPressed = true;
    }

    public float GetPressDuration()
    {
        return pressDuration;
    }
}
