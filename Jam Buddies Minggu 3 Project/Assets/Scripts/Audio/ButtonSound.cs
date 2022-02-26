using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public void PlayButtonSound()
    {
        if (Random.Range(0, 100) < 100)
            AudioManager.instance.PlayS("writing1");
        else
            AudioManager.instance.PlayS("writing2");
    }
}
