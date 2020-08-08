using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5f;
    public float jumpSpeed = 5f;
    public float JumpingSpeedFraction = 0.25f;
    public bool jumping;

    public bool dying;
    public bool dead;
    public GameObject Body;

    private Rigidbody2D rb;
    private Animator anim;

    public GroundCheck GC;
    public float deathForce = 20;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dying && !dead)
        {
            if (rb.velocity.x != 0) anim.SetBool("walking", true);
            else anim.SetBool("walking", false);

            if (GC.isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("Jumping", true);
            }
        }
        else if (dying)
        {
            anim.SetTrigger("Dead");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q Key Pressed");
            Die();
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

    public void Die()
    {
        dying = false;
        dead = true;
        int childCount = Body.transform.childCount;
        Destroy(GetComponent<Animator>());
        for (int i = childCount - 1; i >= 0; i -= 1)
        {
            Transform child = Body.transform.GetChild(i);
            child.parent = null;
            child.gameObject.AddComponent<Rigidbody2D>();
            child.GetComponent<Collider2D>().enabled = true;

            Vector2 destructVector = (child.position - transform.position);
            child.GetComponent<Rigidbody2D>().AddForce(destructVector * deathForce);

        }
        Body.transform.parent = null;
        Body.GetComponent<Collider2D>().enabled = true;
        Body.AddComponent<Rigidbody2D>();
        Destroy(gameObject);

    }




}
