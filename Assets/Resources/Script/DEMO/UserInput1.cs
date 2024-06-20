using UnityEngine;
using System.Collections;

public class UserInput1 : MonoBehaviour
{
    private int response;
    public Animator animator;
    private AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        audioSource.Play();
        Debug.Log("You pressed the Green Button");
        animator.Play("Press");
        StartCoroutine(PlayIdleAnimationAfterDelay(1.5f));
        response = 1;

        GameObject rearCube = GameObject.Find("RearCube");
        ExperimentManager experimentManager = rearCube.GetComponentInChildren<ExperimentManager>();
        experimentManager.SetButtonPressed();
        float duration = experimentManager.GetPressDuration();

        GameObject data = GameObject.Find("Data");
        SaveData saveData = data.GetComponentInChildren<SaveData>();
        saveData.SaveToCSV(experimentManager.GetCurrentSize(), response, duration);
    }

    private IEnumerator PlayIdleAnimationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.Play("Reset");
    }
}
