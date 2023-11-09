using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : MonoBehaviour
{
    public Vector3 direction;
    public float vel;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(vel * Time.deltaTime * direction);
    }

    public void IniciarTiro(bool isRight)
    {
        if(isRight == true)
        {
            direction = Vector3.right;
        }
        else
        {
            direction = Vector3.left;
        }
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Player playerScript = col.gameObject.GetComponent<Player>();
            playerScript.Damage(1);
        }
        Destroy(gameObject);
    }
}
