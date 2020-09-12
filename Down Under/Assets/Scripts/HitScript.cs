using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript : MonoBehaviour
{
    public Collider2D hitDetection;
    private Animator anim;
    public bool isHitting;
    public static bool isInHitDetection;
    public static bool hit;

    private Spider enemy;
    void Start()
    {
        isHitting = false;
        isInHitDetection = false;
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Attack();

        if (isInHitDetection == true && isHitting == true)
        {
            enemy.Health -= 10;
            isHitting = false;
            hit = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            isInHitDetection = true;
            enemy = other.GetComponent<Spider>();

        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            isInHitDetection = false;
        }
    }

    public void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attacking");
            isHitting = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isHitting = false;
        }
    }
}
