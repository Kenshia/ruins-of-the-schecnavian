using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject[] gate;
    public bool isPressed;
    public LayerMask mask;
    public Sprite realOn;
    public Sprite realOff;
    public Sprite unrealOn;
    public Sprite unrealOff;
    private bool oldPressed;
    private bool isReal;
    private SpriteRenderer sr;
    private void Awake()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
    }
    private void Start()
    {
        isReal = true;
        sr = GetComponent<SpriteRenderer>();
        isPressed = false;
        oldPressed = isPressed;
        Events.realWorld.AddListener(OnRealEvent);
        Events.unrealWorld.AddListener(OnUnrealEvent);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (PauseMenuScript.instance.isPaused) return; 
            if (Physics2D.OverlapCircle(transform.position, 0.2f, mask))
                ToggleSwitch();
        }
    }

    private void ToggleSwitch()
    {
        if (isPressed)
        {
            AudioManager.instance.PlayS("switch2");
            isPressed = false;
            sr.sprite = (isReal) ? realOff : unrealOff;
        }
        else
        {
            AudioManager.instance.PlayS("switch1");
            isPressed = true;
            sr.sprite = (isReal) ? realOn : unrealOn;
        }
        UpdateGates();
    }

    private void OnRealEvent()
    {
        bool temp = isPressed;
        isPressed = oldPressed;
        oldPressed = temp;
        isReal = true;
        sr.sprite = (isPressed) ? realOn : realOff;
        UpdateGates();
    }

    private void OnUnrealEvent()
    {
        bool temp = isPressed;
        isPressed = oldPressed;
        oldPressed = temp;
        isReal = false;
        sr.sprite = (isPressed) ? unrealOn : unrealOff;
        UpdateGates();
    }

    private void UpdateGates()
    {
        foreach (GameObject @object in gate)
        {
            @object.SetActive(!isPressed);
        }
    }
}
