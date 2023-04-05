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
using UnityEngine.InputSystem;

public class RoomBehavior : MonoBehaviour
{
    [Header("Settings")]
    public Transform CameraPosition;
    public Transform PlayerStartPosition;

    [Header("Unity")]
    public List<GameObject> Enemies = new List<GameObject>();
    public GameManager GameMaster; //DND REFERENCE!??!?!?!?!?!?!?!

    /// <summary>
    /// Spawns in Gorp and Globbington; respawns enemies from the inspector
    /// </summary>
    public void EnterRoom()
    {
        GameMaster.CurrentRoom = this;
        StartCoroutine(GameMaster.SlideCamera());

        Invoke("RespawnAll", GameMaster.CameraLerpSeconds+0.1f);
    }

    /// <summary>
    /// Goes through public list and resets enemies
    /// </summary>
    public void RespawnAll()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.DefaultPosition = PlayerStartPosition.position;
            player.transform.position = PlayerStartPosition.position;
        }

        foreach (GameObject enemy in Enemies)
        {
            EnemyBehavior enemyBehaviorInstance = enemy.GetComponent<EnemyBehavior>();
            enemyBehaviorInstance.Respawn();
        }

    }

}
