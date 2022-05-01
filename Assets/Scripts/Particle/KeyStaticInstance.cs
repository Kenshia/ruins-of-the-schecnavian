using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyStaticInstance : MonoBehaviour
{
    public static KeyStaticInstance keyStatic;
    public ParticleSystem ps;

    private void Awake()
    {
        if (keyStatic == null) keyStatic = this;
        else Destroy(gameObject);
    }

    public void PlayAt(Vector3 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        ps.Play();
    }
}
