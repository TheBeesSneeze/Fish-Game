/*******************************************************************************
// File Name :         DoorScript.cs
// Author(s) :         Jay Embry
// Creation Date :     3/30/2023
//
// Brief Description : Code that teleports Gorp and Globbington between scenes
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{

    GameObject gorp;
    GameObject globbington;

    public RoomBehavior RoomBehaviorInstance;

    /// <summary>
    /// Detects when either player comes in contact with the door, waits, and 
    /// then teleports them both
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.tag=="Player")
        {

            collision.gameObject.SetActive(false);
            Invoke("Kill", 1f);

        }

    }

    public void Kill()
    {

        gorp = GameObject.Find("Gorp");
        globbington = GameObject.Find("Globbington");

        gorp.SetActive(false);
        globbington.SetActive(false);

        RoomBehaviorInstance.Invoke("EnterRoom", 1f);
    }

}