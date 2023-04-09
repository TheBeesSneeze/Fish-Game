/*******************************************************************************
// File Name :         DoorBehavior.cs
// Author(s) :         Jay Embry, Toby Schamberger
// Creation Date :     3/30/2023
//
// Brief Description : Code that teleports Gorp and Globbington between scenes
// Can be open or closed.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{

    [Header("Debug")]
    public bool Open = true;
    private GameManager gameMaster;
    private bool canEnter=true; //canEnter is just there to fix timing and prevents two players from enterring door and fucking things up. its seperate from Open

    [Header("The room this door is connected too:")]
    public RoomBehaviour ThisRoom;
    [Header("The room this door leads too:")]
    public RoomBehaviour OutputRoom;

    private void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    /// <summary>
    /// Detects when either player comes in contact with the door, waits, and 
    /// then teleports them both
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            if( canEnter && ThisRoom.RoomCleared() )
            {
                canEnter = false;
                collision.gameObject.transform.position = new Vector3(9999, 9999, 0); //send one player to brazil

                Invoke("Kill", gameMaster.DoorEnterTime);
                OutputRoom.Invoke("EnterRoom", gameMaster.DoorEnterTime);             
            }
            
        }

    }

    /// <summary>
    /// Teleports players to Brazil
    /// </summary>
    public void Kill()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players) 
        {
            player.transform.position = new Vector3(9999, 9999, 0);
        }
        canEnter = true;
    }

}
