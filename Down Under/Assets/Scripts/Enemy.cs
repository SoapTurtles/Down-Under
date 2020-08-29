using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float Health = 40;
    public float maxHealth = 40;

    public abstract void TakeDamage(int damage);
    public abstract void Die();




}
