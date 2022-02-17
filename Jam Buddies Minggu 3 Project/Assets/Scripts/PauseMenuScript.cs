using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject PauseMenu;
    public AudioSource BGM;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused)
            {
                Pause();
            }
            else if(isPaused)
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        BGM.UnPause();
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
        BGM.Pause();
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void BackToMainMenu()
    {
        Resume();
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
