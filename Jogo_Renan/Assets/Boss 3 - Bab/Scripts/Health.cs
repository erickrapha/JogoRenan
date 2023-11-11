using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxLife;
    public int nowLife;

    public Text quant_vida;

    public Animator anim; 

    [SerializeField] private AudioSource hitSoundEffect;
    [SerializeField] private AudioSource deathSoundEffect;

    void Awake()
    {
        nowLife = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLive(nowLife);
    }

    void FixedUpdate()
    {
        
    }

    void UpdateLive(int value)
    {
        quant_vida.text = "x " + value.ToString();
    }

    public void TakeDamage(int damage)
    {
        nowLife -= damage;
        anim.SetTrigger("isHit");

        if(nowLife <= 0)
        {
            deathSoundEffect.Play();
            anim.SetTrigger("isDead");
            Invoke("DestroyObject", 1f);
        }

    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Tiro"))
        {
            hitSoundEffect.Play();
            TakeDamage(1);
        }
    }

}