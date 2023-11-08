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
    public bool isStage2 = false;

    [Header("Componentes")]
    public Animator animB2;
    public Rigidbody2D rigB2;

    [Header("Attack")]
    public GameObject ballYetiAtk;
    public Transform ballYetiPos;
    public bool isAtk;

    public void Start()
    {
        isAtk = true;
        animB2 = GetComponent<Animator>();
        rigB2 = GetComponent<Rigidbody2D>();
        
        StartCoroutine(AttackTimer());
    }
    void FixedUpdate()
    {
        MoveBoss();

        if (healthB2 <= 6 && !isCharging)
        {
            StartCoroutine(ChangeStageChargeSpeed());
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
    private void DieBoss()
    {
        animB2.SetTrigger("Die");
        Invoke(nameof(CarregarProxFase), 2.5f);
    }
    private IEnumerator ChangeStageChargeSpeed()
    {
        isCharging = true;
        yield return new WaitForSeconds(0.5f);
        animB2.SetTrigger("charge");
        
        yield return new WaitForSeconds(0.5f);
        moveSpeed = 0f;
        
        yield return new WaitForSeconds(0.5f);
        animB2.SetInteger("transition", 1);
        BackSpeed();
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
    IEnumerator AttackTimer()
    {
        while (true)
        {
            if (healthB2 <= 6)
            {
                isStage2 = true;
            }
            if (isStage2)
            {
                AttackB2();
                yield return new WaitForSeconds(1f);
            }
            else
            {
                AttackB2();
                yield return new WaitForSeconds(3.5f);
            }
            
        }

    }
    void AttackB2()
    {
        GameObject newBall = Instantiate(ballYetiAtk, ballYetiPos.position, ballYetiPos.rotation);
        if (isRight)
        {
            newBall.GetComponent<BallYetiProjetil>().isRight = true;
        }
        else
        {
            newBall.GetComponent<BallYetiProjetil>().isRight = false;
        }

    }
    void UptadeTextHealthB2()
    {
        b2TextHealth.text = "x " + healthB2;
    }
    void CarregarProxFase()
    {
        GameController.instance.CarregarProximaFase();
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
