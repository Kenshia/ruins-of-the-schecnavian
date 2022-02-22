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

    private void Start()
    {
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
        stepCd = 0f;
        rb = GetComponent<Rigidbody2D>();
        realWorld.SetActive(true);
        unrealWorld.SetActive(false);
    }

    private void Update()
    {
        if (PauseMenuScript.instance.isPaused || Time.timeScale == 0f) return;
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
            }
            else
            {
                Time.timeScale = 0f;
                DeathScreen.SetActive(true);
                AudioManager.instance.PlayS("crushed");
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

        bool canMove = false;
        Collider2D[] collision = Physics2D.OverlapCircleAll((Vector2)transform.position + dir, 0.2f);
        if (collision.Length == 0)
        {
            transform.position += (Vector3)dir;
            return;
        }
        foreach (Collider2D collider in collision)
        {
            if (collider.CompareTag("MoveableObject"))
            {
                canMove = collider.GetComponent<ObjectMovement>().CheckMovement(dir);
                if (canMove)
                {
                    transform.position += (Vector3)dir;
                    PushSound();
                }
            }
            else if (collider.CompareTag("Objective"))
            {
                transform.position += (Vector3)dir;
            }
            else if (collider.CompareTag("Door"))
            {
                if (collider.GetComponent<DoorUnlocking>().CheckKey())
                    transform.position += (Vector3)dir;
            }
        }
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

        rb.velocity = speed * dir;

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
