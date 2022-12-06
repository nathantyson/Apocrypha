using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public BulletController bulletPrefab;
    
    public Transform player;
    private AudioSource audiosource;
    public AudioClip shootSound;

    private bool isShooting;
    private bool readyTofire;
    
    private float rotateSpeed = 90;

    private void Start()
    {

        audiosource = GetComponent<AudioSource>();

 
        readyTofire = false; 
    }

    private void Update()
    {
        Vector3 dir = player.position - transform.position;
        dir.Normalize();
        float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        Quaternion desiredRotate = Quaternion.Euler(0, 0, zAngle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotate, rotateSpeed * Time.deltaTime);

        if (readyTofire == true)
        {
            if (isShooting) return;

            StartCoroutine(FireRate());
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            readyTofire = true;
   
        }
        else
        {
            readyTofire = false;
  
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //BOMB AND BULLET INTERACTION
        if (collision.gameObject.tag == "bullet" || collision.gameObject.tag == "bomb")
        {

            FindObjectOfType<GameManager>().AdvancedEnemyDestroyed(this);
            Destroy(this.gameObject);

        }
    }

    void StartShooting()
    {
        BulletController bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        audiosource.PlayOneShot(shootSound, .01f);
        bullet.Shoot(this.transform.up);
    }

    
    IEnumerator FireRate()
    {
        if (isShooting) yield break;

        isShooting = true;
        StartShooting();
        yield return new WaitForSeconds(2f);
        isShooting = false;
        
    }

}
