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
    public GameObject pressEText;
    private bool oldPressed;
    private bool isReal;
    private SpriteRenderer sr;
    private void Awake()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
    }
    private void Start()
    {
        pressEText.SetActive(false);
        isReal = true;
        sr = GetComponent<SpriteRenderer>();
        isPressed = false;
        oldPressed = isPressed;
        Events.realWorld.AddListener(OnRealEvent);
        Events.unrealWorld.AddListener(OnUnrealEvent);
        StartCoroutine(CheckPlayerForText());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (PauseMenuScript.instance.isPaused) return;
            if (!isReal) return;
            if (Physics2D.OverlapCircle(transform.position, 0.2f, mask))
                ToggleSwitch();
        }
    }
    private IEnumerator CheckPlayerForText()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            if (isReal)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
                foreach (Collider2D col in colliders)
                {
                    if (col.CompareTag("Player"))
                    {
                        pressEText.SetActive(true);
                        break;
                    }
                    pressEText.SetActive(false);
                }
            }
            else
            {
                pressEText.SetActive(false);
            }
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
        StartCoroutine( PlayGateSound() );
    }

    private void OnRealEvent()
    {
        isPressed = oldPressed;
        isReal = true;
        sr.sprite = (isPressed) ? realOn : realOff;
        UpdateGates();
    }

    private void OnUnrealEvent()
    {
        oldPressed = isPressed;
        isPressed = !isPressed;
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

    private IEnumerator PlayGateSound() {
        yield return new WaitForSeconds(0.4f);
        if (Random.Range(0, 100) < 50)
            AudioManager.instance.PlayS("gate1");
        else
            AudioManager.instance.PlayS("gate2");
    }
}
