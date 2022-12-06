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

    //drop chance stuff
    public GameObject lifeUp;
    const float dropChance = 1f / 10f;  //1 in 10 chance.
    public GameObject bombUp;
    const float bombChance = 1f / 20f; //1 in 20 chance

    //Bomb detection
    public BombComm bomb;
    private bool exploded;

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

    private void Update()
    {
        BombDetected();
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
            DropLife();
            DropBomb();
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "enemybullet")
        {
            if ((this.size * 0.5f) >= this.minSize)
            {
                Split();
                Split();
            }
            FindObjectOfType<GameManager>().AsteroidCollided(this);
            Destroy(this.gameObject);
        }
        //NON PLAYER INTERACTION = NO POINTS
        /*
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
        */
        if (collision.gameObject.tag == "bomb")
        {
            exploded = true;
        }
    }
   
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bomb")
        {
            exploded = false;
        }
    }

    private void BombDetected()
    {
       
        if(exploded)
        {    
            FindObjectOfType<GameManager>().AsteroidDestroyed(this);
            DropLife();
            //Destroy(this.gameObject); 
            GameObject[] allRoids = GameObject.FindGameObjectsWithTag("asteroid");
            foreach (GameObject asteroid in allRoids)
            {
                GameObject.Destroy(asteroid);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "atmosphere")
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

    private void DropLife()
    {
        if (Random.Range(0f, 1f) <= dropChance)
        {
            Vector2 thisPosition = this.transform.position;

            Instantiate(lifeUp, thisPosition, Quaternion.Euler(0,0,0));
        }
    }

    private void DropBomb()
    {
        if (Random.Range(0f, 1f) <= dropChance)
        {
            Vector2 thisPosition = this.transform.position;

            Instantiate(bombUp, thisPosition, Quaternion.Euler(0, 0, 0));
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
