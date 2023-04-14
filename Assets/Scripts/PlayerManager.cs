/*******************************************************************************
// File Name :         PlayerManager.cs
// Author(s) :         Toby Schamberger
// Creation Date :     3/23/2023
//
// Brief Description : Code that detects when Gorp joins the game so the second player
// can play as Globbington.
// After Globbington joins, this code deletes itself.
//
// This code might have to be changed later! Depending on how Globbington is introduced!
*****************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public GameObject Player2;

    public ControllerPriority controllerPriority = ControllerPriority.Player2;
    public enum ControllerPriority
    {
        Player1,
        Player2,
        Neither
    }

    /// <summary>
    /// Starts the playerCountCheck() coroutine, which runs every 0.05 seconds
    /// </summary>
    void Start()
    {
        StartCoroutine(PlayerCountCheck());
    }

    /// <summary>
    /// Constantly checks how many players are present, or however constant 0.05 seconds is.
    /// Deletes this script after both players are in the game
    /// </summary>
    public IEnumerator PlayerCountCheck()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        while (players.Length<2)
        {
            players = GameObject.FindGameObjectsWithTag("Player");

            if(players.Length==1)
            {
                //Runs when 2 players
                PlayerInputManager pim = GetComponent<PlayerInputManager>();
                pim.playerPrefab = Player2;
            }

            yield return new WaitForSeconds(0.05f);
        }

        //waiiiiitttttttt
        while(players.Length<2)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            yield return new WaitForSeconds(0.2f);
        }

        //AssignController();

        //change the world my final message goodbye
        Destroy(this.gameObject);
    }

    /// <summary>
    /// If theres only one controller, it is assigned to the second player.
    /// </summary>
    private void AssignController()
    {
        PlayerController gorp = GameObject.Find("Gorp").GetComponent<PlayerController>();
        PlayerController globbington = GameObject.Find("Globbington").GetComponent<PlayerController>();

        if (Gamepad.all.Count < 2)
        {
            switch(controllerPriority)
            {
                case ControllerPriority.Player1:
                    gorp.MyGamepad = Gamepad.all[0];
                    globbington.MyGamepad = null;
                    break;
                case ControllerPriority.Player2:
                    gorp.MyGamepad = null;
                    globbington.MyGamepad = Gamepad.all[0];
                    break;
                case ControllerPriority.Neither:
                    gorp.MyGamepad = null;
                    globbington.MyGamepad = null;
                    break;
            }
        }

        else
        {

            gorp.MyGamepad        = Gamepad.all[gorp       .MyPlayerInput.playerIndex];
            globbington.MyGamepad = Gamepad.all[globbington.MyPlayerInput.playerIndex];

        }
        
    }
}
