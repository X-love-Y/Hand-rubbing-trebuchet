using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Move : MonoBehaviour
{
    //public Rigidbody rb;
    //public Collider co;
    //public float Speed;
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    co = GetComponent<Collider>();
    //}

    //void FixedUpdate()
    //{
    //    PlayerMove();
    //}

    //void PlayerMove()
    //{
    //    float horizontalNum = Input.GetAxisRaw("Horizontal");
    //    rb.velocity = new Vector2(horizontalNum*Speed,rb.velocity.y);
    //}
    public float moveSpeed = 5f;
    private Rigidbody rb;
    //private ObjectPreview preview;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        //preview = new ObjectPreview();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //rb.velocity = new Vector3(moveHorizontal * moveSpeed,rb.velocity.y, moveVertical * moveSpeed);
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        rb.AddForce(movement * moveSpeed);
    }

    //void OnDisable()
    //{
    //    preview.Cleanup();
    //}
}
