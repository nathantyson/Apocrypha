using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecScoreBarrier : MonoBehaviour
{
    public int requiredScore = 200;

    BoxCollider2D barrier;
    SpriteRenderer barrierSprite;
    GameObject manager;
    GameManager getScore;

    private bool canPlay = false;

    void Start()
    {
        barrier = GetComponent<BoxCollider2D>();
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
            FindObjectOfType<GameManager>().BarrierVoice();
            Destroy(this.gameObject);

        }
    }
}
