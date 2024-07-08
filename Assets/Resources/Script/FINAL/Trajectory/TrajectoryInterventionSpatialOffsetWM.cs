using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class TrajectoryInterventionSpatialOffsetWM : MonoBehaviour
{
    public Transform Userball;
    private HashSet<Vector3> savedPoints;
    private string filePath;
    private string username;
    private string track;

    void Start()
    {
        GameObject experimentManager_block = GameObject.Find("ExperimentManager_Block");
        ProjectManager_Block projectManager_block = experimentManager_block.GetComponent<ProjectManager_Block>();
        username = projectManager_block.usernameInput.text;
        GameObject pathManager =GameObject.Find("PathManager");
        PathManager pathManagerScript = pathManager.GetComponent<PathManager>();
        track=pathManagerScript.Trajectory;
        Debug.Log("Track is " + pathManagerScript.Trajectory);
        savedPoints = new HashSet<Vector3>();
        filePath = Application.dataPath + "/CSV/Trajectory/InterventionSpatialOffsetWeightMarker";
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        if (!File.Exists(filePath))
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.WriteLine("Username,Trajectory,X,Y,Z");
            }
        }
    }

    void Update()
    {
        Vector3 currentPosition = Userball.position;
        if (savedPoints.Add(currentPosition))
        {
            SaveTrajectoryData(currentPosition);
        }
    }

    void SaveTrajectoryData(Vector3 point)
    {
        using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8))
        {
            string data = string.Format("{0},{1},{2},{3},{4}",username, track, point.x, point.y, point.z);
            sw.WriteLine(data);
            Debug.Log("Data Saved: " + data);
        }
        Debug.Log("Trajectory data saved to: " + filePath);
    }
}
