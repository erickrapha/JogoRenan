using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss2Control : MonoBehaviour
{
    [Header("Atributos")]
    public float moveSpeed; 
    public int healthB2 = 12;
    public int damageb2 = 1;
    public Text b2TextHealth;

    [Header("Booleanos")] 
    public bool isRight;
    public bool isCharging;

    [Header("Componentes")]
    public Animator animB2;
    public Rigidbody2D rigB2;

    [Header("Attack")] 
    public float timeAttack = 4f;
    public GameObject ballYetiAtk;
    public Transform ballYetiPos;
    public bool isAtk;
    
    

    public void Start()
    {
        animB2 = GetComponent<Animator>();
        rigB2 = GetComponent<Rigidbody2D>();
        
    }

    void FixedUpdate()
    {
        MoveBoss();

        if (healthB2 <= 6 && !isCharging)
        {
            StartCoroutine(ChangeStageChargeAttackSpeed());
        }
        
        if (healthB2 <= 0)
        {
            moveSpeed = 0f;
            healthB2 = 0;
            DieBoss();
        }
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

        if (healthB2 <= 0)
        {
                return;
        }



        animB2.SetInteger("transition", 1);
    }

    private void AttackBoss()
    {
        animB2.SetInteger("Transition", 4 );
        BallYetiProjetil newBall = Instantiate(ballYetiAtk, transform.position, Quaternion.identity)
            .GetComponent<BallYetiProjetil>();
        isAtk = false;
        Invoke(nameof(TimeForAttack), timeAttack);

    }

    
    private void DieBoss()
    {
        animB2.SetTrigger("Die");
        Invoke(nameof(CarregarProxFase), 5f);
    }

    private IEnumerator ChangeStageChargeAttackSpeed()
    {
        isCharging = true;
        yield return new WaitForSeconds(0.5f);
        animB2.SetTrigger("charge");
        
        yield return new WaitForSeconds(0.5f);
        moveSpeed = 0f;
        
        yield return new WaitForSeconds(0.5f);
        animB2.SetInteger("transition", 1);
        BackSpeed();
        timeAttack = 2f;
    }
    void BackSpeed()
    {
        if (healthB2 > 6)
        {
            moveSpeed = 5f;
        }

        if (healthB2 <= 6)
        {
            moveSpeed = 8f;
        }

        
    }
    
    public void DamageB2(int dmgB2)
    {
        healthB2 -= dmgB2;
        animB2.SetTrigger("hit");
        Invoke(nameof(UptadeTextHealthB2), 0f);
        
    }

    public void TimeForAttack()
    {
        isAtk = true;
    }

    void UptadeTextHealthB2()
    {
        b2TextHealth.text = "x " + healthB2;
    }

    void CarregarProxFase()
    {
        SceneManager.LoadScene("Boss 3 - Bab");
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
            moveSpeed = 0f;
            animB2.SetTrigger("hit");
            Invoke(nameof(BackSpeed), 1f);
        }
    }


}
