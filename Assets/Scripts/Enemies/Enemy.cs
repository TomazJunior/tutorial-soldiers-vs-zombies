using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    const int SPEED_FACTOR = 10;
    internal float speed;
    private Rigidbody2D rig;


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


    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rig.velocity = new Vector2(-1, 0) * speed * SPEED_FACTOR * Time.fixedDeltaTime;
    }
}
