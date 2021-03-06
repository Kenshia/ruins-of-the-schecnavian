using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishColliderDetection : MonoBehaviour
{
    public FinishAndTime finish;
    public SpriteRenderer sr;
    public Sprite real;
    public Sprite unreal;

    private void Awake()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
    }

    private void Start()
    {
        Events.realWorld.AddListener(OnReal);
        Events.unrealWorld.AddListener(OnUnreal);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !PauseMenuScript.instance.isPaused) finish.Complete();
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
