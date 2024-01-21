using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public Button yourButton; // Drag your Button here in inspector
    public VideoPlayer videoPlayer; // Drag your VideoPlayer here in inspector
    public GameObject videoUI;
    void Start()
    {
        // Add a listener to the button
        yourButton.onClick.AddListener(PlayVideo);

        // Add a listener to the videoPlayer
        videoPlayer.loopPointReached += LoadScene;
    }

    void PlayVideo()
    {
        videoUI.SetActive(true);
        // Play the video
        videoPlayer.Play();
    }

    void LoadScene(VideoPlayer vp)
    {
        // Load the game scene
        SceneManager.LoadScene("SampleScene");
    }
}
