using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneReal : MonoBehaviour
{
    private Vector2 pos;
    private float soundCd;

    private void Start()
    {
        soundCd = 1f;
        pos = transform.position;
        Events.realWorld.AddListener(OnRealEvent);
        Events.unrealWorld.AddListener(OnUnrealEvent);
    }

    private void OnRealEvent()
    {
        transform.position = pos;
    }

    private void OnUnrealEvent()
    {
        pos = transform.position;
    }
    private void FixedUpdate()
    {
        soundCd -= Time.deltaTime;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && soundCd <= 0)
        {
            soundCd = 1f;
            switch (Random.Range(0, 4))
            {
                case 1:
                    AudioManager.instance.PlayS("push1");
                    break;
                case 2:
                    AudioManager.instance.PlayS("push2");
                    break;
                case 3:
                    AudioManager.instance.PlayS("push3");
                    break;
                default:
                    break;
            }
        }
    }
}
