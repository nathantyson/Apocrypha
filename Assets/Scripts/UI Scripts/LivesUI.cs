using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    Text lifeText;
    GameObject manager;
    GameManager getLife;
    void Start()
    {
        lifeText = GetComponent<Text>();
        manager = GameObject.FindWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        getLife = manager.GetComponent<GameManager>();
        int lifeamount = getLife.lives;
        lifeText.text = lifeamount.ToString();
    }
}
