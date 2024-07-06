using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPlayer : MonoBehaviour
{
    public GameObject[] videos;
    private int currentVideoIndex = -1;

    void Start()
    {
        for (int i = 0; i < videos.Length; i++)
        {
            videos[i].SetActive(false);
        }
    }

    public void PlayVideo()
    {
        // Deactivate the currently active video
        if (currentVideoIndex >= 0 && currentVideoIndex < videos.Length)
        {
            videos[currentVideoIndex].SetActive(false);
        }

        // Select a new video to play
        currentVideoIndex = Random.Range(0, videos.Length);
        videos[currentVideoIndex].SetActive(true);
    }

    public void PauseVideo()
    {
        // Deactivate the currently active video
        if (currentVideoIndex >= 0 && currentVideoIndex < videos.Length)
        {
            videos[currentVideoIndex].SetActive(false);
        }
    }
}
