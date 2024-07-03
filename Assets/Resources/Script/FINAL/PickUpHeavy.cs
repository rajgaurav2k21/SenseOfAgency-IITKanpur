using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHeavy : MonoBehaviour
{
    public Transform[] positionsMarkerArray;
    private Transform positionMarker;
    public int currentTransform;
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
            Debug.Log("Weight Disturbed by " + other.name);
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
        switch(currentTransform)
        {
            case 0:
                positionMarker = positionsMarkerArray[0];
                Debug.Log("PositionMarker 0 is Activated");
                break;
            case 1:
                positionMarker = positionsMarkerArray[1];
                Debug.Log("PositionMarker 1 is Activated");
                break;
            case 2:
                positionMarker = positionsMarkerArray[2];
                Debug.Log("PositionMarker 2 is Activated");
                break;
            case 3:
                positionMarker = positionsMarkerArray[3];
                Debug.Log("PositionMarker 3 is Activated");
                break;
            case 4:
                positionMarker = positionsMarkerArray[4];
                Debug.Log("PositionMarker 4 is Activated");
                break;
            case 5:
                positionMarker = positionsMarkerArray[5];
                Debug.Log("PositionMarker 5 is Activated");
                break;
        }
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
