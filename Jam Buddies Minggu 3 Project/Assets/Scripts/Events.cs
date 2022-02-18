using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class Events : MonoBehaviour
{
    public static UnityEvent realWorld;
    public static UnityEvent unrealWorld;
    public static UnityEvent key;
    public Text currentWorld;

    private void Awake()
    {
        if (realWorld == null) realWorld = new UnityEvent();
        if (unrealWorld == null) unrealWorld = new UnityEvent();
        if (key == null) key = new UnityEvent();
    }

    private void Start()
    {
        realWorld.AddListener(Real);
        unrealWorld.AddListener(Unreal);

        realWorld.Invoke(); //start with real world
    }

    private void Unreal()
    {
        currentWorld.text = "UNREAL";
    }

    private void Real()
    {
        currentWorld.text = "REAL";
    }
}
