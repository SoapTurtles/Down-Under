using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 5f;
    public float jumpSpeed = 5f;
    public float JumpingSpeedFraction = 0.25f;
    public bool jumping;

    public bool dying;
    public bool dead;
    public GameObject Body;

    private float dashTime;
    public float startDashTime;
    public int direction;
    public float dashSpeed;

    private Rigidbody2D rb;
    private Animator anim;

    public GroundCheck GC;
    public float deathForce = 20;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                direction = 1;
            }
            else if(Input.GetKeyDown(KeyCode.D))
            {
                direction = 2;
            }
        }
        else
        {
            if(dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;
            }

            if(direction == 1 && Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = Vector2.left * dashSpeed;
            }
            if (direction == 2 && Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = Vector2.right * dashSpeed;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!dying && !dead)
        {
            float h = Input.GetAxisRaw("Horizontal");

            if (jumping)
            {
                h = h * JumpingSpeedFraction;
            }

            float F = ((speed * h - rb.velocity.x) / Time.deltaTime) * rb.mass;
            rb.AddForce(new Vector2(F, 0));

            if (h > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (h < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    public void Jump()
    {
        if (GC.isGrounded)
        {
            float F = ((jumpSpeed - rb.velocity.y) / Time.deltaTime) * rb.mass;
            rb.AddForce(new Vector2(0, F));
        }
        jumping = false;
    }
}
