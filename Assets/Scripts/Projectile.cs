using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    internal string collisionTag;

    void Awake()
    {
        LevelManager.instance.OnGameOver += HandleGameOver;
    }

    void OnDestroy()
    {
        LevelManager.instance.OnGameOver -= HandleGameOver;
    }
    private void HandleGameOver(object sender, EventArgs e)
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag(collisionTag))
        {
            collider2D.GetComponent<TakeDamageManager>().TakeDamage();
            Destroy(this.gameObject);
        }
    }
}
