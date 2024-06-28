using System.Collections;
using UnityEngine;

public class RestTest : MonoBehaviour
{
    public bool restActive = false;

    void Start()
    {
        StartCoroutine(TestRestActive());
    }

    IEnumerator TestRestActive()
    {
        yield return new WaitUntil(() => restActive);

        Debug.Log("restActive is now true! Rest triggered.");
    }
}
