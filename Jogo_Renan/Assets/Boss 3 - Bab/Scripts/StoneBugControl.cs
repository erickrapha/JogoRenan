using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class StoneBugControl : MonoBehaviour
{
    [Header("Componentes")]
    private Rigidbody2D rig;
    private Animator anim;
    public Player playerScript;
    
    [Header("Atributos")]
    public int speed;
    public int health = 10;

    [Header("Movimentação")]
    public bool isLeft;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (isLeft)
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
        float moveDirection = isLeft ? 1 : -1;
        Vector2 velocity = new Vector2(moveDirection * speed, rig.velocity.y);
        rig.velocity = velocity;
        anim.SetInteger("value",1);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            isLeft = !isLeft;
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
        if (col.gameObject.CompareTag("Player"))
        {
            playerScript.Damage(1);
        }
        
    }
    public void Damage(int dmg)
    {
        health -= dmg;
        
        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }

}