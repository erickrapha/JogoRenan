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
    private bool isFire;

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
        BowFire();
    }
    void Move()
    {
        float movement = Input.GetAxis("Horizontal");
        //Debug.Log(movement);
        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        if (movement > 0)
        {
            if (!isJumping)
            {
                anim.SetInteger("transicao", 1);
            }
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (movement < 0)
        {
            if (!isJumping)
            {
                anim.SetInteger("transicao", 1);
            }
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if(movement == 0 && !isJumping && !isFire)
        {
            anim.SetInteger("transicao", 0);

        }

    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                doubleJump = true;
                isJumping = true;
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
    void BowFire()
    {
        StartCoroutine("Fire");
    }

    IEnumerator Fire()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isFire = true;
            anim.SetInteger("transicao", 3);
            yield return new WaitForSeconds(0.25f);
            anim.SetInteger("transicao", 0);
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
