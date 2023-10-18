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

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out animator);
        TryGetComponent(out rig);
        Destroy(gameObject, 2f);
        //Instantiate(gameObject, 1f);
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
        if (collision.gameObject.tag == "Enemy")
        {
            //collision.GetComponent<>().Damage(damage);

            //Trocar animação de destruir
            animator.Play("Destroy_Bow");
            hit = true;
            //Aplicar dano no Enemy
            Destroy(gameObject, 1);
        }

    }

}
