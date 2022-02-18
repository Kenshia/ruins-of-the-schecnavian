using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public LevelData[] levels;


    [System.Serializable]
    public class LevelData
    {
        public int level;
        public float timeSpent;
        public int abilityUsed;
    }
}
