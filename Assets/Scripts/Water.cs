using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer sr;
    private float timePerFrame;
    private int i;
    private void Awake()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
    }
    private void Start()
    {
        timePerFrame = 1f;
        sr = GetComponent<SpriteRenderer>();
        Events.realWorld.AddListener(OnReal);
        Events.unrealWorld.AddListener(OnUnreal);
    }
    private void FixedUpdate()
    {
        timePerFrame -= Time.deltaTime;
        if(timePerFrame <= 0)
        {
            timePerFrame = 1f;
            sr.sprite = sprites[i % 3];
            i = (i + 1) % 3;
        }
    }

    private void OnReal()
    {
        gameObject.SetActive(true);
    }

    private void OnUnreal()
    {
        gameObject.SetActive(false);
    }
}
