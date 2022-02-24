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

    private void Awake()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
    }

    private void Start()
    {
        text.text = keyNeeded.ToString();
        Events.realWorld.AddListener(OnReal);
        Events.unrealWorld.AddListener(OnUnreal);
    }

    public bool CheckKey()
    {
        if (KeyCounter.instance.keyCount >= keyNeeded)
        {
            KeyCounter.instance.keyCount -= keyNeeded;
            AudioManager.instance.PlayS("door");
            //ganti ke change sprite / whatever it is
            Destroy(gameObject);
            return true;
        }
        return false;
    }
    private void OnReal()
    {
        sr.sprite = real;
    }
    private void OnUnreal()
    {
        sr.sprite = unreal;
    }
}
