using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public int level;
    public GameObject finishScreen;
    private float time;

    private void Start()
    {
        Time.timeScale = 1f;
        finishScreen.SetActive(false);
        time = 0;
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;
    }

    public void Complete()
    {
        Time.timeScale = 0f;
        finishScreen.SetActive(true);
        LevelSelector.instance.SaveFinishLevel(level, time);
        Debug.Log("completed");
    }
}
