using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSystemOrbit : MonoBehaviour
{
    public Transform toOrbit;
    public GameObject secret;
    Rigidbody2D bod;
    CircleCollider2D circle;

    SpriteRenderer sprite;
    RedCircleReq child;

    private bool unfreezeTime;
    private int freezeCount;


    public float orbitSpeed;
    public float gravityVelocity;

    public float gravity;
    public float initialImpulse;
    public bool usingInitialImpulse;
    Vector2 direction;
    void Start()
    {
        freezeCount = 0;
        circle = GetComponent<CircleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        child = GetComponentInChildren<RedCircleReq>();
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

        if (child.frozen)
        {
            sprite.color = Color.red;
            circle.enabled = false;
            unfreezeTime = true;
            UnfreezeTime();
        }
    }

    private void UnfreezeTime()
    {

        if (unfreezeTime)
        {
            freezeCount += 1;
            if(freezeCount == 1)
            {
                Vector2 position = this.transform.position;
                Instantiate(secret, position, Quaternion.identity);
                unfreezeTime = false;
            }
        }
    }

    
}
