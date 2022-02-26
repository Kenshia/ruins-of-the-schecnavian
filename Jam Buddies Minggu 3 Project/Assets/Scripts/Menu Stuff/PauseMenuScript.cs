using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static PauseMenuScript instance;
    public bool isPaused = false;
    public GameObject PauseMenu1;
    public GameObject PauseMenu2;
    public GameObject PauseMenu3;
    public GameObject PauseMenu4;
    public GameObject settingMenu;
    private GameObject ActivePauseMenu;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        ActivePauseMenu = PauseMenu1;
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
        ActivePauseMenu.SetActive(false);
        isPaused = false;
    }

    public void Pause()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                ActivePauseMenu = PauseMenu1;
                PauseMenu1.SetActive(true);
                isPaused = true;
                break;
            case 1:
                ActivePauseMenu = PauseMenu2;
                PauseMenu2.SetActive(true);
                isPaused = true;
                break;
            case 2:
                ActivePauseMenu = PauseMenu3;
                PauseMenu3.SetActive(true);
                isPaused = true;
                break;
            case 3:
                ActivePauseMenu = PauseMenu4;
                PauseMenu4.SetActive(true);
                isPaused = true;
                break;
        }
    }

    public void EnableActivePauseMenu()
    {
        ActivePauseMenu.SetActive(true);
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
