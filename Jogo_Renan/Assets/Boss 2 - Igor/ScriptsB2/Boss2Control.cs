using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss2Control : MonoBehaviour
{
    [Header("Atributos")]
    public float moveSpeed; 
    public int healthB2;

    [Header("Booleanos")] 
    public bool isRight;

    [Header("Componentes")]
    public Animator animB2;
    public Rigidbody2D rigB2;
    

    public void Start()
    {
        animB2 = GetComponent<Animator>();
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
        
        
        animB2.SetInteger("transition", 1);
    }

    private void AttackBoss()
    {
        
    }

    private void AlterandoEstágio()
    {
        
    }

    private void DieBoss()
    {
        healthB2 = 0;
        animB2.SetTrigger("Die");
    }
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Wall"))
        {
            isRight = !isRight;
        }
    }

    public void DamageB2(int dmgB2)
    {
        healthB2 -= dmgB2;
        animB2.SetTrigger("hit");
    }


}
