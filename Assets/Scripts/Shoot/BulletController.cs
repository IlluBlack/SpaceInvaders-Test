using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletController : MonoBehaviour, IPooledObject
{
    [SerializeField]
    private int bulletDamage;
    [SerializeField]
    private GameObject hitFX;

    private Rigidbody2D _rigidBody;

    private float maxPosY;
    private GameObject myFX;

    private ObjectPooler pooler;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        maxPosY = ScreenController.maxPosY + 2f;

        pooler = ObjectPooler.Instance;
    }

    private void Update()
    {
        if (this.transform.position.y > maxPosY)
            Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable other = collision.GetComponent<Damageable>();

        if (other != null)
            other.TakeDamage(bulletDamage);

        Die();
    }

    private void ShowFX()
    {
        myFX = pooler.SpawnFromPool(TypeOfPool.FXBullet, transform.position, Quaternion.identity);
    }

    public void OnObjectSpawn() { }

    private void RemoveForces()
    {
        _rigidBody.velocity = Vector2.zero;
    }

    private void Die()
    {
        ShowFX();
        Disable();
    }

    private void Disable()
    {
        this.gameObject.SetActive(false);
        RemoveForces();
    }

}
