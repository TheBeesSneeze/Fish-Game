/*******************************************************************************
// File Name :         RoomBehavior.cs
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

public class RoomBehaviour : MonoBehaviour
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
        GameMaster.LastRoom = GameMaster.CurrentRoom;
        GameMaster.CurrentRoom = this;
        StartCoroutine(GameMaster.SlideCamera());

        RespawnEnemies();
        //Players will respawn after these messages!
        Invoke("RespawnPlayers", GameMaster.CameraLerpSeconds+0.1f); 
    }

    /// <summary>
    /// Goes through public list and resets enemies
    /// </summary>
    public void RespawnPlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.DefaultPosition = PlayerStartPosition.position;
            player.transform.position = PlayerStartPosition.position;
        }

        if(GameMaster.LastRoom != null)
            GameMaster.LastRoom.DespawnEnemies();
    }


    public void RespawnEnemies()
    {
        foreach (GameObject enemy in Enemies)
        {
            EnemyBehavior enemyBehaviorInstance = enemy.GetComponent<EnemyBehavior>();
            enemyBehaviorInstance.Respawn();
        }
    }

    public void DespawnEnemies()
    {
        foreach (GameObject enemy in Enemies)
        {
            enemy.SetActive(false);
        }
    }

}
