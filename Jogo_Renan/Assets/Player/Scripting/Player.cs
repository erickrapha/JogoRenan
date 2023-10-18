using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int heath = 3;
    public float speed;
    public float Jumpforce;
    public Animator anim;
    public bool isJumping;
    public GameObject bow;
    public Transform firePoint;
    public float cooldown = 0.7f;
    public bool fireReady = true;
    public float current;
    private Rigidbody2D rig;
    private bool doubleJump;
    private bool isFire;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        GameController.instance.UpdateLives(heath);
        current = cooldown;
        fireReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        // BowFire();
        FireNotIenumerator();
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

    void FireNotIenumerator()
    {
        if (!fireReady)
        {
            current -= Time.deltaTime;

            if (current < 0)
            {
                current = cooldown;
                fireReady = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && fireReady)
        {
            fireReady = false;
            isFire = true;
            anim.SetInteger("transicao", 3);
            GameObject Bow = Instantiate(bow, firePoint.position, firePoint.rotation);

            if (transform.rotation.y == 0)
            {
                Bow.GetComponent<Bow>().isRight = true;
            }
            if (transform.rotation.y == 180)
            {
                Bow.GetComponent<Bow>().isRight = false;
            }

            Invoke("BackToTransicao", 0.25f);
            
        }
    }
    void BackToTransicao()
    {
        isFire = false;
        if (isJumping)
        {
            anim.SetInteger("transicao", 2);
        }
        else
        {
             anim.SetInteger("transicao", 0);
        }
       
    }
    IEnumerator Fire()
    {
        if (!fireReady)
        {
            current -= Time.deltaTime;

            if (current < 0)
            {
                current = cooldown;
                fireReady = true;
            }
        }
        

        if (Input.GetKeyDown(KeyCode.E) && fireReady)
        {
            fireReady = false;
            isFire = true;
            anim.SetInteger("transicao", 3);
            GameObject Bow = Instantiate(bow, firePoint.position, firePoint.rotation);

            if (transform.rotation.y == 0)
            {
                //Bow.transform.eulerAngles = new Vector3(0, 0, 0);
                Bow.GetComponent<Bow>().isRight = true;
            }
            if (transform.rotation.y == 180)
            {
                //Bow.transform.eulerAngles = new Vector3(0, 180, 0);
                Bow.GetComponent<Bow>().isRight = false;
            }

            yield return new WaitForSeconds(0.25f);
            isFire = false;
            anim.SetInteger("transicao", 0);
        }
    }
    public void Damage(int dmg)
    {
        heath -= dmg;
        GameController.instance.UpdateLives(heath);

        if (heath <= 0)
        {
            //Chamar o game over

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
