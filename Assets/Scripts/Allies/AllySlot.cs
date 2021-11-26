using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySlot : MonoBehaviour
{
    private Ally ally;

    internal bool HasAlly()
    {
        return ally != null;
    }

    internal void SetAlly(AllyStats allyStats) {
        this.ally = Instantiate<Ally>(allyStats.allyPrefab, transform.position, Quaternion.identity);
        this.ally.transform.SetParent(this.transform);
        this.ally.Sprite = allyStats.sprite;        
    }
}
