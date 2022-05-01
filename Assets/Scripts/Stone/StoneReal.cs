using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneReal : MonoBehaviour
{
    private Vector2 pos;
    public ObjectMovement objectMovementScript;

    private void Start()
    {
        pos = transform.position;
        Events.realWorld.AddListener(OnRealEvent);
        Events.unrealWorld.AddListener(OnUnrealEvent);
    }

    private void OnRealEvent()
    {
        transform.position = pos;
        objectMovementScript.CheckCrushed();
    }

    private void OnUnrealEvent()
    {
        pos = transform.position;
        objectMovementScript.CheckCrushed();
    }
}
