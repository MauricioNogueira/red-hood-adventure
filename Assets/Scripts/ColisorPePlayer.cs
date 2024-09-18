using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisorPePlayer : MonoBehaviour
{
    private bool isImpulso = true;
    public float forcaImpulso = 4f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Inimigo" && this.isImpulso)
        {
            Debug.Log("Pisou no inimigo");
            this.isImpulso = false;
            Rigidbody2D rbPlayer = GetComponentInParent<Rigidbody2D>();

            rbPlayer.AddForce(new Vector2(0f, forcaImpulso), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Inimigo")
        {
            Debug.Log("Deixou de pisar no inimigo");
            this.isImpulso = true;
        }
    }
}
