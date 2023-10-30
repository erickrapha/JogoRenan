using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBugController : MonoBehaviour
{
    [Header("Básico")]
    public int bossHealth;

    [Header("Componentes")]
    private Rigidbody2D rig;

    [Header("Movimentação")]
    public float moveSpeed;
    public float walkTime;
    private float timer;
    public bool isMovingRight = true;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        BossAttack();
    }

    void Update()
    {
        
    }

    private void BossAttack()
    {
        timer += Time.deltaTime;
        if(timer >= walkTime)
        {
            isMovingRight = !isMovingRight;
            timer = 0f;
        }

        if(isMovingRight)
        {
            transform.eulerAngles = new Vector2(0,180);
            rig.velocity = Vector2.right * moveSpeed;
        }

        else
        {
            transform.eulerAngles = new Vector2(0,0);
            rig.velocity = Vector2.left * moveSpeed;
        }
    }
}