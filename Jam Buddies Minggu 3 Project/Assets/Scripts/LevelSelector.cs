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
    public TextMeshProUGUI abilityText;

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

    public void SaveFinishLevel(int level, float time, int abilityUsed)
    {
        int idx = level - 1;
        if(levels[idx].timeSpent > time) //best time now
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
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveData.dat");
        SaveData data = new SaveData();

        for(int i=0; i<levels.Length; i++)
        {
            data.timeSpent[i] = levels[i].timeSpent;
        }

        bf.Serialize(file, data);
        file.Close();
        Debug.Log("saved");
    }

    public void LoadResult()
    {
        if (!File.Exists(Application.persistentDataPath + "/SaveData.dat"))
        {
            Debug.Log("no data");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/SaveData.dat", FileMode.Open);
        SaveData data = (SaveData)bf.Deserialize(file);
        file.Close();
        for(int i=0; i<levels.Length; i++)
        {
            levels[i].timeSpent = data.timeSpent[i];
        }

        Debug.Log("loaded");
    }

    [System.Serializable]
    public class LevelData
    {
        public int level;
        public float timeSpent;
    }

    [System.Serializable]
    public class SaveData
    {
        public float[] timeSpent = new float[25];
    }
}
