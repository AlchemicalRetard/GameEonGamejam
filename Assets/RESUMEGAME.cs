using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RESUMEGAME : MonoBehaviour
{
    private void Awake()
    {
        if (Time.timeScale < 1)
        {
            Time.timeScale = 1;
        }
    }
}
