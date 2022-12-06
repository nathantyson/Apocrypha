using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretAsteroidController : MonoBehaviour
{
    public Transform toOrbit;
    Rigidbody2D bod;

    public GameObject portal;

    public float orbitSpeed;  
    public float gravityVelocity; 

    public float gravity;  
    public float initialImpulse; 
    public bool usingInitialImpulse; 
    Vector2 direction;
    void Start()
    {
        bod = GetComponent<Rigidbody2D>();
        if (usingInitialImpulse == true)
        {
            direction = toOrbit.transform.position - transform.position;
            transform.right = direction;
            bod.AddForce(transform.up * initialImpulse);
        }
    }
    void Update()
    {
        if (toOrbit.gameObject != null)
        {

            //get initial direction
            direction = toOrbit.transform.position - transform.position;

            //set debug red/green lines FIXME
            transform.right = direction;
            Debug.DrawRay(transform.position, transform.right * 100, Color.red);
            Debug.DrawRay(transform.position, transform.up * 100, Color.green);

            //gravity babyyyyyy:}
            if (usingInitialImpulse)
            {
                bod.AddForce(transform.right * gravity);
            }
            else
            {
                bod.velocity = transform.right * gravityVelocity + transform.up * orbitSpeed;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 position = this.transform.position;
        //if bullet and asteroid collide, create portal to secret level
        if (collision.gameObject.tag == "bullet")
        {
            Instantiate(portal, position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}