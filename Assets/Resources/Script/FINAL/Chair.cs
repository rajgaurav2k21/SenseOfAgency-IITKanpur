using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    public Transform VirtualChair;
    public Transform Body;
    public float x = 0.08f;
    public float y = -2.18f;
    public float z = -2.56f;

    void Update()
    {
        Vector3 BodyPosition = new Vector3(z, y, x) +Body.position;
        VirtualChair.position = BodyPosition;

        Vector3 BodyRotationEuler = Body.rotation.eulerAngles;
        //BodyRotationEuler.y += y;
        //Quaternion BodyRotation = Quaternion.Euler(BodyRotationEuler);
        //VirtualChair.rotation = BodyRotation;
    }
}

