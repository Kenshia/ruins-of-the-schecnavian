using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishColliderDetection : MonoBehaviour
{
    public FinishAndTime finish;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) finish.Complete();
    }
}
