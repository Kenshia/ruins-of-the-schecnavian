using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public bool real;
    public GameObject realWorld;
    public GameObject unrealWorld;
    public Animator anim;
    public GameObject DeathScreenBatu;
    public GameObject DeathScreenTenggelem;
    public GameObject DeathScreenBatuUnreal;
    private Rigidbody2D rb;
    private Vector2 dir;
    private bool skip;
    [HideInInspector] public Vector3 nextPos, oldPos;
    private Vector3 moveDir;

    private void Start()
    {
        skip = false;
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
        rb = GetComponent<Rigidbody2D>();
        realWorld.SetActive(true);
        unrealWorld.SetActive(false);
    }

    private void Update()
    {
        if (PauseMenuScript.instance.isPaused || Time.timeScale == 0f || skip) return;
        NewMovement();
        if (Input.GetKeyDown(KeyCode.Space)) ToggleWorld();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            string objectName = collision.gameObject.name;

            if (objectName.Equals("WaterDeathCheck"))
            {
                PauseMenuScript.instance.isPaused = true;
                DeathScreenTenggelem.SetActive(true);
                AudioManager.instance.PlayS("drown");
                StartCoroutine(PlayGameOver());
            }
            else if(objectName.Equals("StoneRealDeathCheck"))
            {
                PauseMenuScript.instance.isPaused = true;
                DeathScreenBatu.SetActive(true);
                AudioManager.instance.PlayS("crushed");
                StartCoroutine(PlayGameOver());
            }
            else if(objectName.Equals("StoneUnrealDeathCheck"))
            {
                PauseMenuScript.instance.isPaused = true;
                DeathScreenBatuUnreal.SetActive(true);
                AudioManager.instance.PlayS("crushed");
                StartCoroutine(PlayGameOver());
            }    
        }
    }
    private IEnumerator PlayGameOver()
    {
        yield return new WaitForSeconds(3.5f);
        AudioManager.instance.PlayM("gameOver");
    }

    private void ToggleWorld()
    {
        AudioManager.instance.PlayS("toggleWorld");
        if (real)
        {
            real = false;
            Events.unrealWorld.Invoke();
            realWorld.SetActive(false);
            unrealWorld.SetActive(true);
        }
        else
        {
            real = true;
            Events.realWorld.Invoke();
            realWorld.SetActive(true);
            unrealWorld.SetActive(false);
        }
    }

    private void NewMovement()
    {
        bool thereIsKey = false;
        dir = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            dir.y = 1;
            anim.SetFloat("Vertical", 1);
            anim.SetFloat("Horizontal", 0);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            dir.y = -1;
            anim.SetFloat("Vertical", -1);
            anim.SetFloat("Horizontal", 0);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            dir.x = -1;
            anim.SetFloat("Horizontal", -1);
            anim.SetFloat("Vertical", 0);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            dir.x = 1;
            anim.SetFloat("Horizontal", 1);
            anim.SetFloat("Vertical", 0);
        }

        if (dir == Vector2.zero) return;

        bool canMove = false;
        Collider2D[] collision = Physics2D.OverlapCircleAll((Vector2)transform.position + dir, 0.2f);
        if (collision.Length == 0)
        {
            Move();
            return;
        }
        foreach (Collider2D collider in collision)
        {
            if (collider.CompareTag("MoveableObject"))
            {
                canMove = collider.GetComponent<ObjectMovement>().CheckMovement(dir);
                if (canMove)
                {
                    Move();
                    PushSound();
                }
                else
                {
                    break;
                }
            }
            else if (collider.CompareTag("Objective"))
            {
                Move();
            }
            else if (collider.CompareTag("Door"))
            {
                if (collider.GetComponent<DoorUnlocking>().CheckKey())
                    Move();
            }
            else if (collider.CompareTag("Key"))
            {
                thereIsKey = true;
            }
        }
        if (thereIsKey) Move();
    }

    private void Move()
    {
        StepSound();
        nextPos = transform.position + (Vector3)dir;
        StartCoroutine(Anim());
        StartCoroutine(IMove());
    }
    private IEnumerator IMove()
    {
        skip = true;
        moveDir = nextPos - transform.position;
        rb.velocity = moveDir * 3f;
        while (Mathf.Abs(nextPos.x - transform.position.x) > 0.1f || Mathf.Abs(nextPos.y - transform.position.y) > 0.1f)
        {
            yield return new WaitForSeconds(0.01f);
        }
        rb.velocity = Vector2.zero;
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
        skip = false;
    }

    private IEnumerator Anim()
    {
        anim.SetBool("Left", !anim.GetBool("Left"));
        yield return new WaitForSeconds(0.15f);
        anim.SetBool("Left", !anim.GetBool("Left"));
        yield return new WaitForSeconds(0.15f);
        anim.SetBool("Left", !anim.GetBool("Left"));
    }

    private void StepSound()
    {
        switch (Random.Range(0,7))
        {
            case 1:
                AudioManager.instance.PlayS("footstep1");
                break;
            case 4:
                AudioManager.instance.PlayS("footstep4");
                break;
            case 6:
                AudioManager.instance.PlayS("footstep6");
                break;
            default:
                StepSound();
                break;
        }
    }

    private void PushSound()
    {
        if (Random.Range(0, 100) < 50) return;
        switch (Random.Range(0, 4))
        {
            case 0:
                PushSound();
                break;
                //why? idk
            case 1:
                AudioManager.instance.PlayS("push1");
                break;
            case 2:
                AudioManager.instance.PlayS("push2");
                break;
            case 3:
                AudioManager.instance.PlayS("push3");
                break;
            default:
                AudioManager.instance.PlayS("push1");
                break;
        }
    }
}
