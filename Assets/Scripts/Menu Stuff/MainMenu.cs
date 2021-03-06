using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.PlayM("mainMenu");
    }
    public void Play(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.LogWarning("Quit game");
    }
}
