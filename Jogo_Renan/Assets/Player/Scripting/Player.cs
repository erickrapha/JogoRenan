using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float Jumpforce;
    public Animator anim;
    public bool isJumping;
    private Rigidbody2D rig;
    private bool doubleJump;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }
    void Move()
    {
        float movement = Input.GetAxis("Horizontal");
        //Debug.Log(movement);
        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        if (movement > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (movement < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if(rig.velocity.x != 0)
        {
            anim.SetInteger("transicao", 1);
        }
        else if (rig.velocity == Vector2.zero)
        {
            anim.SetInteger("transicao", 0);
        }


    }
    void Jump()
    {
        if (!isJumping)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                doubleJump = true;
                rig.AddForce(new Vector2(0, Jumpforce), ForceMode2D.Impulse);
                anim.SetInteger("transicao", 2);
            }
            else
            {
                if (doubleJump)
                {
                    rig.AddForce(new Vector2(0, Jumpforce * 2), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }

        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isJumping = false;
        }
    }

}
