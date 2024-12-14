using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float bulletSpeed = 5f;

    private float nextFireTime;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        
        Bullets bulletScript = bullet.GetComponent<Bullets>();
        if (bulletScript != null)
        {
            bulletScript.direction = Vector2.left;
            bulletScript.shooterTag = gameObject.tag;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}