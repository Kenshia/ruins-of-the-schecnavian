using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [HideInInspector] public List<Vector2> moveHistory;
    private int count;
    private void Awake()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
    }

    public bool CheckMovement(Vector2 dir)
    {
        bool canMove = false;
        Collider2D[] collision = Physics2D.OverlapCircleAll((Vector2)transform.position + dir, 0.2f);
        if (collision.Length == 0)
        {
            SaveMovement();
            transform.position += (Vector3)dir;
            return true; ;
        }
        foreach (Collider2D collider in collision)
        {
            if (collider.CompareTag("MoveableObject"))
            {
                canMove = collider.GetComponent<ObjectMovement>().CheckMovement(dir);
                if (canMove)
                {
                    SaveMovement();
                    transform.position += (Vector3)dir;
                    return true;
                }
            }
        }
        return false;
    }
    private void SaveMovement()
    {
        moveHistory.Add(transform.position);
        count++;
    }
    public void DeleteMoveHistory()
    {
        moveHistory.Clear();
        count = 0;
    }
    public Vector2 RefundMovement()
    {
        if (count == 0) return transform.position;
        count--;
        Vector2 pos = moveHistory[count];
        return pos;
    }
}
