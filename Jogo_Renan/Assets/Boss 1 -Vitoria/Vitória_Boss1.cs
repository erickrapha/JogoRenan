using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitória_Boss1 : MonoBehaviour
{
    public float speed;
    //public float walkTime;
    public float timer;
    public int health = 10;
    private bool walkRight;

    private Animator anim;
    private Rigidbody2D rig;
   
    void Start()
    {
        rig = GetComponent< Rigidbody2D >();
        anim = GetComponent< Animator >();
    }
    void FixedUpdate()
    {
        if (walkRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 180);
        }
        if (!walkRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
        }

    }
    public void Damage(int dmg)
    {
        health -= dmg;
        anim.SetTrigger("Hit");
        if (health <= 0)
        {
            GameController.instance.CarregarProximaFase();
            Destroy(gameObject);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            walkRight = !walkRight;
        }

    }

}
