using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField]
    private Transform fireFrom;

    public float bulletImpulse = 20f;

    private ObjectPooler pooler;

    private void Start()
    {
        pooler = ObjectPooler.Instance;
    }

    private void Update()
    {
        if (GameManager.Instance.IsPlaying())
        {
            if (Input.GetButton("Fire1"))
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        GameObject bullet = pooler.SpawnFromPool(TypeOfPool.PlayerBullets, fireFrom.position, fireFrom.rotation);

        Rigidbody2D bullet_rb = bullet.GetComponent<Rigidbody2D>();

        if(bullet_rb != null)
        {
            bullet_rb.AddForce(fireFrom.up * bulletImpulse, ForceMode2D.Impulse);
        }
    }
}
