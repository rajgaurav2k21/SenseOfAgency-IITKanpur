using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feedback : MonoBehaviour
{
    public GameObject fb;
    public GameObject Range;
    public Transform TargetTransform;
    public Transform ColliderTransform;
    void Start()
    {
        fb.SetActive(false);
        
    }
    void Update()
    {
        ColliderTransform.transform.position = TargetTransform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
            fb.SetActive(true);
            StartCoroutine(DestroyAfterDelay(fb, 5.0f));
    }
     private IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
        Range.SetActive(false);
    }
}
