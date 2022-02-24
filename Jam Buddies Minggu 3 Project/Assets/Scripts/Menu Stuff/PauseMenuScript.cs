using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static PauseMenuScript instance;
    public bool isPaused = false;
    public GameObject PauseMenu;
    public GameObject settingMenu;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

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
        settingMenu.SetActive(false);
        PauseMenu.SetActive(false);
        isPaused = false;
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
        isPaused = true;
    }

    public void BackToMainMenu()
    {
        Resume();
        AudioManager.instance.aSource.Stop();
        AudioManager.instance.mSource.Stop();
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        Resume();
        AudioManager.instance.aSource.Stop();
        AudioManager.instance.mSource.Stop();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
