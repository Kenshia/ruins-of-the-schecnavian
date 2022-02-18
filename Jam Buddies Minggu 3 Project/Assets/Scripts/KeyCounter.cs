using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCounter : MonoBehaviour
{
    public static KeyCounter instance;
    public int keyCount;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}
