using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallYetiProjetil : MonoBehaviour
{
    public bool isRight;
    public Animator animBall;
    public float speedBall;
    public int damageBallYeti = 1;
    public Rigidbody2D rigBall;
   
    void Start()
    {
        rigBall = GetComponent<Rigidbody2D>();
        animBall = GetComponent<Animator>();
        Destroy(gameObject, 2f);
        
    }

    void FixedUpdate()
    {
        if (isRight)
        {
            rigBall.velocity = Vector2.right * speedBall;
        }
        
        else
        {
            rigBall.velocity = Vector2.left * speedBall;
        }
    }

    private void OnTriggerEnter2D(Collider2D colBall)
    {
        if (colBall.gameObject.CompareTag("Player"))
        {
            colBall.gameObject.GetComponent<Player>().Damage(damageBallYeti);
            Destroy(gameObject);
        }
        
        if (colBall.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
