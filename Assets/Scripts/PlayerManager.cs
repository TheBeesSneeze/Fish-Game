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
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;


public class PlayerManager : MonoBehaviour
{
    public GameObject Player2;

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

        while (players.Length < 2)
        {
            players = GameObject.FindGameObjectsWithTag("Player");

            if (players.Length == 1)
            {
                //Runs when 2 players
                PlayerInputManager pim = GetComponent<PlayerInputManager>();
                pim.playerPrefab = Player2;
            }

            yield return new WaitForSeconds(0.05f);
        }

        //change the world my final message goodbye
        Destroy(this.gameObject);
    }
}