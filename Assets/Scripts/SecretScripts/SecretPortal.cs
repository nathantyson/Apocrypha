using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SecretPortal : MonoBehaviour
{
    public Text textToDisplay;
    private Canvas thisCanvas;

    private bool paused = false;
    private void Awake()
    {
        thisCanvas = FindObjectOfType<Canvas>();
    }

    private void Update()
    {
        if (paused == true)
        {
            Time.timeScale = 0;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 1;
                paused = false;
                Destroy(this.gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "playerpickup")
        {
            Text textAppear = Instantiate(textToDisplay) as Text;
            textAppear.transform.SetParent(thisCanvas.transform, false);
            ZAWARUDO();

        }
    }

    private void ZAWARUDO()
    {
        paused = true;
    }
}
