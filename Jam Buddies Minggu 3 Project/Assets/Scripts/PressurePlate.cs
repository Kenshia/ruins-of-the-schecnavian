using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject[] Gates;
    public SpriteRenderer sr;
    public Sprite real;
    public Sprite unreal;
    private void Awake()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
    }
    private void Start()
    {
        Deactivate();
        Events.realWorld.AddListener(OnReal);
        Events.unrealWorld.AddListener(OnUnreal);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("MoveableObject"))
        {
            AudioManager.instance.PlayS("pressurePlate1");
            Activate();
            Debug.Log("Activated");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MoveableObject"))
        {
            AudioManager.instance.PlayS("pressurePlate2");
            Deactivate();
            Debug.Log("Deactivated");
        }
    }
    private void Activate()
    {
        foreach (GameObject gate in Gates)
        {
            gate.SetActive(false);
        }
    }
    private void Deactivate()
    {
        foreach (GameObject gate in Gates)
        {
            gate.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(gate.transform.position, 0.2f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("MoveableObject"))
                {
                    collider.GetComponent<ObjectMovement>().CheckCrushed();
                    break;
                }
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
