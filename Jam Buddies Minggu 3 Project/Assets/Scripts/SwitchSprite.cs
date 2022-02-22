using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSprite : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite real;
    public Sprite unreal;
    private bool realstate;
    private void Start()
    {
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
}
