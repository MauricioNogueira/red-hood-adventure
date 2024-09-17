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

    public float velocidadeMovimento;

    // Start is called before the first frame update
    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        this.targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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

    void GerarNovoDestino()
    {
        this.targetPosition = transform.position + new Vector3((5f * this.direction), 0f, 0f);
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
        // Mover o inimigo em direção ao alvo
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, velocidadeMovimento * Time.deltaTime);

        // Verificar se chegou ao destino
        if (Vector3.Distance(transform.position, targetPosition) <= stopDistance)
        {
            //Debug.Log("Inimigo chegou ao destino!");
            this.direction *= -1;
            return true;
        }

        return false;
    }
}
