using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text scoreText;
    public Text totalFrutaUI;
    public static GameController instance;
    public int totalTodasAsFrutas;

    private int totalScore;
    private int totalFruta;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        totalFruta = totalTodasAsFrutas;
        totalFrutaUI.text = totalTodasAsFrutas.ToString("D2");
    }

    public void AddScore(int score)
    {
        totalScore += score;

        scoreText.text = "Score: " + totalScore.ToString("D3");
        totalFruta -= 1;
        totalFrutaUI.text = totalFruta.ToString("D2");
    }
}
