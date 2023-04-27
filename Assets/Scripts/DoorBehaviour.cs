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
    [Header("The room this door is connected too:")]
    public RoomBehaviour ThisRoom;
    [Header("The room this door leads too:")]
    public RoomBehaviour OutputRoom;

    [Header("Debug")]
    public bool Open = true;
    private GameManager gameMaster;
    private bool canEnter=true; //canEnter is just there to fix timing and prevents two players from enterring door and fucking things up. its seperate from Open
    public Collider2D MyCollider;
    public LightController DoorLight;

    [Header("Animations")]
    public Animator DoorAnimator;
    //public Animation OpenAnimation;
    //public Animation ClosedAnimation;
    //public Animation OpeningAnimation;

    private void Start()
    {
        if (Open)
            OpenDoor();
        else
            CloseDoor();

        bool previouslyOpen = Open;
        CloseDoor();
        Open = previouslyOpen;

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
            if(ThisRoom.RoomCleared() && Open)
            {
                //teleport any player that touches
                collision.gameObject.transform.position = new Vector3(9999, 9999, 0); //send one player to brazil

                if(canEnter) //this prevents the door transition from happening once
                    StartCoroutine(EnterDoorAnimation());
            }
        }
    }

    /// <summary>
    /// Detects when either player comes in contact with the door, waits, and 
    /// then teleports them both
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hi");
        ThisRoom.SetAllDoors();

        if (Open)
        {
            OnTriggerEnter2D(collision.collider);
        }
    }

    /// <summary>
    /// Smooth transition between rooms. first player dissappears. other player dissapears,
    /// Door closes. transition rooms.
    /// </summary>
    /// <param name="FirstPlayer"></param>
    private IEnumerator EnterDoorAnimation()
    {
        //Make sure this function doesnt happen again, and wait
        canEnter = false;

        yield return new WaitForSeconds(gameMaster.DoorEnterTime/2);

        //Teleport both players
        RelocatePlayers();

        yield return new WaitForSeconds(0.25f);

        //Close door:
        bool previouslyOpen = Open;
        CloseDoor();
        Open = previouslyOpen;

        yield return new WaitForSeconds(gameMaster.DoorEnterTime);

        //Transition rooms
        OutputRoom.EnterRoom();
    }

    /// <summary>
    /// Teleports all players to Brazil
    /// </summary>
    public void RelocatePlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players) 
        {
            player.transform.position = new Vector3(9999, 9999, 0);
        }
        canEnter = true;
    }

    /// <summary>
    /// Opens the door!
    /// </summary>
    public void OpenDoor()
    {
        Open = true;

        MyCollider.isTrigger = true;
     
        if (DoorAnimator != null)
            DoorAnimator.SetBool("Open", true);

        if (DoorLight != null)
        {
            DoorLight.LightRadius = 5f;
            DoorLight.enabled = true;
            DoorLight.UpdateLightRadius(1f,true);
        }
    }

    /// <summary>
    /// Closes the door!
    /// </summary>
    public void CloseDoor()
    {
        Open = false;

        MyCollider.isTrigger = false;

        if (DoorAnimator != null)
            DoorAnimator.SetBool("Open", false);

        if (DoorLight != null)
        {
            DoorLight.LightRadius = 0f;
            DoorLight.enabled = false;
            DoorLight.UpdateLightRadius(1f);
        }
    }
}
