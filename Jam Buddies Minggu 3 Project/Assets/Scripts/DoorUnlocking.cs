using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorUnlocking : MonoBehaviour
{
    public TextMeshPro text;
    public int keyNeeded;
    private void Start()
    {
        text.text = keyNeeded.ToString();
        Events.key.AddListener(CheckKey);
    }

    private void CheckKey()
    {
        if (KeyCounter.instance.keyCount >= keyNeeded)
        {
            //ganti ke change sprite / whatever it is
            Destroy(gameObject);
        }
    }
}
