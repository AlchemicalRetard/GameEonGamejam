using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clock : MonoBehaviour
{
   public TextMeshProUGUI clockText;
    public GameObject winScreen;
    private float realTime;
    private float gameTime;
    private bool isPaused = false;

    void Update()
    {
        // Increase real time by Time.deltaTime every frame
        realTime += Time.deltaTime;

        // If 15 real seconds have passed
        if (realTime >= 25)
        {
            // Increase game time by 30 minutes
            gameTime += 30;

            // Reset real time
            realTime = 0;
        }

        // Calculate hours and minutes
        int hours = (int)(gameTime / 60);
        int minutes = (int)(gameTime % 60);

        // Update clock text
        clockText.text = hours.ToString("00") + ":" + minutes.ToString("00") + "AM";

        if(hours == 6 && minutes == 0 && !isPaused)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
    }
}
