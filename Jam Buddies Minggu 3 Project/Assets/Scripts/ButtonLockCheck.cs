using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLockCheck : MonoBehaviour
{
    public int level;
    void Start()
    {
        if(PlayerPrefs.GetFloat("BestTime" + (level-2).ToString()) == 0f)
        {
            GetComponent<Image>().color = Color.gray;
            GetComponent<Button>().interactable = false;
        }
    }
}
