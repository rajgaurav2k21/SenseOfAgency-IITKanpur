using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    public GameObject WrongBelief;
    public Transform TargetTransform;
    public Transform ColliderTransform;
    public ProjectManager_Block projectManager_Block;
    void Start()
    {
        WrongBelief.SetActive(false);
        
    }
    void Update()
    {
        ColliderTransform.transform.position = TargetTransform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag== "Weight")
        {
            Debug.Log("Wrong belief triggered by "+ other.name);
            WrongBelief.SetActive(true);
            StartCoroutine(Delay(7.0f));
        }
    }
     private IEnumerator Delay(float delay)
    {
        Debug.Log("DelayCoroutine called");
        yield return new WaitForSeconds(delay);
        WrongBelief.SetActive(false);
        projectManager_Block.HideMisconceptions();
    }
}
