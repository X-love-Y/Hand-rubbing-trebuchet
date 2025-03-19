using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody rb;
    public Collider co;
    public float Speed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        co = GetComponent<Collider>();
    }

    void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        float horizontalNum = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalNum*Speed,rb.velocity.y);
    }
}
