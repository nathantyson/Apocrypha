using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D body;

    public float speed = 500.0f;

    public float bulletLifetime = 7.0f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector2 direction)
    {
        body.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.bulletLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "atmosphere")
        {
            Destroy(this.gameObject);
        }

    }
}
