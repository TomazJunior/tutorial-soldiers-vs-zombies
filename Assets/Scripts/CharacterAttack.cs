using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;

    internal void Attack(float projectileForce, string tag)
    {
        Projectile projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectile.collisionTag = tag;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * projectileForce, ForceMode2D.Impulse);

        Destroy(projectile.gameObject, 5);
    }
}
