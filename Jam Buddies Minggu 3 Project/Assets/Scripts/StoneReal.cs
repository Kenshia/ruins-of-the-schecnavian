using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneReal : MonoBehaviour
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
        transform.position = pos;
    }

    private void OnUnrealEvent()
    {
        pos = transform.position;
    }
}
