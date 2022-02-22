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
    public GameObject DeathScreen;
    private Rigidbody2D rb;
    private Vector2 dir;
    private float stepCd;
    private bool skip;
    [HideInInspector] public Vector3 nextPos, oldPos;
    private Vector3 moveDir;

    private void Start()
    {
        skip = false;
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
        stepCd = 0f;
        rb = GetComponent<Rigidbody2D>();
        realWorld.SetActive(true);
        unrealWorld.SetActive(false);
    }

    private void Update()
    {
        if (PauseMenuScript.instance.isPaused || Time.timeScale == 0f || skip) return;
        NewMovement();
        //Anim();
        StepSound();
        if (Input.GetKeyDown(KeyCode.Space)) ToggleWorld();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            string objectName = collision.gameObject.name;

            if (objectName.Equals("WaterDeathCheck"))
            {
                Time.timeScale = 0f;
                DeathScreen.SetActive(true);
                AudioManager.instance.PlayS("drown");
                AudioManager.instance.PlayM("gameOver");
            }
            else
            {
                Time.timeScale = 0f;
                DeathScreen.SetActive(true);
                AudioManager.instance.PlayS("crushed");
                AudioManager.instance.PlayM("gameOver");
            }
        }
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
            AudioManager.instance.PlayAmbiance("unreal");
        }
        else
        {
            real = true;
            Events.realWorld.Invoke();
            realWorld.SetActive(true);
            unrealWorld.SetActive(false);
            AudioManager.instance.PlayAmbiance("real");
        }
    }

    private void NewMovement()
    {
        dir = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.W))
        {
            dir.y = 1;
            anim.SetFloat("Vertical", 1);
            anim.SetFloat("Horizontal", 0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            dir.y = -1;
            anim.SetFloat("Vertical", -1);
            anim.SetFloat("Horizontal", 0);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            dir.x = -1;
            anim.SetFloat("Horizontal", -1);
            anim.SetFloat("Vertical", 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            dir.x = 1;
            anim.SetFloat("Horizontal", 1);
            anim.SetFloat("Vertical", 0);
        }

        if (dir == Vector2.zero) return;
        else anim.SetBool("Left", !anim.GetBool("Left"));

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
        }
    }

    private void Move()
    {
        nextPos = transform.position + (Vector3)dir;
        oldPos = transform.position;
        StartCoroutine(IMove());
    }
    private IEnumerator IMove()
    {
        //with velocity
        skip = true;
        moveDir = (nextPos - oldPos);
        rb.velocity = moveDir * 100f;
        while (transform.position != nextPos)
        {
            Debug.Log(moveDir + " | " + rb.velocity + "  <- STILL IN LOOP");
            yield return new WaitForSeconds(0.01f);
        }
        Debug.Log("END LOOP");
        rb.velocity = Vector2.zero;
        skip = false;


        /*
        // with transform
        skip = true;
        moveDir = (nextPos - transform.position) / 5;
        while (transform.position != nextPos)
        {
            transform.position += moveDir;
            yield return new WaitForSeconds(0.01f);
        }
        skip = false;
        */
    }

    private void Anim()
    {
        anim.SetFloat("Horizontal", dir.x);
        anim.SetFloat("Vertical", dir.y);
        anim.SetFloat("Speed", dir.sqrMagnitude);
    }

    private void StepSound()
    {
        if (stepCd <= 0)
        {
            if (rb.velocity == Vector2.zero) return;
            stepCd = 1f;
            AudioManager.instance.PlayS("footstepStone");
        }
        else
        {
            stepCd -= Time.deltaTime;
        }
    }

    private void PushSound()
    {
        switch (Random.Range(0, 4))
        {
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

    private void Movement()
    {
        //to prevent unwanted movement
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W)) dir = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.W)) dir.y++;        
        if (Input.GetKeyDown(KeyCode.S)) dir.y--;        
        if (Input.GetKeyDown(KeyCode.D)) dir.x++;
        if (Input.GetKeyDown(KeyCode.A)) dir.x--;

        if (Input.GetKeyUp(KeyCode.W)) dir.y--;
        if (Input.GetKeyUp(KeyCode.S)) dir.y++;       
        if (Input.GetKeyUp(KeyCode.D)) dir.x--;      
        if (Input.GetKeyUp(KeyCode.A)) dir.x++;

        //rb.velocity = speed * dir;

        anim.SetFloat("Horizontal", dir.x);
        anim.SetFloat("Vertical", dir.y);
        anim.SetFloat("Speed", dir.sqrMagnitude);

        if(stepCd <= 0)
        {
            if (rb.velocity == Vector2.zero) return;
            stepCd = 1f;
            AudioManager.instance.PlayS("footstepStone");
        }
        else
        {
            stepCd -= Time.deltaTime;
        }
    }
}
