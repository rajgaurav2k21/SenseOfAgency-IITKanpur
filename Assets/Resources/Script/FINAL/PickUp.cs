using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PickUp : MonoBehaviour
{
    public Transform[] positionsMarker;
    private Transform positionMarker;

    private bool isAttached = false;
    private Transform palmTransform;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("HandColliderLag");
            palmTransform = other.transform;
            this.transform.SetParent(palmTransform);
            UpdateCubePositionAndRotation();
            isAttached = true;
            Debug.Log("Weight is Up");
            Debug.Log(" Weight Disturbed by" + other.name);
            GameObject experimentManager_block = GameObject.Find("ExperimentManager_Block");
            ProjectManager_Block projectManager_block = experimentManager_block.GetComponent<ProjectManager_Block>();
            projectManager_block.BallPicked = true;
            projectManager_block.Pickupmessage_LightWeight.SetActive(false);
            projectManager_block.Pickupmessage_HeavyWeight.SetActive(false);
            projectManager_block.lightComp.enabled = false;
            projectManager_block.heavyComp.enabled = false;


        }
    }

    void Update()
    {
        if (isAttached && palmTransform != null)
        {
            UpdateCubePositionAndRotation();
        }
    }
    void UpdateCubePositionAndRotation()
    {
        if (palmTransform == null || positionMarker == null)
            return;
        Vector3 localPosition = palmTransform.InverseTransformPoint(positionMarker.position);
        Quaternion localRotation = Quaternion.Inverse(palmTransform.rotation) * positionMarker.rotation;
        this.transform.localPosition = localPosition;
        this.transform.localRotation = localRotation;
    }

    public void Detach()
    {
        this.transform.SetParent(null);
        isAttached = false;
    }
}
