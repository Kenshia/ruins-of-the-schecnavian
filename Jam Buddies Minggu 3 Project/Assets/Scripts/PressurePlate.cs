using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private bool isActivated;
    public GameObject[] Gates;
    public SpriteRenderer sr;
    public Sprite real;
    public Sprite unreal;
    private void Start()
    {
        isActivated = false;
        Events.realWorld.AddListener(OnReal);
        Events.unrealWorld.AddListener(OnUnreal);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Stone")
        {
            AudioManager.instance.PlayS("pressurePlate1");
            isActivated = true;
            Debug.Log("Activated");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stone")
        {
            AudioManager.instance.PlayS("pressurePlate2");
            isActivated = false;
            Debug.Log("Deactivated");
        }
    }

    private void Update()
    {
        if (isActivated)
        {
            foreach (GameObject gate in Gates)
            {
                gate.SetActive(false);
            }
        }
        else if (!isActivated)
        {
            foreach (GameObject gate in Gates)
            {
                gate.SetActive(true);
            }
        }
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
