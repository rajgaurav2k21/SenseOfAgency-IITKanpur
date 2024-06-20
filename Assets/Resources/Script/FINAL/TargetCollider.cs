using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollider : MonoBehaviour
{
    public TargetCallScript targetCallScript;
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
        targetCallScript.call=true;
        Debug.Log("called" + other.name);
        }
    }
}
