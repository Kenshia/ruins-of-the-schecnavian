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
    public void CheckCrushed()
    {
        StartCoroutine(CoroutineCheckCrushed());
    }
    public IEnumerator CoroutineCheckCrushed()
    {
        yield return new WaitForSeconds(0.02f);
        /*
         * if crushed, delete crushed, multiply current to all 4 sides except if the place has objective, and obstacle
         */
        Collider2D[] collision = Physics2D.OverlapCircleAll(transform.position, 0.2f);
        foreach (Collider2D collider in collision)
        {
            if (collider.gameObject == this.gameObject || collider.CompareTag("Death")) continue;
            if (collider.CompareTag("Player")) yield return null;
            if (collider.CompareTag("MoveableObject"))
            {
                Destroy(collider.gameObject);
                if (!Physics2D.OverlapCircle(transform.position + Vector3.right, 0.2f)) Instantiate(gameObject, transform.position + Vector3.right, Quaternion.identity);
                if (!Physics2D.OverlapCircle(transform.position + Vector3.left, 0.2f)) Instantiate(gameObject, transform.position + Vector3.left, Quaternion.identity);
                if (!Physics2D.OverlapCircle(transform.position + Vector3.up, 0.2f)) Instantiate(gameObject, transform.position + Vector3.up, Quaternion.identity);
                if (!Physics2D.OverlapCircle(transform.position + Vector3.down, 0.2f)) Instantiate(gameObject, transform.position + Vector3.down, Quaternion.identity);
                Destroy(gameObject);

            }
        }
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
