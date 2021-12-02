using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_", menuName = "ScriptableObjects/Enemy")]
public class EnemyStats : ScriptableObject
{
    public float speed;
    public Sprite sprite;
    public float distanceToAttack;
    public float power = 1;
    public float fullLife = 3;
    public int coins = 1;
}
