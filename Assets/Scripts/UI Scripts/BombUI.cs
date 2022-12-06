using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombUI : MonoBehaviour
{
    Text bombText;
    GameObject manager;
    GameManager getBomb;
    void Start()
    {
        bombText = GetComponent<Text>();
        manager = GameObject.FindWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        getBomb = manager.GetComponent<GameManager>();
        int bombamount = getBomb.bombs;
        bombText.text = bombamount.ToString();
    }
}
