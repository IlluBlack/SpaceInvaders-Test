using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    private int health = 1;

    private int initialHealth;
    private Vector2 initialPosition;

    private void Start()
    {
        initialHealth = health;
        initialPosition = this.transform.position;
    }


    private void Update()
    {
       
    }

    public void Restart()
    {
        this.gameObject.transform.position = initialPosition;
        this.health = initialHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == EnemiesController.Instance.layer)
        {
            TakeDamage(1); //at this moment player dies with just one hit with an enemy
        }
    }

    public void TakeDamage(int damage)
    {
        health-= damage;

        if (health <= 0)
            Die();
    }

    public void Die()
    {
        GameManager.Instance.FinishGame();
    }

   
}
