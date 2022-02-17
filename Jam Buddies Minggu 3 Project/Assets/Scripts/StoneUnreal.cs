using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneUnreal : MonoBehaviour
{
    private Vector2 pos;

    private void Start()
    {
        pos = transform.position;
        Events.realWorld.AddListener(OnRealEvent);
        Events.unrealWorld.AddListener(OnUnrealEvent);
    }

    private void OnRealEvent()
    {
        pos = transform.position;
    }

    private void OnUnrealEvent()
    {
        transform.position = pos;
    }
}
