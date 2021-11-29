using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAlly : Ally
{
    [SerializeField] float fereRateInSeconds = 1;
    [SerializeField] CharacterAttack characterAttack;
    public float Power { get; set; }
    private System.DateTime lastTimeAttack;

    protected override void Awake()
    {
        base.Awake();
    }
    void FixedUpdate()
    {
        Attack();
    }

    void Attack()
    {
        System.TimeSpan timeSpan = System.DateTime.UtcNow - lastTimeAttack;
        if (timeSpan.TotalSeconds >= fereRateInSeconds)
        {
            Debug.Log("Attack");
            lastTimeAttack = System.DateTime.UtcNow;
            characterAttack.Attack(Power, "Enemy");
        }
    }
}
