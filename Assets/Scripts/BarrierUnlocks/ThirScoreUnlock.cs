using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirScoreUnlock : MonoBehaviour
{
    public int requiredScore = 500;

    CircleCollider2D barrier;
    SpriteRenderer barrierSprite;
    GameObject manager;
    GameManager getScore;
    bool canPlay = false;


    void Start()
    {
        barrier = GetComponent<CircleCollider2D>();
        barrierSprite = GetComponent<SpriteRenderer>();
        manager = GameObject.FindWithTag("GameController");
    }


    void Update()
    {
        getScore = manager.GetComponent<GameManager>();
        int scoreamount = getScore.score;
        if (scoreamount >= requiredScore)
        {
            barrier.enabled = false;
            barrierSprite.enabled = false;
            canPlay = true;
        }
        PlayVoice();
    }

    void PlayVoice()
    {
        if (canPlay)
        {
            FindObjectOfType<GameManager>().OrbBarrier();
            Destroy(this.gameObject);

        }
    }
}
