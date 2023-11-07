using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class StoneBugControl : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;

    public bool isRight;
    public float speed;
    public Player playerScript;
    
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", true);

        if (isRight)
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }
    
    void Update()
    {
        MoveBug();
    }

    void MoveBug()
    {
        float moveDirection = isRight ? 1 : -1;
        Vector2 velocity = new Vector2(moveDirection * speed, rig.velocity.y);
        rig.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            isRight = !isRight;
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }

        if (col.gameObject.CompareTag("Player"))
        {
            playerScript.Damage(1);
        }
    }
    
}