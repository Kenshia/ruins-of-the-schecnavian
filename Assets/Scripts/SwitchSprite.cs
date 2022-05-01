using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSprite : MonoBehaviour
{
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
    private void OnReal()
    {
        sr.sprite = real;
    }
    private void OnUnreal()
    {
        sr.sprite = unreal;
    }
}
