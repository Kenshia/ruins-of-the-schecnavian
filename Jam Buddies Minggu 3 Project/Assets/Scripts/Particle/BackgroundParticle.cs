using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParticle : MonoBehaviour
{
    private ParticleSystem.MainModule ps;
    private float R, G, B;
    private void Start()
    {
        ps = GetComponent<ParticleSystem>().main;
        R = 106f / 255f;
        G = 104f / 255f;
        B = 205f / 255f;
        Events.realWorld.AddListener(OnReal);
        Events.unrealWorld.AddListener(OnUnreal);
    }


    private void OnReal()
    {
        ps.startColor = Color.white;
    }
    private void OnUnreal()
    {
        ps.startColor = new Color(R, G, B);
    }
}
