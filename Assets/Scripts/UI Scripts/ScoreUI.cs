using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    Text scoreText;
    GameObject manager;
    GameManager getScore;
    void Start()
    {
        scoreText = GetComponent<Text>();
        manager = GameObject.FindWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        getScore = manager.GetComponent<GameManager>();
        int scoreamount = getScore.score;
        scoreText.text = "SCORE - " + scoreamount.ToString();
    }
}
