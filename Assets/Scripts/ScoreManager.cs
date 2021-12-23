using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int totalScore;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "0";
        Observer.score += UpdateScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateScore(int score)
    {
        totalScore += score;
        scoreText.text = totalScore.ToString();
    }
}
