using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LevelSelector : MonoBehaviour
{
    public LevelSelect select;
    public static LevelSelector instance;
    public LevelData[] levels;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI timeText;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadResult();
    }

    public void SaveFinishLevel(int level, float time)
    {
        int idx = level - 1;
        if(time < levels[idx].timeSpent || levels[idx].timeSpent == 0)
        {
            levels[idx].timeSpent = time;
            SaveResult();
        }
    }

    public void SelectLevel(int level)
    {
        int index = level - 1;
        levelText.text = "Level: " + levels[index].level.ToString();
        timeText.text = "Best Time: " + levels[index].timeSpent.ToString("0.00");
        select.input = index;
    }

    public void SaveResult()
    {
        for(int i=0; i<levels.Length; i++)
        {
            PlayerPrefs.SetFloat("BestTime" + i.ToString(), levels[i].timeSpent);
        }
    }

    public void LoadResult()
    {
        for(int i=0; i<levels.Length; i++)
        {
            levels[i].timeSpent = PlayerPrefs.GetFloat("BestTime" + i.ToString(), 0f);
        }
    }

    [System.Serializable]
    public class LevelData
    {
        public int level;
        public float timeSpent;
    }
}
