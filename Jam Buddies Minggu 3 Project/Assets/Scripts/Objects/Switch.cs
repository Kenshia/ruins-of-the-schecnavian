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
            if (Physics2D.OverlapCircle(transform.position, 0.8f, mask))
                ToggleSwitch();
    }

    private void ToggleSwitch()
    {
        if (isPressed)
        {
            isPressed = false;
            sr.color = Color.white;
        }
        else
        {
            isPressed = true;
            sr.color = Color.blue;
        }
    }

    private void OnRealEvent()
    {
        isPressed = hidden;
    }

    private void OnUnrealEvent()
    {
        hidden = isPressed;
    }
}
