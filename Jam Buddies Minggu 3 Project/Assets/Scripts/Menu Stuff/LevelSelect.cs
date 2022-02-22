using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [HideInInspector]
    public int input = -1;

    public void StartLevel()
    {
        if (input == -1) return;
        SceneManager.LoadScene(input);
    }
    public void LevelLoad(int index)
    {
        SceneManager.LoadScene(index);
    }
}
