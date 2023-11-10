using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidade = 10f; 
    public Vector2 direcao = Vector2.right; 
    public Player player;

    void Update()
    {
        transform.Translate(direcao * velocidade * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player.Damage(1);
            Destroy(gameObject, 0.1f);
        }
    }
}