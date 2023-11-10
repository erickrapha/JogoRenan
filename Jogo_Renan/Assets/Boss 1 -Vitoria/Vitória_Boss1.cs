using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Vitória_Boss1 : MonoBehaviour
{
    public float speed;
    private bool walkRight;

    public int health = 5;
    public float timer;

    public GameObject balaPrefab; 
    private Animator anim;
    private Rigidbody2D rig;
    public Player player;
    public Text vida;
    public Transform firePoint;

    public float tempoEntreTiros = 1f;
    private float tempoUltimoTiro; 

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        ControllerVitoria.instance.UpdateLives(health);
        tempoUltimoTiro = Time.time; 
    }

    void FixedUpdate()
    {
        UpdateHealthText();

        if (walkRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 180);
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (health <= 5)
        {
            if (Time.time - tempoUltimoTiro >= tempoEntreTiros)
            {
                SpawnObject(); 
                tempoUltimoTiro = Time.time; 
            }
        }
    }

    void SpawnObject()
    {
        
        anim.SetInteger("transition", 1);
        GameObject bala = Instantiate(balaPrefab, firePoint.position, Quaternion.identity);
        
        Bala balaScript = bala.GetComponent<Bala>();
        if (balaScript != null)
        {
            balaScript.direcao = firePoint.right;
        }
    }


    public void Damage(int dmg)
    {
        health -= dmg;
        anim.SetTrigger("Hit");

        if (health <= 0)
        {
            Destroy(gameObject, 0.3f);
            anim.SetInteger("transition", 3);
        }

        UpdateHealthText();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            walkRight = !walkRight;
        }

        if (col.gameObject.CompareTag("Player"))
        {
            player.Damage(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Tiro"))
        {
            Damage(1);
        }
    }

    void UpdateHealthText()
    {
        if (vida != null)
        {
            vida.text = "Vida: " + health.ToString();
        }
    }
}
