using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public bool real;
    public PauseMenuScript PauseMenu;
    private Rigidbody2D rb;
    private Vector2 dir;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) && !PauseMenu.isPaused) ToggleWorld();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death")) Debug.Log("ded");
    }

    private void ToggleWorld()
    {
        AudioManager.instance.PlayS("toggleWorld");
        if (real)
        {
            real = false;
            Events.unrealWorld.Invoke();
        }
        else
        {
            real = true;
            Events.realWorld.Invoke();
        }
    }

    private void Movement()
    {
        //to prevent unwanted movement
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W)) rb.velocity = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.W)) dir.y++;
        if (Input.GetKeyDown(KeyCode.S)) dir.y--;
        if (Input.GetKeyDown(KeyCode.D)) dir.x++;
        if (Input.GetKeyDown(KeyCode.A)) dir.x--;

        if (Input.GetKeyUp(KeyCode.W)) dir.y--;
        if (Input.GetKeyUp(KeyCode.S)) dir.y++;
        if (Input.GetKeyUp(KeyCode.D)) dir.x--;
        if (Input.GetKeyUp(KeyCode.A)) dir.x++;
        rb.velocity = speed * dir;
    }
}
