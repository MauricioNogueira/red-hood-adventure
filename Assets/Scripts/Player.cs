using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public Collider2D colliderFrontal;

    private Rigidbody2D rb;
    private Animator animator;

    public LayerMask groundLayer;
    private bool isGrounded;

    public Vector2 boxSize;
    public float castDistance;

    private BoxCollider2D boxColliderPlayer;

    private bool stateAction = true;
    private SpriteRenderer spriteRenderer;

    public static Player player;
    public bool invencibilidade = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxColliderPlayer = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = this;
        //rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    // Update is called once per frame
    void Update()
    {
        //BoxHead();
        if (stateAction)
        {
            Move();
            Jump();
        }
    }


    void Move()
    {
        float movimento = Input.GetAxis("Horizontal");

        //ColisaoRaycast(movimento);

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
            //rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            Impulso(0f, jumpForce);
        }

        animator.SetBool("jump", !this.isGrounded);
        animator.SetFloat("jumpTree", rb.velocity.y);
    }

    bool IsGrounded()
    {
        RaycastHit2D hit2 = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer);

        //Debug.DrawRay(transform.position - transform.up * castDistance, -transform.up, Color.red);

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /**if (collision.gameObject.CompareTag("CabecaInimigo"))
        {
            
            CaoPlanta c = collision.gameObject.GetComponentInParent<CaoPlanta>();
            Debug.Log(c);
        }*/
    }

    public void TakeDamage(Collider2D collision, float forcaDeRecuoDoDano, int dir)
    {
        //float direcao = transform.eulerAngles.y;

        float valorForca = forcaDeRecuoDoDano;

        valorForca *= dir;

        Vector2 forcaImpulso = new Vector2(valorForca, forcaDeRecuoDoDano);
        //Impulso(valorForca, forcaDeRecuoDoDano);
        Debug.Log("Player tomou dano");
        animator.SetBool("damage", true);
        this.stateAction = false;

        StartCoroutine(RemoverAcaoDano(0.3f, collision));

    }

    void RemoveDamage(Collider2D collision)
    {
        animator.SetBool("damage", false);
        this.stateAction = true;
        DefesaTemporaria(3f);
    }

    IEnumerator RemoverAcaoDano(float time, Collider2D collision)
    {
        yield return new WaitForSeconds(time);

        RemoveDamage(collision);
    }

    public void Teste(float time, Collider2D collision)
    {
        StartCoroutine(RemoverAcaoDano(time, collision));
    }

    public void Impulso(float x, float y)
    {
        rb.AddForce(new Vector2(x, y), ForceMode2D.Impulse);
    }


    IEnumerator InvencibilidadeTemporaria(float delay)
    {
        invencibilidade = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        yield return new WaitForSeconds(delay);
        invencibilidade = false;
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }

    public void DefesaTemporaria(float time)
    {
        StartCoroutine(InvencibilidadeTemporaria(time));
    }

    public GameObject GetColliderFoot()
    {
        return GameObject.FindGameObjectWithTag("Pe");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
    }

    public Rigidbody2D GetRigidbody2D()
    {
        return rb;
    }
}
