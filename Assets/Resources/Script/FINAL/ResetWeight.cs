using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetWeight : MonoBehaviour
{
    public GameObject weight1;
    public GameObject weight2;
    public GameObject weight1ResetPoint;
    public GameObject weight2ResetPoint;
    public bool reset = false;

    void Update()
    {
        if (reset)
        {
            weight1.transform.position = weight1ResetPoint.transform.position;
            weight2.transform.position = weight2ResetPoint.transform.position;

            weight1.transform.rotation = weight1ResetPoint.transform.rotation;
            weight2.transform.rotation = weight2ResetPoint.transform.rotation;

            reset = false;

            Debug.Log("Weights have been reset.");
        }
    }
}
