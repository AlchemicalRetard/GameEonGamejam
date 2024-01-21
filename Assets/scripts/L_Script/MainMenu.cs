using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject level;
    public GameObject EnemyPanel;
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
            Application.Quit();
    }
    public void Tutorial()
    {
        level.SetActive(true);
    }

    public void Skip()
    {
        level.SetActive(false);
        EnemyPanel.SetActive(true);
    }
    public void BackToMenu()
    {
        //level.SetActive(false);
        EnemyPanel.SetActive(false);
    }

}
