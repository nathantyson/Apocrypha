using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    public BulletController bulletPrefab;

    private Rigidbody2D body;
    
    private bool thrust;
    public float thrustSpeed = 1.0f;

    private float rotateDirection;
    public float rotateSpeed = 1.0f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
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
    }

    private void ShootBullet()
    {
        BulletController bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Shoot(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "asteroid" || collision.gameObject.tag == "grabber")
        {
            body.velocity = Vector3.zero;
            body.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            //this function sucks, but I don't know how else I should do this. 
            FindObjectOfType<GameManager>().PlayerDie();

        }


    }
}
