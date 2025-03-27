using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnim : MonoBehaviour
{
    private Animator anim;
    public GameObject stone;
    public Vector3 forceDirection;
    public float force = 10f;
    public Transform tr;

    [SerializeField] private GameObject targetObj;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.E))
        {
            anim.SetBool("Attack", true);
        }
    }

    public void ShengSet() 
    {
        targetObj.SetActive(false);
    }
    public void AttackSetFalse() 
    {
        anim.SetBool("Attack", false);
        targetObj.SetActive(true);

    }

    public void ProduceStone()
    {
        Vector3 spawnPosition = tr.position;

        GameObject newStone = Instantiate(stone, spawnPosition, Quaternion.identity);

        Rigidbody rb = newStone.GetComponent<Rigidbody>();
        if(rb == null)
        {
            rb = newStone.GetComponent<Rigidbody>();
        }

        rb.AddForce(forceDirection * force, ForceMode.Impulse);
    }

}
