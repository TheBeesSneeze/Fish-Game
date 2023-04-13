/*******************************************************************************
// File Name :         GameManager.cs
// Author(s) :         Jay Embry, Toby Schamberger
// Creation Date :     3/30/2023
//
// Brief Description : Code that keeps track of game state. Slides camera when
// enterring new rooms.
// Contains the function for players to swap controllers / characters. (the
// input is in PlayerController)
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    public float CameraLerpSamples;
    public float CameraLerpSeconds;

    public float DoorEnterTime;
    public float DoorTransitionTime;

    [Header("Unity Stuff")]
    private GameObject _camera;
    public RoomBehaviour CurrentRoom;
    public RoomBehaviour LastRoom;

    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    /// <summary>
    /// Smoothly transitions the camera from its old positon to CurrentRoom.CameraPosition
    /// </summary>
    /// <returns></returns>
    public IEnumerator SlideCamera()
    {
        Vector3 oldCameraPosition = _camera.transform.position;
        float i = 0;

        while(i < CameraLerpSamples) 
        {
            _camera.transform.position = Vector3.Lerp(oldCameraPosition, CurrentRoom.CameraPosition.position, i/CameraLerpSamples);
            i++;
            yield return new WaitForSeconds(CameraLerpSeconds / CameraLerpSamples);
        }
        
    }

    /// <summary>
    /// Lets your younger brother take a spin as gorp
    /// </summary>
    public void SwapPlayers()
    {
        Debug.Log("Swapping players!");

        PlayerInput gorp = null;
        PlayerInput glob = null;
        try { gorp = GameObject.Find("Gorp")       .GetComponent<PlayerInput>();            } catch { gorp = null; }
        try { glob = GameObject.Find("Globbington").GetComponent<PlayerInput>(); } catch { glob = null; }

        if( gorp != null && glob != null) 
        {
            var temp = gorp.defaultActionMap;
            glob.defaultActionMap = gorp.defaultActionMap;
            gorp.defaultActionMap = temp;
            
        }
    }

}
