using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinishAndTime : MonoBehaviour
{
    public int level;
    public GameObject finishScreen;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI FinishTimeText;
    private float time;
    private bool aaaaa;
    public GameObject textToDisplay;
    public LASTSCRIPT last;

    private void Start()
    {
        if(textToDisplay != null)
        {
            aaaaa = false;
            textToDisplay.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            aaaaa = true;
            Time.timeScale = 1f;
        }
        finishScreen.SetActive(false);
        time = 0;
    }
    private void Update()
    {
        if (!aaaaa)
        {
            if (Input.anyKey)
            {
                textToDisplay.SetActive(false);
                aaaaa = true;
                Time.timeScale = 1f;
            }
        }
    }
    private void FixedUpdate()
    {
        if (PauseMenuScript.instance.isPaused || !aaaaa) return;
        time += Time.deltaTime;
        timeText.text = ((int)time / 60).ToString("00") + ":" + (time%60).ToString("00");
    }

    public void Complete()
    {
        Time.timeScale = 0f;
        finishScreen.SetActive(true);
        FinishTimeText.text = "Time Spent : " + ((int)time / 60).ToString("00") + ":" + (time % 60).ToString("00");
        int idx = level - 1;
        float best = PlayerPrefs.GetFloat("BestTime" + idx.ToString(), 0f);
        if(best == 0 || time < best)
        {
            PlayerPrefs.SetFloat("BestTime" + idx.ToString(), time);
        }
        Debug.Log("completed");
        if (last != null) last.ShowText();
    }
}
