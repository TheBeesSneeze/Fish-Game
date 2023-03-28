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

    void Start()
    {
        StartCoroutine("PlayerCountCheck");
        StartCoroutine(PlayerCountCheck());
    }

    /// <summary>
    /// Constantly checks how many players are present, or however constant 0.25 seconds is.
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

            yield return new WaitForSeconds(0.25f);
        }
            
        //change the world my final message goodbye
        Destroy(this.gameObject);
    }
}
