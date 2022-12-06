using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PassScript : MonoBehaviour
{
    //Initialize all images that can be displayed
    Image firstBullet;
    Image firstBomb;
    Image secondBullet;
    Image secondBomb;
    Image thirdBullet;
    Image thirdBomb;
    Image fourthBullet;
    Image fourthBomb;

    //Initialize Ticket Printouts that result from correct Passwords
    Image ticket1;
    Image ticket2;

    //Initialize list that will serve as a check for passwords
    List<int> passList = new List<int>();

    List<int> firstTicket = new List<int>();

    //Back to start condition
    private bool backToStart;
   
    
    void Start()
    {
        backToStart = false;

        //Get Image Components assigned to variables
        firstBullet = GetComponentInChildren<Transform>().Find("firstline1").GetComponent<Image>();
        firstBomb = GetComponentInChildren<Transform>().Find("firstline2").GetComponent<Image>();

        secondBullet = GetComponentInChildren<Transform>().Find("secondline1").GetComponent<Image>();
        secondBomb = GetComponentInChildren<Transform>().Find("secondline2").GetComponent<Image>();

        thirdBullet = GetComponentInChildren<Transform>().Find("thirdline1").GetComponent<Image>();
        thirdBomb = GetComponentInChildren<Transform>().Find("thirdline2").GetComponent<Image>();

        fourthBullet = GetComponentInChildren<Transform>().Find("fourthline1").GetComponent<Image>();
        fourthBomb = GetComponentInChildren<Transform>().Find("fourthline2").GetComponent<Image>();

        //Tickets assigned to variables
        ticket1 = GetComponentInChildren<Transform>().Find("ticket1").GetComponent<Image>();
        ticket2 = GetComponentInChildren<Transform>().Find("ticket2").GetComponent<Image>();
        //Disable All Images on start. probably a better way to do this, but it works!

        firstBullet.enabled = false;
        firstBomb.enabled = false;

        secondBullet.enabled = false;
        secondBomb.enabled = false;

        thirdBullet.enabled = false;
        thirdBomb.enabled = false;

        fourthBullet.enabled = false;
        fourthBomb.enabled = false;

        //Tickets un enabled
        ticket1.enabled = false;
        ticket2.enabled = false;

        //Make Pass List
        passList.Add(0);
        passList.Add(0);
        passList.Add(0);
        passList.Add(0);

        //Make Ticket checkList
        firstTicket.Add(1);
        firstTicket.Add(1);
        firstTicket.Add(1);
        firstTicket.Add(1);

    }

    //Now will check for input - using a list to determine if a spot has already been taken by a previous input
    void Update()
    {
        //Check for Bullet Input
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (passList[0] == 0)
            {
                firstBullet.enabled = true;
                passList[0] = 1;
            }
            
            else if((passList[0] != 0) && (passList[1] == 0))
            {
                secondBullet.enabled = true;
                passList[1] = 1;
            }
            
            else if ((passList[0] != 0) && (passList[1] != 0) && (passList[2] == 0))
            {
                thirdBullet.enabled = true;
                passList[2] = 1;
            }
            
            else if ((passList[0] != 0) && (passList[1] != 0) && (passList[2] != 0) && (passList[3] == 0))
            {
                fourthBullet.enabled = true;
                passList[3] = 1;
            }


            else if ((passList[0] != 0) && (passList[1] != 0) && (passList[2] != 0) && (passList[3] != 0))
            {
                passList.Add(0);
            }

        }
        //Check for Bomb Input
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetMouseButtonDown(1))
        {
            if (passList[0] == 0)
            {
                firstBomb.enabled = true;
                passList[0] = 2;
            }

            else if ((passList[0] != 0) && (passList[1] == 0))
            {
                secondBomb.enabled = true;
                passList[1] = 2;
            }

            else if ((passList[0] != 0) && (passList[1] != 0) && (passList[2] == 0))
            {
                thirdBomb.enabled = true;
                passList[2] = 2;
            }

            else if ((passList[0] != 0) && (passList[1] != 0) && (passList[2] != 0) && (passList[3] == 0))
            {
                fourthBomb.enabled = true;
                passList[3] = 2;
            }

            else if ((passList[0] != 0) && (passList[1] != 0) && (passList[2] != 0) && (passList[3] != 0))
            {
                passList.Add(0);
            }
        }
    }


    private void FixedUpdate()
    {
        if (((passList[0] == 0) && (passList[1] == 0) && (passList[2] == 0) && (passList[3] == 0)) || ((passList[0] == 0) && (passList[1] == 0) && (passList[2] == 0) && (passList[3] == 0) && (passList[4] == 0)))
        {
            return;
        }
        if (passList.Count == 5)
        {
            SceneManager.LoadScene("StartScreen");
        }

        if ((passList[0] == 1) && (passList[1] == 1) && (passList[2] == 2) && (passList[3] == 1))
        {
            ticket1.enabled = true;
            
        }
        if ((passList[0] == 2) && (passList[1] == 1) && (passList[2] == 2) && (passList[3] == 2))
        {
            ticket2.enabled = true;
        }
        

        
    }
    
}
