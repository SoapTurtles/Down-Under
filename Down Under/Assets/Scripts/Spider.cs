using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spider : Enemy
{
    public float walkingSpeed = 5;
    public float jumpSpeed = 5;
    public float travelDistance = 5;
    private bool movingRight = true;
    private float startX;
    public GroundCheck  GC;

    private Rigidbody2D rb;

    public GameObject acid;

    private float fireRate;
    private float nextFire;
    public bool enteredRadius;

    public override void Die()
    {

    }

    public override void TakeDamage(int damage)
    {

    }

    void Start()
    {
        startX = transform.position.x;
        rb = GetComponent<Rigidbody2D>();

        fireRate = 1f;
        nextFire = Time.time;
        enteredRadius = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            enteredRadius = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            enteredRadius = false;
        }
    }
    public void FixedUpdate()
    {
        handleFixedPace();
        if (GC.isGrounded)
        {
            rb.velocity = rb.velocity + Vector2.up * jumpSpeed;
        }
    }

    private void handleFixedPace()
    {
        if (movingRight)
        {
            if(startX + travelDistance > transform.position.x)
            {
                //transform.position = transform.position + Vector3.right * walkingSpeed * Time.deltaTime;
                rb.velocity = new Vector2(walkingSpeed, rb.velocity.y);
            }
            else
            {
                movingRight = false;
            }
        }
        else
        {
            if(startX < transform.position.x)
            {
                //transform.position = transform.position - Vector3.right * walkingSpeed * Time.deltaTime;
                rb.velocity = new Vector2(-walkingSpeed, rb.velocity.y);
            }
            else
            {
                movingRight = true;
            }
        }
    }

    void Update()
    {
        FireTime();
        if(Health == 0)
        {
            Destroy(gameObject);
        }
    }

    public void FireTime()
    {
        if(Time.time > nextFire && enteredRadius == true)
        {
            Instantiate(acid, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}
