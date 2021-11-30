using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : MonoBehaviour
{
    internal LifeManager lifeManager;
    private SpriteRenderer spriteRenderer;
    private Sprite sprite;
    public Sprite Sprite
    {
        get { return sprite; }
        set
        {
            sprite = value;
            spriteRenderer.sprite = sprite;
        }
    }

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
}
