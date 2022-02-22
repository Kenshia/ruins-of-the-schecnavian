using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishColliderDetection : MonoBehaviour
{
    public FinishAndTime finish;
    public SpriteRenderer sr;
    public Sprite real;
    public Sprite unreal;
    private bool realstate;
    private void Start()
    {
        realstate = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) finish.Complete();
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
