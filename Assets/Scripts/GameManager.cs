using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public PlayerShipController player;
    public ParticleSystem explode;
    public int lives = 3;
    public float respawnTime = 2.0f;
    public float invulnerabilityTime = 3.0f;

    public int score = 0;

   
    public void PlayerDie()
    {
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

    private void Respawn()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("PlayerDead");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), this.invulnerabilityTime);
    }

    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }
    private void GameOver()
    {
        //FIX ME
    }

    public void AsteroidDestroyed(AsteroidController asteroid)
    {
        this.explode.transform.position = asteroid.transform.position;
        this.explode.Play();

        if (asteroid.size < 0.75f)
        {
            this.score += 100;
        }
        else if (asteroid.size < 1.2f)
        {
            this.score += 50;
        }
        else
        {
            this.score += 25;
        }
    }

    public void AsteroidCollided(AsteroidController asteroid)
    {
        this.explode.transform.position = asteroid.transform.position;
        this.explode.Play();

    }

    public void EnemyDestroyed(BasicEnemyAI enemy)
    {
        this.score += 100;
        this.explode.transform.position = enemy.transform.position;
        this.explode.Play();
    }
}
