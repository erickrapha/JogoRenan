using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("Life stuff")]
    public int maxLife;
    public int nowLife;

    public Text quant_vida;

    [Header("Componente")]
    public Animator anim; 

    [Header("AudioSource")]
    [SerializeField] private AudioSource hitSoundEffect;
    [SerializeField] private AudioSource deathSoundEffect;

    void Awake()
    {
        nowLife = maxLife;
    }

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
            Invoke("DestroyAndLoadNextScene", 2f);
        }
    }

    void DestroyAndLoadNextScene()
    {
        hitSoundEffect.Stop();
        Destroy(gameObject);
        CarregarProxFase();
    }

    void CarregarProxFase()
    {
        GameController.instance.CarregarProximaFase();
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
