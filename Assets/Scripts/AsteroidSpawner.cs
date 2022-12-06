using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    //AsteroidController script reference
    public AsteroidController asteroidPrefab;

    //changeable spawn variables
    public float rateOf = 1.0f;
    public int amountOf = 1;
    public float spawnDistance = 15.0f;
    public float trajectoryAngle = 15.0f;

    //public int timer = 5;

    private void Start()
    {
        //invokes the endless spawn of Asteroids!!!!! should tweak variables to find a good balance
        InvokeRepeating(nameof(Spawn), this.rateOf, this.rateOf);
  
    }

    //AHHH why can't i get this to work :(
    /*private void FixedUpdate()
    {
        timer--;
        if (timer <= 120 && timer > 0)
        {
            spawnDistance = 20.0f;
            Debug.Log("Asteroids Incoming!");
            
        }
        else if(timer <= 0)
        {
            timer = getRandTime();
        }
        else
        {
            spawnDistance = 150.0f;
            
        }
    }
    
    private int getRandTime()
    {
        int timerMin = 60;
        int timerMax = 600;

        return timer = Random.Range(timerMin, timerMax);
    }
    */

    private void Spawn()
    {
        for (int i = 0; i < this.amountOf; i++)
        {
            //create circle in which asteroids will spawn (attached to player- should spawn outside camera range)
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            //randomize incoming angle - should be able to go "towards" player within a range
            float variance = Random.Range(-this.trajectoryAngle, this.trajectoryAngle);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            //reference to AsteroidController prefab- actually spawns the asteroid
            AsteroidController asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);

            //reference to AsteroidController public variables + setting trajectory based on spawn point
            //spawnDirection set to negative because asteroid should come towards player!!!!
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }


  
}
