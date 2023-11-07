using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Boss2Control : MonoBehaviour
{
    [Header("Atributos")]
    public float moveSpeed; 
    public int healthB2 = 12;
    public int damageb2 = 1;
    public Text b2TextHealth;

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
        Invoke(nameof(BackSpeed),0f);
    }

    private void DieBoss()
    {
        if (healthB2 == 0)
        {
            moveSpeed = 0;
            animB2.SetTrigger("Die");
        }
    }
        
    void BackSpeed()
    {
        if (healthB2 > 6)
        {
            moveSpeed = 5;
        }

        if (healthB2 <= 6)
        {
            moveSpeed = 8;
        }

        
    }
    
    public void DamageB2(int dmgB2)
    {
        healthB2 -= dmgB2;
        animB2.SetTrigger("hit");
        Invoke(nameof(UptadeTextHealthB2), 0f);
    }

    void UptadeTextHealthB2()
    {
        b2TextHealth.text = "x " + healthB2;
    }
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Wall"))
        {
            isRight = !isRight;
        }

        if (coll.gameObject.CompareTag("Player"))
        {
            coll.gameObject.GetComponent<Player>().Damage(damageb2);
        }

    }



    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Tiro"))
        {
            moveSpeed = 0;
            animB2.SetTrigger("hit");
            Invoke(nameof(BackSpeed), 1f);
        }
    }


}
