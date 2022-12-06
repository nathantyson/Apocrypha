using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCondition : MonoBehaviour
{
    GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(manager.hasOrb)
        {
            if(collision.gameObject.tag == "Player")
            {
                SceneManager.LoadScene("Secret1");
            }
        }
    }
}
