using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secret1 : MonoBehaviour
{
    public GameObject secret;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 position = this.transform.position;
        //if bullet and asteroid collide, create portal to secret level
        if (collision.gameObject.tag == "bullet")
        {
            Instantiate(secret, position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
