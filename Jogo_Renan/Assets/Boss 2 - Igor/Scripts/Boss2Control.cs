using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Control : MonoBehaviour
{
    [Header("Atributos")]
    public float distance;
    public float speed;
    public int health;
    
    [Header("Booleanos")]
    public bool isRight;
    
    [Header("Componentes")]
    public Transform groundCheck;
    public Animator anim;
    

    
    void Update()
    {
        MoveBoss();
    }

    private void MoveBoss()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, Vector2.down, distance);

        if (ground.collider == false)
        {
            if (isRight == true)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isRight = true;
            }
        }
    }

    private void AttackBoss()
    {
        
    }

    private void Regenerate()
    {
        
    }
}
