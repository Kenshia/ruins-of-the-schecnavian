using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public bool real;
    public GameObject realWorld;
    public GameObject unrealWorld;
    private Rigidbody2D rb;
    private Vector2 dir;
    private float stepCd;
    public Animator anim;
    public GameObject DeathScreen;

    private void Start()
    {
        stepCd = 0f;
        rb = GetComponent<Rigidbody2D>();
        realWorld.SetActive(true);
        unrealWorld.SetActive(false);
    }

    private void Update()
    {
        if (PauseMenuScript.instance.isPaused) return;
        Movement();
        if (Input.GetKeyDown(KeyCode.Space)) ToggleWorld();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death")) Debug.Log("ded");

        string objectName = collision.gameObject.name;

        if (objectName.Equals("Stone"))
        {
            Debug.Log("ded by stone");
            DeathScreen.SetActive(true);
            AudioManager.instance.PlayS("crushed");
        }
        else
        {
            Debug.Log("ded by water");
            DeathScreen.SetActive(true);
            AudioManager.instance.PlayS("drown");
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
