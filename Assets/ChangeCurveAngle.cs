using UnityEngine;
using CurvedUI;

public class ChangeCurveAngle : MonoBehaviour
{
    public CurvedUISettings curvedUISettings;
    public int Curveangle=180;

    void Start()
    {
            curvedUISettings = GetComponent<CurvedUISettings>();
            curvedUISettings.Angle = Curveangle;
    }
}
