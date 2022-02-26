using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [HideInInspector] public List<Vector2> moveHistory;
    private int count;
    Rigidbody2D rb;
    Vector3 nextPos, dir, moveDir;
    private void Awake()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            if (collider.CompareTag("MoveableObject") || collider.CompareTag("Gate"))
            {
                if (!Physics2D.OverlapCircle(transform.position + Vector3.right, 0.2f)) Instantiate(gameObject, transform.position + Vector3.right, Quaternion.identity);
                if (!Physics2D.OverlapCircle(transform.position + Vector3.left, 0.2f)) Instantiate(gameObject, transform.position + Vector3.left, Quaternion.identity);
                if (!Physics2D.OverlapCircle(transform.position + Vector3.up, 0.2f)) Instantiate(gameObject, transform.position + Vector3.up, Quaternion.identity);
                if (!Physics2D.OverlapCircle(transform.position + Vector3.down, 0.2f)) Instantiate(gameObject, transform.position + Vector3.down, Quaternion.identity);
                Destroy(gameObject);

            }
        }
    }
    public bool CheckMovement(Vector2 v)
    {
        dir = v;
        bool canMove = false;
        bool thereIsKey = false;
        Collider2D[] collision = Physics2D.OverlapCircleAll((Vector2)transform.position + (Vector2)dir, 0.2f);
        if (collision.Length == 0)
        {
            //transform.position += (Vector3)dir;
            Move();
            return true;
        }
        foreach (Collider2D collider in collision)
        {
            if (collider.CompareTag("MoveableObject"))
            {
                canMove = collider.GetComponent<ObjectMovement>().CheckMovement(dir);
                if (canMove)
                {
                    //transform.position += (Vector3)dir;
                    Move();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (collider.CompareTag("Key"))
            {
                thereIsKey = true;
                continue;
            }
        }
        if (thereIsKey)
        {
            Move();
            return true;
        }
        else return false;
    }

    private void Move()
    {
        nextPos = transform.position + (Vector3)dir;
        StartCoroutine(IMove());
    }
    private IEnumerator IMove()
    {
        //with velocity
        moveDir = nextPos - transform.position;
        rb.velocity = moveDir * 3f;
        while (Mathf.Abs(nextPos.x - transform.position.x) > 0.1f || Mathf.Abs(nextPos.y - transform.position.y) > 0.1f)
        {
            yield return new WaitForSeconds(0.01f);
        }
        rb.velocity = Vector2.zero;
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
    }

}
