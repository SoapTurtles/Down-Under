using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSpray : MonoBehaviour
{
    public float moveSpeed = 7f;
    public Rigidbody2D rb;



    public PlayerController target;
    private Vector2 moveDir;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<PlayerController>();
        moveDir = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(gameObject, 3f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            target.TakeDamage(20);
            Debug.Log("Hit!");
            Destroy(gameObject);
        }
    }
}

