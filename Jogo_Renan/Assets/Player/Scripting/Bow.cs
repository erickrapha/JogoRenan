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

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out animator);
        TryGetComponent(out rig);
        Destroy(gameObject, 2f);
       
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
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
                //collision.GetComponent<>().Damage(damage);
                sound.Play();
                //Trocar anima��o de destruir
                animator.Play("Destroy_Bow");
                hit = true;
                //Aplicar dano no Enemy
                Destroy(gameObject, 0.75f);
            }
        }

    }

}
