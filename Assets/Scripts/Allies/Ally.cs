using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour, ITakeDamage
{
    internal LifeManager lifeManager;
    private Sprite sprite;
    public Sprite Sprite
    {
        get { return sprite; }
        set
        {
            sprite = value;
            GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }

    protected virtual void Awake()
    {
        lifeManager = GetComponent<LifeManager>();
        lifeManager.OnLifeChanged += HandleLifeChenged;
    }

    private void HandleLifeChenged(object sender, float life)
    {
        if (life == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage()
    {
        lifeManager.Life--;
    }
}
