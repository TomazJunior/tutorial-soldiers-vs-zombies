using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    internal string collisionTag;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag(collisionTag))
        {
            collider2D.GetComponent<ITakeDamage>().TakeDamage();
            Destroy(this.gameObject);
        }
    }
}
