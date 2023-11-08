using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public float speed;
    public int damage;
    public bool isRight;
    private Rigidbody2D rig;
    private Animator animator;
    private bool hit;
    private AudioSource sound;
    public string[] enemyTags;

    void Start()
    {
        TryGetComponent(out animator);
        TryGetComponent(out rig);
        Destroy(gameObject, 2f);
       
        sound = GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        if (hit)
        {
            rig.velocity = Vector2.zero;
            return;
        }

        if (isRight)
        {
            rig.velocity = Vector2.right * speed;
        }
        else
        {
            rig.velocity = Vector2.left * speed;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var tag in enemyTags)
        {
            if (collision.gameObject.tag == tag)
            {
                //boss bug
                if (collision.GetComponent<Health>())
                {
                    //Executa se o objeto atacado tem Heath
                    collision.GetComponent<Health>().TakeDamage(1);
                }
                //boss yeti
                if (collision.GetComponent<Boss2Control>())
                {
                    //Executa se o objeto atacado tem Heath
                    collision.GetComponent<Boss2Control>().DamageB2(1);
                }
                //boss vitoria
                if (collision.GetComponent<Health>())
                {
                    //Executa se o objeto atacado tem Heath
                    collision.GetComponent<Health>().TakeDamage(1);
                }
                sound.Play();
                //Trocar animação de destruir
                animator.Play("Destroy_Bow");
                hit = true;
                //Aplicar dano no Enemy
                Destroy(gameObject, 0.75f);
            }

        }

    }

}
