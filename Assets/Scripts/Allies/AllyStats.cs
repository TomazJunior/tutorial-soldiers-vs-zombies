using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Ally_", menuName = "ScriptableObjects/Ally")]
public class AllyStats : ScriptableObject
{
    public Sprite sprite;
    public Ally allyPrefab;
    public float power = 1;
    public float fullLife = 3;
}
