using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class TrajectoryInterventionMidpoint : MonoBehaviour
{
    public Transform Userball;
    private List<Vector3> trajectoryPoints;
    private string filePath;
    private string username;

    void Start()
    {
        GameObject experimentManager_block = GameObject.Find("ExperimentManager_Block");
        if (experimentManager_block != null)
        {
            ProjectManager_Block projectManager_block = experimentManager_block.GetComponent<ProjectManager_Block>();
            if (projectManager_block != null && projectManager_block.usernameInput != null)
            {
                username = projectManager_block.usernameInput.text; // Assuming usernameInput is a TMP_InputField
            }
            else
            {
                Debug.LogWarning("ProjectManager_Block or usernameInput is not assigned.");
            }
        }
        else
        {
            Debug.LogWarning("ExperimentManager_Block GameObject not found.");
        }

        trajectoryPoints = new List<Vector3>();
        filePath = Application.dataPath + "/CSV/Trajectory/InterventionMidpoint.csv";
        Directory.CreateDirectory(Application.dataPath + "/CSV");

        if (!File.Exists(filePath))
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.WriteLine("Username,X,Y,Z");
            }
        }
    }

    void Update()
    {
        Vector3 currentPosition = Userball.position;
        trajectoryPoints.Add(currentPosition);
    }

    public void SaveTrajectoryData()
    {
        if (trajectoryPoints.Count == 0)
        {
            Debug.LogWarning("No trajectory points to save.");
            return;
        }

        if (string.IsNullOrEmpty(username))
        {
            Debug.LogWarning("Username is null or empty. Cannot save trajectory data.");
            return;
        }

        using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8))
        {
            foreach (Vector3 point in trajectoryPoints)
            {
                string data = string.Format("{0},{1},{2},{3}", username, point.x, point.y, point.z);
                sw.WriteLine(data);
            }
        }
        Debug.Log("Trajectory data saved to: " + filePath);
    }

    public void ClearTrajectoryData()
    {
        trajectoryPoints.Clear();
        Debug.Log("Trajectory data cleared.");
    }
}
