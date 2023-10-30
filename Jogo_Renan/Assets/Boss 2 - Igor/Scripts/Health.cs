using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxLife;
    public int nowLife;

    Animator anim; 

    void Awake()
    {
        nowLife = maxLife;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    public void TakeDamage(int damage)
    {
        nowLife -= damage;
        anim.SetTrigger("Hit");

        if(nowLife >= 0)
        {
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Tiro"))
        {
            TakeDamage(4);
        }
    }
}
