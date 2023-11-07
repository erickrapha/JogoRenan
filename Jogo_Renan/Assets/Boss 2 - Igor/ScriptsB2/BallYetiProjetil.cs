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
        Destroy(gameObject, 3f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRight)
        {
            rigBall.velocity = Vector2.right * speedBall;
        }
        
        if (!isRight)
        {
            rigBall.velocity = Vector2.left * speedBall;
        }
    }

    private void OnTriggerEnter2D(Collider2D colBall)
    {
        if (colBall.gameObject.CompareTag("Player"))
        {
            colBall.gameObject.GetComponent<Player>().Damage(damageBallYeti);
        }
    }
}
