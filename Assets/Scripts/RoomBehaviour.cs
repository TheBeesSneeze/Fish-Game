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
    [Header("Camera Bounds")]

    [Tooltip("Units up the camera can travel")]
    public float CamUp;
    [Tooltip("Units down the camera can travel")]
    public float CamDown;
    [Tooltip("Units left the camera can travel")]
    public float CamLeft;
    [Tooltip("Units right the camera can travel")]
    public float CamRight;

    [Header("Settings")]
    public bool RequireSweep; // Recquires all enemies to be killed
    public Transform CameraPosition;
    public Transform PlayerStartPosition;

    [Header("Drag Enemies into this list! Very important")]
    public List<GameObject> RespawnObjects = new List<GameObject>();

    [Header("Drag doors (that are connected to this room and lead into other rooms) into this list! Very important")]
    public List<DoorBehaviour> Doors = new List<DoorBehaviour>();

    [Header("You don't need to touch this:")]
    public GameManager GameMaster; //DND REFERENCE!??!?!?!?!?!?!?!
    public bool PreviouslyCleared = false;
    private CameraController cameraControl;

    /// <summary>
    /// finds CameraController
    /// </summary>
    private void Start()
    {
        cameraControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        GameMaster = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    /// <summary>
    /// Spawns in Gorp and Globbington; respawns enemies from the inspector.
    /// Locks the doors.
    /// </summary>
    public void EnterRoom()
    {
        cameraControl.MoveCamera = false;
        GameMaster.LastRoom = GameMaster.CurrentRoom;
        GameMaster.CurrentRoom = this;

        StartCoroutine(GameMaster.SlideCamera());

        if (GameMaster.LastRoom != null)
            GameMaster.LastRoom.DespawnEnemies();
        RespawnEnemies();

        //Putting this check right after respawning all enemies was very intentional
        SetDoors( ! RoomCleared() );

        //Players will respawn after these messages!
        Invoke("RespawnPlayers", GameMaster.CameraLerpSeconds+0.1f);

        
    }

    /// <summary>
    /// Returns true / false if all the enemies have been killed
    /// OR if the players have already cleared this room.
    /// Also will just return true if room doesnt need enemies to die.
    /// </summary>
    /// <returns>true if the room is okay to leave</returns>
    public bool RoomCleared()
    {
        //if checking if dead even matters
        if( ! RequireSweep || PreviouslyCleared)
            return true;

        foreach(GameObject enemy in RespawnObjects) 
        {
            try
            {
                EnemyBehavior enemyBehavior = enemy.GetComponent<EnemyBehavior>();

                // "If enemy is alive and it needs to not be alive"
                if (enemy.activeSelf && enemyBehavior.EnemyData.RequiredToKill)
                    return false;
            }
            catch { } //bro why do i even need catch tbh
        }

        return true;
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

        cameraControl.UpdateCamera(CameraPosition.position, CamUp, CamDown, CamLeft, CamRight);
    }

    public void RespawnEnemies()
    {
        foreach (GameObject enemy in RespawnObjects)
        {
            CharacterBehavior characterBehaviorInstance = enemy.GetComponent<CharacterBehavior>();
            characterBehaviorInstance.Respawn();
        }
    }

    public void DespawnEnemies()
    {
        foreach (GameObject enemy in RespawnObjects)
        {
            enemy.SetActive(false);
        }
    }

    /// <summary>
    /// Sets all doors to open or closed
    /// </summary>
    /// <param name="Open">if door should be Open</param>
    public void SetDoors(bool Open)
    {
        foreach(DoorBehaviour door in Doors)
        {
            door.Open = Open;
        }
    }
}
