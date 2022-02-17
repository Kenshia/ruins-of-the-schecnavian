using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private bool isActivated;
    public GameObject[] Gates;
    private void Start()
    {
        isActivated = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Stone")
        {
            isActivated = true;
            Debug.Log("Activated");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stone")
        {
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
}
