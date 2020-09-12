using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private float movementInputDirection;
    public float moveSpeed = 10f;
    public float jumpForce = 7f;
    private float heldJump = 0.0f;

    public float groundCheckRadius;

    [Header("Health Settings")]

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBarScript healthBar;

    private bool isRight = true;
    private bool isWalking;
    public bool isGrounded;
    private bool canJump;

    private Rigidbody2D rb;
    private Animator anim;

    public Transform groundCheck;
    public LayerMask whatIsGround;

    [Header("Dash Settings")]
    private bool isDashing;
    public float dashTime;
    public float dashSpeed;
    public float distanceBetweenImages;
    public float dashCooldown;
    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckDirection();
        UpdateAnimations();
        CheckDash();

        if (Input.GetButton("Jump"))
        {
            heldJump += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(20);
        }

        if(currentHealth == 0)
        {
            SceneManager.LoadScene("Death Screen");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(Time.time >= (lastDash + dashCooldown))
            {
                AttemptToDash();
            }
        }
    }

    public void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        AfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }

    private void CheckDash()
    {
        if (isDashing)
        {
            if(dashTimeLeft > 0)
            {
                rb.velocity = new Vector2(dashSpeed * movementInputDirection, rb.velocity.y);
                dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    AfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }
            if(dashTimeLeft <= 0)
            {
                isDashing = true;
            }

        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurrondings();
    }

    private void CheckSurrondings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void CheckDirection()
    {
        if (isRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isRight && movementInputDirection > 0)
        {
            Flip();
        }

        if(movementInputDirection != 0)
        {
            isWalking = true;
        }
        else if(movementInputDirection == 0)
        {
            isWalking = false;
        }

    }
    private void Flip()
    {
        isRight = !isRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isWalking",isWalking);
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            Jump();
        }
    }

    private void Jump()
    {  
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

    }

    private void ApplyMovement()
    {
        rb.velocity = new Vector2(moveSpeed * movementInputDirection,rb.velocity.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

}