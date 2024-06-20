using UnityEngine;

public class Mirror : MonoBehaviour
{
    public RenderTexture monitorTexture;

    void Start()
    {
    
        GetComponent<Renderer>().material.mainTexture = monitorTexture;
    }

    void Update()
    {

        Graphics.Blit(null, monitorTexture);

        GetComponent<Renderer>().material.mainTexture = monitorTexture;
    }
}
