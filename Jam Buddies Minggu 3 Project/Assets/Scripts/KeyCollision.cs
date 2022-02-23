using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollision : MonoBehaviour
{
    private void Start()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlayS("key");
            KeyCounter.instance.keyCount++;
            Events.key.Invoke();
            KeyStaticInstance.keyStatic.PlayAt(transform.position);
            Destroy(gameObject);
        }
    }
}
