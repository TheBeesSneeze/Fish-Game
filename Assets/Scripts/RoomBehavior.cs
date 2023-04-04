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
    public Vector3 CameraPosition;
    public Vector3 GorpDefaultPosition;
    public Vector3 GlobbingtonDefaultPosition;

    [Header("Unity")]
    public List<GameObject> Enemies = new List<GameObject>();
    private GameObject gorp;
    private GameObject globbington;

    /// <summary>
    /// Spawns in Gorp and Globbington; respawns enemies from the inspector
    /// </summary>
    public void EnterRoom()
    {

        gorp = GameObject.Find("Gorp");
        globbington = GameObject.Find("Globbington");

        gorp.transform.position = GorpDefaultPosition;
        globbington.transform.position = GlobbingtonDefaultPosition;

        gorp.GetComponent<PlayerController>().DefaultPosition = GorpDefaultPosition;
        globbington.GetComponent<PlayerController>().DefaultPosition = GlobbingtonDefaultPosition;


        gorp.SetActive(true);
        globbington.SetActive(true);

        RespawnAll();

    }

    /// <summary>
    /// Goes through public list and resets enemies
    /// </summary>
    public void RespawnAll()
    {

        for(int i = 0; i <  Enemies.Count; i++)
        {

            EnemyBehavior enemyBehaviorInstance = Enemies[i].GetComponent<EnemyBehavior>();
            enemyBehaviorInstance.Respawn();

        }

    }

}
