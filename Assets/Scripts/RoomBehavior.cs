/*******************************************************************************
// File Name :         RoomScript.cs
// Author(s) :         Jay Embry, Toby Schamberger
// Creation Date :     3/30/2023
//
// Brief Description : Code that keeps track of enemies and default positions 
// of players and the camera.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehavior : MonoBehaviour
{
    [Header("Settings")]
    public Transform CameraPosition;
    public Transform PlayerStartPosition;

    [Header("Unity")]
    public List<GameObject> Enemies = new List<GameObject>();
    public GameManager GameMaster; //DND REFERENCE!??!?!?!?!?!?!?!
    private PlayerController gorpBehavior;
    private PlayerController globbingtonBehavior;

    /// <summary>
    /// Spawns in Gorp and Globbington; respawns enemies from the inspector
    /// </summary>
    public void EnterRoom()
    {

        gorpBehavior        = GameObject.Find("Gorp"       ).GetComponent<PlayerController>();
        globbingtonBehavior = GameObject.Find("Globbington").GetComponent<PlayerController>();

        //gorp.transform.position = PlayerStartPosition.position;
        //globbington.transform.position = PlayerStartPosition.position;

        gorpBehavior       .DefaultPosition = PlayerStartPosition.position;
        globbingtonBehavior.DefaultPosition = PlayerStartPosition.position;


        GameObject.Find("Gorp"       ).SetActive(true);
        GameObject.Find("globbington").SetActive(true);

        RespawnAll();

    }

    /// <summary>
    /// Goes through public list and resets enemies
    /// </summary>
    public void RespawnAll()
    {
        gorpBehavior       .Respawn();
        globbingtonBehavior.Respawn();

        for (int i = 0; i <  Enemies.Count; i++)
        {
            EnemyBehavior enemyBehaviorInstance = Enemies[i].GetComponent<EnemyBehavior>();
            enemyBehaviorInstance.Respawn();
        }

    }

}
