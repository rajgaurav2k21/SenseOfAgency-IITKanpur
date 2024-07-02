using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandColliderLag : MonoBehaviour
{
    public bool Weightlag=false;
    public Transform hand;
    public Transform Weight;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        Vector3 handPosition = hand.position;
        if(Weightlag)
        {
        Weight.position=handPosition;
        }
    }
}

 