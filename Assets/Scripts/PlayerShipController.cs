using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    public BulletController bulletPrefab;
    public BombComm bombPrefab;

    private AudioSource audiosource;
    public AudioClip shootSound;
    public AudioClip dieSound;


    private Rigidbody2D body;
    
    private bool thrust;
    public float thrustSpeed = 1.0f;

    private float rotateDirection;
    public float rotateSpeed = 1.0f;

    //Hoop Boosting
    public float hoopBoost = 3.0f;
    private float boostTimer = 0;
    private bool isBoosting;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        audiosource = GetComponent<AudioSource>();
        isBoosting = false;
    }
    private void Update()
    {
        thrust = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rotateDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rotateDirection = -1.0f;
        }
        else
        {
            rotateDirection = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            ShootBullet();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetMouseButtonDown(1))
        {
            ShootBomb();
        }

    }

    private void FixedUpdate()
    {
        if (thrust)
        {
            body.AddForce(this.transform.up * this.thrustSpeed);
        }

        if (rotateDirection != 0.0f)
        {
            body.AddTorque(rotateDirection * this.rotateSpeed);
        }

        if (isBoosting)
        {
            body.AddForce(this.transform.up * this.hoopBoost);
            FindObjectOfType<GameManager>().PlayerBoosting();
            boostTimer += Time.deltaTime;
            if(boostTimer >= 4)
            {
                isBoosting = false;
                boostTimer = 0;
                FindObjectOfType<GameManager>().TurnOnCollisions();
            }
        }
    }

    private void ShootBullet()
    {
        BulletController bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        audiosource.PlayOneShot(shootSound, .01f);
        bullet.Shoot(this.transform.up);
        
    }

    private void ShootBomb()
    {
        if (FindObjectOfType<GameManager>().bombs > 0)
        {
            BombComm bomb = Instantiate(this.bombPrefab, this.transform.position, this.transform.rotation);
            audiosource.PlayOneShot(shootSound, .01f);
            bomb.ShootBomb(this.transform.up);
            FindObjectOfType<GameManager>().UseBomb();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "asteroid" || collision.gameObject.tag == "grabber" || collision.gameObject.tag == "planet" || collision.gameObject.tag == "enemybullet")
        {
            
            body.velocity = Vector3.zero;
            body.angularVelocity = 0.0f;
            FindObjectOfType<GameManager>().PlayerDie();
            this.gameObject.SetActive(false);

            //this function sucks, but I don't know how else I should do this. 
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "hoop")
        {
            isBoosting = true;
        }

        if (collision.gameObject.tag == "life")
        {
            FindObjectOfType<GameManager>().LifeUp();
        }

        if (collision.gameObject.tag == "extraBomb")
        {
            FindObjectOfType<GameManager>().BombUp();
        }
    }
}
