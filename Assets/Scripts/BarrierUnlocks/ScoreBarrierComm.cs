using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBarrierComm : MonoBehaviour
{
    public int requiredScore = 100;
    public PlayerShipController player;
    BoxCollider2D barrier;
    SpriteRenderer barrierSprite;
    GameObject manager;
    GameManager getScore;

    private AudioSource source;
    private bool canPlay = false;

    void Start()
    {
        barrier = GetComponent<BoxCollider2D>();
        barrierSprite = GetComponent<SpriteRenderer>();
        manager = GameObject.FindWithTag("GameController");
        source = GetComponent<AudioSource>();
    }

    

    void Update()
    {
        getScore = manager.GetComponent<GameManager>();
        int scoreamount = getScore.score;
        if(scoreamount >= requiredScore)
        {
            barrier.enabled = false;
            barrierSprite.enabled = false;
            canPlay = true;
        }
        PlayVoice();
    }

    void PlayVoice()
    {
        if(canPlay)
        {
            FindObjectOfType<GameManager>().BarrierVoice();
            Destroy(this.gameObject);
            
        }
    }
}
