using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorUnlocking : MonoBehaviour
{
    public TextMeshPro text;
    public int keyNeeded;
    public SpriteRenderer sr;
    public Sprite real;
    public Sprite unreal;
    private bool realstate;
    private void Start()
    {
        text.text = keyNeeded.ToString();
        realstate = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (realstate)
            {
                sr.sprite = unreal;
                realstate = false;
            }
            else if (!realstate)
            {
                sr.sprite = real;
                realstate = true;
            }
        }
    }

    public bool CheckKey()
    {
        if (KeyCounter.instance.keyCount >= keyNeeded)
        {
            KeyCounter.instance.keyCount -= keyNeeded;
            AudioManager.instance.PlayS("door");
            //ganti ke change sprite / whatever it is
            Destroy(gameObject, 0.1f);
            return true;
        }
        return false;
    }
}
