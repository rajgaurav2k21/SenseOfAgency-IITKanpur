using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetWeight : MonoBehaviour
{
    public GameObject weight1;
    public GameObject weight2;
    public GameObject weight3;
    public bool reset = true;

    void Update()
    {
        if (reset)
        {
            weight1.transform.position = new Vector3(0.476485252f, -0.00179994106f, 0.0182390213f);
            weight2.transform.position = new Vector3(0.230999947f, 0f, 0.0140000004f);
            weight3.transform.position = new Vector3(0f, 0f, 0.0189999994f);
            weight1.transform.rotation = Quaternion.Euler(0f, 359.78537f, 0f);
            weight2.transform.rotation = Quaternion.Euler(0f, 1.65688634f, 0f);
            weight3.transform.rotation = Quaternion.Euler(0f, 1.65688634f, 0f);

            reset = false;
        }
    }
}
