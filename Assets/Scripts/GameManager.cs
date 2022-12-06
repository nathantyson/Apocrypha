using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public PlayerShipController player;
    public ParticleSystem explode;
    public ParticleSystem boostEffect;

    public BombComm bomb;

    private AudioSource playerAudio;
    public AudioClip dieSound;
    public AudioClip lifeGet;
    public AudioClip boostGet;
    public AudioClip respawnSound;
    public AudioClip barrierBroken;
    public AudioClip orbFree;
    public AudioClip orbTaken;

    public int lives = 3;
    public int bombs = 1;
    public float respawnTime = 2.0f;
    public float invulnerabilityTime = 3.0f;

    public int score = 0;

    public bool hasOrb = false;

    private Vector3 audioDistance = new Vector3(0,0,20);

    public void PlayerBoosting()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("PlayerBoosting");
        this.boostEffect.transform.position = this.player.transform.position;
        this.boostEffect.Play();
    }
   
    public void PlayerDie()
    {
        AudioSource.PlayClipAtPoint(dieSound, this.player.transform.position);
        this.explode.transform.position = this.player.transform.position;
        this.explode.Play();
       
        
        this.lives--;

        if (this.lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), this.respawnTime);
        }


    }

    public void UseBomb()
    {
        this.bombs--;
    }

    public void GotOrb()
    {
        Debug.Log("Got Orb!");
        hasOrb = true;
        AudioSource.PlayClipAtPoint(orbTaken, Camera.main.transform.position + audioDistance);
    }
    private void Respawn()
    {
      
        this.player.gameObject.layer = LayerMask.NameToLayer("PlayerDead");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), this.invulnerabilityTime);
        AudioSource.PlayClipAtPoint(respawnSound, player.transform.position);
    }

    public void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }
    private void GameOver()
    {
        //FIX ME
    }

    public void AsteroidDestroyed(AsteroidController asteroid)
    {
        AudioSource.PlayClipAtPoint(dieSound, asteroid.transform.position);
        this.explode.transform.position = asteroid.transform.position;
        this.explode.Play();

        if (asteroid.size < 0.75f)
        {
            this.score += 25;
        }
        else if (asteroid.size < 1.2f)
        {
            this.score += 10;
        }
        else
        {
            this.score += 5;
        }
    }


    public void AsteroidCollided(AsteroidController asteroid)
    {
        AudioSource.PlayClipAtPoint(dieSound, asteroid.transform.position);
        this.explode.transform.position = asteroid.transform.position;
        this.explode.Play();

    }

    public void EnemyDestroyed(BasicEnemyAI enemy)
    {
        AudioSource.PlayClipAtPoint(dieSound, enemy.transform.position);
        this.score += 25;
        this.explode.transform.position = enemy.transform.position;
        this.explode.Play();
    }

    public void AdvancedEnemyDestroyed(EnemyMovement enemy)
    {
        AudioSource.PlayClipAtPoint(dieSound, enemy.transform.position);
        this.score += 25;
        this.explode.transform.position = enemy.transform.position;
        this.explode.Play();
    }

    public void HoopFlown(HoopController hoop)
    {
        AudioSource.PlayClipAtPoint(boostGet, player.transform.position);
        this.score += 25;
        this.explode.transform.position = hoop.transform.position;
        this.explode.Play();
    }

    public void LifeUp()
    {
        AudioSource.PlayClipAtPoint(lifeGet, player.transform.position);
        lives += 1;
    }

    public void BombUp()
    {
        AudioSource.PlayClipAtPoint(lifeGet, player.transform.position);
        bombs += 1;
    }

    public void BarrierVoice()
    {
        //AudioSource.PlayClipAtPoint(barrierBroken, player.transform.position);
        AudioSource.PlayClipAtPoint(barrierBroken, Camera.main.transform.position + audioDistance);
    }

    public void OrbBarrier()
    {
        AudioSource.PlayClipAtPoint(orbFree, Camera.main.transform.position + audioDistance);
    }
}
