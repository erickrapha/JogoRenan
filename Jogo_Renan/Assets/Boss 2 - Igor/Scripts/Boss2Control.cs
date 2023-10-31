using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss2Control : MonoBehaviour
{
    [Header("Atributos")]
    public float moveSpeed; 
    public int health;

    [Header("Booleanos")] 
    public bool isRight;

    [Header("Componentes")]
    public Animator anim;
    public Rigidbody2D rigB2;
    

    public void Start()
    {
        anim = GetComponent<Animator>();
        rigB2 = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        MoveBoss();
    }

    private void MoveBoss()
    {
        if (isRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
        }
        
        if (!isRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 180);
        }
        
        
        anim.SetInteger("transition", 1);
    }

    private void AttackBoss()
    {
        
    }

    private void Regenerate()
    {
        
    }

    private void DieBoss()
    {
        health = 0;
        anim.SetTrigger("Die");
    }
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Wall"))
        {
            isRight = !isRight;
        }
    }

   


}
