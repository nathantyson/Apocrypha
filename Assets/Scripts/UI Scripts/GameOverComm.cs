using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOverComm : MonoBehaviour
{
    Animator GOText;
    Text text;
    GameObject manager;
    GameManager getLivesAmount;


    void Start()
    {
        GOText = GetComponent<Animator>();
        text = GetComponent<Text>();
        manager = GameObject.FindWithTag("GameController");
        text.enabled = false;
        GOText.enabled = false;
    }


    void Update()
    {
        getLivesAmount = manager.GetComponent<GameManager>();
        int lifeamount = getLivesAmount.lives;
         if (lifeamount <= 0)
        {
            text.enabled = true;
            GOText.enabled = true;

        }
        
    }

    public void GameOver()
    {
        SceneManager.LoadScene("StartScreen");

    }

}
