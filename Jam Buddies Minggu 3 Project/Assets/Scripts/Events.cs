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
        AudioManager.instance.PlayM("inGame");
        AudioManager.instance.PlayAmbiance("real");
        realWorld.AddListener(Real);
        unrealWorld.AddListener(Unreal);

        StartCoroutine(RealWorldInvoke()); //start with real world
    }

    private IEnumerator RealWorldInvoke()
    {
        yield return new WaitForSeconds(0.05f);
        realWorld.Invoke();
    }

    private void Unreal()
    {
        AudioManager.instance.PlayAmbiance("unreal");
        currentWorld.text = "IMAGINARY";
    }

    private void Real()
    {
        AudioManager.instance.PlayAmbiance("real");
        currentWorld.text = "REAL";
    }
}
