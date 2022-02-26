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
            PauseMenuScript.instance.isPaused = true;
        }
        else
        {
            aaaaa = true;
            PauseMenuScript.instance.isPaused = false;
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
                PauseMenuScript.instance.isPaused = false;
            }
        }
        //time
        if (PauseMenuScript.instance.isPaused || !aaaaa) return;
        time += Time.deltaTime;
        timeText.text = ((int)time / 60).ToString("00") + ":" + (time % 60).ToString("00");
    }

    public void Complete()
    {
        StartCoroutine(PlayLevelClear());
        AudioManager.instance.PlayS("levelClear");
        AudioManager.instance.aSource.Stop();
        PauseMenuScript.instance.isPaused = true;
        finishScreen.SetActive(true);
        FinishTimeText.text = "Time Spent\n" + ((int)time / 60).ToString("00") + ":" + (time % 60).ToString("00") + ":" + ((time*1000)%1000).ToString("000");
        int idx = level - 1;
        float best = PlayerPrefs.GetFloat("BestTime" + idx.ToString(), 0f);
        if(best == 0 || time < best)
        {
            PlayerPrefs.SetFloat("BestTime" + idx.ToString(), time);
        }
        Debug.Log("completed");
        if (last != null) last.ShowText();
    }

    private IEnumerator PlayLevelClear()
    {
        yield return new WaitForSeconds(2f);
        AudioManager.instance.PlayM("levelClear");
    }
}
