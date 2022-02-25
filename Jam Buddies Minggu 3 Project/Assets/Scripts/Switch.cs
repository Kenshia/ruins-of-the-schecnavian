using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool isPressed;
    public LayerMask mask;
    private bool hidden;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        isPressed = false;
        hidden = isPressed;
        Events.realWorld.AddListener(OnRealEvent);
        Events.unrealWorld.AddListener(OnUnrealEvent);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            if (PauseMenuScript.instance.isPaused) return; 
            if (Physics2D.OverlapCircle(transform.position, 0.8f, mask))
                ToggleSwitch();
    }

    private void ToggleSwitch()
    {
        if (isPressed)
        {
            AudioManager.instance.PlayS("switch2");
            isPressed = false;
            sr.color = Color.white;
        }
        else
        {
            AudioManager.instance.PlayS("switch1");
            isPressed = true;
            sr.color = Color.blue;
        }
    }

    private void OnRealEvent()
    {
        if (hidden == isPressed) return;

        ToggleSwitch();
        hidden = isPressed;
    }

    private void OnUnrealEvent()
    {
        if (hidden == isPressed) return;

        ToggleSwitch();
        hidden = isPressed;
    }
}
