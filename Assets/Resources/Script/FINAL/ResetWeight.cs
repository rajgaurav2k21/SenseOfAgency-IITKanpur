using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetWeight : MonoBehaviour
{
    public GameObject weight1;
    public GameObject weight2;
    public bool reset = false;

    void Update()
    {
        if (reset)
        {
            // Reset positions
            weight1.transform.position = new Vector3(0.282000005f, 0.00999999978f, -0.00300000003f);
            weight2.transform.position = new Vector3(0.0790000036f, 0.00999999978f, -0.0140000004f);

            // Reset rotations
            weight1.transform.rotation = Quaternion.Euler(0, 177.709122f, 0);
            weight2.transform.rotation = Quaternion.Euler(0, 177.709122f, 0);

            // Set reset to false to prevent continuous resetting
            reset = false;

            // Optional: Log to confirm reset has been completed
            Debug.Log("Weights have been reset.");
        }
    }
}
