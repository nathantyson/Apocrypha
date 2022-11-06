using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    //array for asteroid variations
    public Sprite[] variations;

    //initialize variables
    private SpriteRenderer spriteRend;
    private Rigidbody2D rigBod;
    public float speed = 50.0f;
    public float maxLifetime = 30.0f;

    //asteroid size constraints
    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;

    private void Awake()
    {
        //attain references
        spriteRend = GetComponent<SpriteRenderer>();
        rigBod = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        //randomize from sprite array
        spriteRend.sprite = variations[Random.Range(0, variations.Length)];

        //randomize initial rotation and scale(scale will affect mass/physics)
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale =  Vector3.one * this.size;

        //set mass equal to scale size
        rigBod.mass = this.size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        rigBod.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if bullet and asteroid collide, split asteroid if above minimum size
        //PLAYER BULLET INTERACTION
        if (collision.gameObject.tag == "bullet")
        {
            if ((this.size * 0.5f) >= this.minSize)
            {
                Split();
                Split();
            }
            FindObjectOfType<GameManager>().AsteroidDestroyed(this);
            Destroy(this.gameObject);
        }
        //NON PLAYER INTERACTION = NO POINTS
        if (collision.gameObject.tag == "atmosphere" || collision.gameObject.tag == "walls")
        {
            if ((this.size * 0.5f) >= this.minSize)
            {
                Split();
                Split();
            }
            FindObjectOfType<GameManager>().AsteroidCollided(this);
            Destroy(this.gameObject);
        }
    }

    private void Split()
    {
        //maybe should mess around more with this split to make the collisions feel more natural?
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * .05f;
        AsteroidController splitOff = Instantiate(this, position, this.transform.rotation);
        splitOff.size = this.size * 0.5f;
        splitOff.SetTrajectory(Random.insideUnitCircle.normalized * this.speed);
    }
}
