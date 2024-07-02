using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : MonoBehaviour
{
    public ProjectManager_Block projectManager_Block;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Exteriment Triggerered by Pressing Space");
            projectManager_Block.StartExp();
        }
    }
}
