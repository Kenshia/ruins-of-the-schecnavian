using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public LevelSelect select;
    public LevelData[] levels;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI timeText;

    private void Start()
    {
        LoadResult();
    }

    public void SelectLevel(int level)
    {
        int index = level - 1; //index for array
        levelText.text = "Level: " + levels[index].level.ToString();
        timeText.text = "Best Time: " + ((int)levels[index].timeSpent/60).ToString("00") + ":" + (levels[index].timeSpent%60).ToString("00");
        select.input = index + 1; //level1 = build index 1
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) SaveResult();
    }
}
