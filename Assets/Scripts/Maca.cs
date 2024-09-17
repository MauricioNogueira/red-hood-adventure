using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.UI;

public class Maca : MonoBehaviour
{
    public int score;

    private CircleCollider2D cc2D;
    private SpriteRenderer spriteRendererMaca;
    public GameObject smokeMaca;
    // Start is called before the first frame update
    void Start()
    {
        cc2D = GetComponent<CircleCollider2D>();
        spriteRendererMaca = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameController.instance.AddScore(score);

            ChangeAppleToSmoke();

            Destroy(gameObject, 0.12f);
        }
    }

    private void ChangeAppleToSmoke()
    {
        cc2D.enabled = false;
        spriteRendererMaca.enabled = false;
        smokeMaca.SetActive(true);
    }
}
