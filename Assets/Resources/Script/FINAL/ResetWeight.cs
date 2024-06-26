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
            weight1.transform.position = new Vector3(0.282000005f,0.00999999978f,-0.00300000003f);
            weight2.transform.position = new Vector3(0.0790000036f,0.00999999978f,-0.0140000004f);
            weight1.transform.rotation = Quaternion.Euler(0,177.709122f,0);
            weight2.transform.rotation = Quaternion.Euler(0,177.709122f,0);
            reset = false;
        }
    }
}
