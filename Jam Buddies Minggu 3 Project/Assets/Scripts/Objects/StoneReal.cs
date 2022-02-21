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
        //CheckOverlap(false);
    }

    public void CheckOverlap(bool calledFromOtherObject)
    {
        /*
         * ketika tp, dia cek apakah tempatnya ada obstacle lain
         * kalau ada air atau apapun maka dia tp balik
         * kalau ada batu lain, maka suruh batu itu balik ke tempat sebelomnya
         */
        if (calledFromOtherObject)
        {
            transform.position = objectMovementScript.RefundMovement();
            pos = transform.position;
        }
        Collider2D[] collision = Physics2D.OverlapCircleAll((Vector2)transform.position, 0.2f, 128);
        foreach (Collider2D collider in collision)
        {
            if (collider.gameObject == gameObject) continue;
            else if (collider.CompareTag("Obstacle")) //air
            {
                transform.position = objectMovementScript.RefundMovement();
                pos = transform.position;
            }
            else if (collider.CompareTag("MoveableObject"))
            {
                if (collider.name.Substring(0, 9).Equals("StoneReal"))
                    collider.gameObject.GetComponent<StoneReal>().CheckOverlap(true);
                else
                    collider.gameObject.GetComponent<StoneUnreal>().CheckOverlap(true);
            }
        }
    }
    private void OnUnrealEvent()
    {
        pos = transform.position;
        objectMovementScript.CheckCrushed();
    }
}
