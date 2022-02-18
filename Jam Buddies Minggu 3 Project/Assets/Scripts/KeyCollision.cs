using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            KeyCounter.instance.keyCount++;
            Events.key.Invoke();
            Destroy(gameObject);
        }
    }
}
