using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Button resumeButton;
    public Button quitButton;
    public GameObject target;
    public static bool isPaused = false;

    void Start()
    {
        resumeButton.onClick.AddListener(Resume);
        quitButton.onClick.AddListener(Quit);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            target.SetActive(!target.activeSelf);
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;
        }
    }

    public void Resume()
    {
        target.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }
    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
