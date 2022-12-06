using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombComm : MonoBehaviour
{
    
    public Animator explode;
    private Rigidbody2D body;

    public float speed = 200f;
    public float bombLifetime = 10f;

    //public bool exploded;



    // Start is called before the first frame update
    private void Awake()
    {

        //exploded = false;
        body = GetComponent<Rigidbody2D>();
        explode = gameObject.GetComponent<Animator>();
        explode.enabled = false;
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "asteroid" || collision.gameObject.tag == "atmosphere" || collision.gameObject.tag == "enemy")
        {
            explode.enabled = true;
            ExplodeReaction();
        }
    }
    */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "asteroid" || collision.gameObject.tag == "atmosphere" || collision.gameObject.tag == "grabber" || collision.gameObject.tag == "shooter")
        {
            explode.enabled = true;
        }
    }

    public void ShootBomb(Vector2 direction)
    {
        body.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.bombLifetime);
    }

   
    private void DestroyBomb()
    {
        Destroy(this.gameObject);
    }

}
