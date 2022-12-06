using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playerpickup")
        {
            Destroy(this.gameObject);
        }
    }
}
