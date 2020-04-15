using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour, IDamageable
{
    public int health;

    public virtual void TakeDamage(int damage)
    {
        health-= damage;

        if (health <= 0)
            Die();
    }

    public abstract void Die();

}
        
  
