using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    public float rotateSpeed = 90f;
    public float moveSpeed = 10f;
    public Transform player;

    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = player.position - transform.position;
        dir.Normalize();
        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        Quaternion desiredRotate = Quaternion.Euler(0, 0, zAngle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotate, rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AttackPlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //PLAYER AND BULLET INTERACTION
        if (collision.gameObject.tag == "bullet" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "bomb")
        {
            
            FindObjectOfType<GameManager>().EnemyDestroyed(this);
            Destroy(this.gameObject);

        }
    }

        private void AttackPlayer()
    {
        float step = moveSpeed * Time.deltaTime;
        Vector2 playerPos = new Vector2(player.position.x, player.position.y);
        transform.position = Vector2.MoveTowards(transform.position, playerPos, step);
    }
}
