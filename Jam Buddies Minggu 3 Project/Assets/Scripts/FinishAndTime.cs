using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishAndTime : MonoBehaviour
{
    public int level;
    public GameObject finishScreen;
    public TextMeshProUGUI timeText;
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
        timeText.text = ((int)time / 60).ToString("00") + ":" + (time%60).ToString("00");
    }

    public void Complete()
    {
        Time.timeScale = 0f;
        finishScreen.SetActive(true);
        int idx = level - 1;
        float best = PlayerPrefs.GetFloat("BestTime" + idx.ToString(), 0f);
        if(best == 0 || time < best)
        {
            PlayerPrefs.SetFloat("BestTime" + idx.ToString(), time);
        }
        Debug.Log("completed");
    }
}
