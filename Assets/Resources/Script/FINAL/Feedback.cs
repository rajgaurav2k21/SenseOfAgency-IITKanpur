using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    public GameObject WrongBelief;
    public GameObject Range;
    public Transform TargetTransform;
    public Transform ColliderTransform;
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
        if(other.tag== "Player")
        {
            Debug.Log("Wrong belief triggered by "+ other.name);
            WrongBelief.SetActive(true);
            StartCoroutine(DestroyAfterDelay(WrongBelief, 7.0f));
            Debug.Log("Misconception over");
        }
    }
     private IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
        Range.SetActive(false);
    }
}
