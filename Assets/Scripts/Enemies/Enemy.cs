using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] LayerMask allyLayer;
    [SerializeField] float fereRateInSeconds = 1;
    [SerializeField] CharacterAttack characterAttack;
    internal LifeManager lifeManager;

    const int SPEED_FACTOR = 10;
    internal float speed;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rig;

    private System.DateTime lastTimeAttack;
    
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

    public float DistanceToAttack { get; set; }
    public float Power { get; set; }
    public int Coins { get; set; }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();
        lifeManager = GetComponent<LifeManager>();
        lifeManager.OnLifeChanged += HandleLifeChanged;
    }

    private void HandleLifeChanged(object sender, float life)
    {
        if (life == 0)
        {
            LevelManager.instance.RemoveEnemy(this);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector2.left, DistanceToAttack, allyLayer);
        if (raycastHit2D.collider != null)
        {
            rig.velocity = Vector2.zero;
            Attack();
        }
        else
        {
            rig.velocity = Vector2.left * speed * SPEED_FACTOR * Time.fixedDeltaTime;
        }
    }

    void Attack()
    {
        System.TimeSpan timeSpan = System.DateTime.UtcNow - lastTimeAttack;
        if (timeSpan.TotalSeconds >= fereRateInSeconds)
        {
            lastTimeAttack = System.DateTime.UtcNow;
            characterAttack.Attack(Power, "Ally");
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(-DistanceToAttack, 0));
    }
}
