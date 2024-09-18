using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisorFrontalPlayer : MonoBehaviour
{
    private Rigidbody2D rigidPlayer;
    private Animator animator;
    private Collider2D colliderPlayer;
    private Collider2D colisorDamage;


    public float forcaDeRecuoDoDano;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Inimigo"))
        {
            GetComponentInParent<Player>().TakeDamage(collision, forcaDeRecuoDoDano);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Inimigo"))
        {
            GetComponentInParent<Player>().Teste(0.3f, collision);
            //Physics2D.IgnoreCollision(collision, colliderPlayer, false);
            //Physics2D.IgnoreCollision(collision, colisorDamage, false);
            //animator.SetBool("damage", false);
        }
    }

    void TakeDamage()
    {

    }
}
