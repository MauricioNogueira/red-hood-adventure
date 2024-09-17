using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float focaDeRecuoDoDano;
    public Collider2D colliderFrontal;

    private Rigidbody2D rb;
    private Animator animator;

    public LayerMask groundLayer;
    private bool isGrounded;

    public Vector2 boxSize;
    public float castDistance;

    private BoxCollider2D boxColliderPlayer;

    private bool stateAction = true;


    private Collider2D colliderInimigo;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxColliderPlayer = GetComponent<BoxCollider2D>();
        //rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    // Update is called once per frame
    void Update()
    {
        if (stateAction)
        {
            Move();
            Jump();
        }
    }


    void Move()
    {
        float movimento = Input.GetAxis("Horizontal");

        if (movimento != 0)
        {
            Vector3 novaPosicao = new Vector3 (movimento * speed * Time.deltaTime, 0f, 0f);

            transform.position += novaPosicao;

            animator.SetBool("walk", true);
            ChangeDirection(movimento);

        } else
        {
            animator.SetBool("walk", false);
        }
    }

    void Jump()
    {
        bool jumpPress = Input.GetButtonDown("Jump");

        this.isGrounded = IsGrounded();

        animator.SetBool("ground", isGrounded);

        if (isGrounded && jumpPress)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        animator.SetBool("jump", !this.isGrounded);
        animator.SetFloat("jumpTree", rb.velocity.y);
    }

    bool IsGrounded()
    {
        RaycastHit2D hit2 = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer);

        if (hit2.collider != null && rb.velocity.y == 0)
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up * castDistance, boxSize);
    }

    void ChangeDirection(float direcao)
    {
        if (direcao < 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else
        {
            gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Inimigo")
        {
            TakeDamage(collision);

            float direcao = transform.eulerAngles.y;

            float valorForca = focaDeRecuoDoDano;

            if (direcao == 180f)
            {
                valorForca *= -1;
            }

            rb.AddForce(new Vector2(valorForca, focaDeRecuoDoDano), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Inimigo")
        {
            this.colliderInimigo = collision;
            Invoke("RemoveDamage", 0.3f);
        }
    }

    void TakeDamage(Collider2D collision)
    {
        Physics2D.IgnoreCollision(collision, boxColliderPlayer, true);
        Physics2D.IgnoreCollision(collision, colliderFrontal, true);
        Debug.Log("Player tomou dano");
        animator.SetBool("damage", true);
        this.stateAction = false;
    }

    void RemoveDamage()
    {
        Physics2D.IgnoreCollision(this.colliderInimigo, boxColliderPlayer, false);
        Physics2D.IgnoreCollision(this.colliderInimigo, colliderFrontal, false);
        animator.SetBool("damage", false);
        this.stateAction = true;
        Debug.Log("Remove damage");
        this.colliderInimigo = null;
    }
}
