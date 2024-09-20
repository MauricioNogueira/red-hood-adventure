using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CaoPlanta : MonoBehaviour
{
    private Rigidbody2D rg2d;
    private Vector3 targetPosition;
    private Animator animator;
    private int direction = -1;
    private float stopDistance = 0.1f;
    private Collider2D boxColliderBody;
    private bool stateAction = true;
    private SpriteRenderer spriteRenderer;

    public float velocidadeMovimento;
    public float distanciaAPercorrer = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxColliderBody = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (stateAction)
        {
            Move();
        }
    }

    void Move()
    {
        if (isDestino())
        {
            GerarNovoDestino();
            animator.SetBool("walk", false);
            //Debug.Log("Reecalculando rota de destino");
            //Debug.Log("Ponto atual: " + transform.position.x);
            //Debug.Log("Ponto destino: " + this.targetPosition.x);
        }
        else
        {
            animator.SetBool("walk", true);
            transform.position += new Vector3((velocidadeMovimento * Time.deltaTime * this.direction), 0f, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !Player.player.invencibilidade)
        {
            Player.player.TakeDamage(collision.collider, 3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pe")
        {
            //Die();
            Debug.Log("Inimigo levou dano e impulso no inimigo");
            Player.player.Impulso(0f, 8f);
            Player.player.DefesaTemporaria(0.3f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    void GerarNovoDestino()
    {
        this.targetPosition = transform.position + new Vector3((distanciaAPercorrer * this.direction), 0f, 0f);
        ChangeDirectionSprite();
        //Debug.Log("Direção atual do cão planta: " + this.direction);
    }

    void ChangeDirectionSprite()
    {
        //Debug.Log("Direção atual do cão planta: " + this.direction);
        if (this.direction < 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    bool isDestino()
    {
        // Verificar se chegou ao destino
        if (Vector3.Distance(transform.position, targetPosition) <= stopDistance)
        {
            //Debug.Log("Inimigo chegou ao destino!");
            this.direction *= -1;
            return true;
        }

        return false;
    }

    void AnimationDamage()
    {
        boxColliderBody.enabled = false;
        spriteRenderer.enabled = false;

        Destroy(gameObject, 2f);
    }

    void Die()
    {
        animator.SetBool("damage", true);
        stateAction = false;
        Invoke("AnimationDamage", 0.3f);
    }
}
