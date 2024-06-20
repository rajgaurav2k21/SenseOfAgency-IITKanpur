using System.Collections;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Animator animator;

    public void OnTriggerEnter(Collider other)
    {
            Debug.Log("gate opening");
            animator.Play("gate");
            StartCoroutine(CloseGateAfterDelay());
    }

    IEnumerator CloseGateAfterDelay()
    {
        yield return new WaitForSeconds(7f);
        animator.Play("gateclose");
    }
}
