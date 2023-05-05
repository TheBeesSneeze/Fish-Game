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

    [Header("Player Defined Settings")]
    public bool Rumble = true;
    public bool Music = true;
    public bool SFX = true;

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
        StartCoroutine(SlideCamera(false));
        yield return null; //fufilling my coroutine requirements or whatever
    }

    /// <summary>
    /// Overload for SlideCamera, includes whether or not the camera will zoom in/out
    /// </summary>
    /// <param name="zoom">If the camera will worry about the z axis</param>
    public IEnumerator SlideCamera(bool zoom)
    {
        Vector3 oldCameraPosition = _camera.transform.position;
        Vector3 newCameraPosition = CurrentRoom.CameraPosition.position;

        if (!zoom)
            newCameraPosition = new Vector3(newCameraPosition.x, newCameraPosition.y, oldCameraPosition.z);

        float i = 0;
            
        while (i < CameraLerpSamples)
        {
            _camera.transform.position = Vector3.Lerp(oldCameraPosition, newCameraPosition , i / CameraLerpSamples);
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
        try { gorp = GameObject.Find("Gorp")       .GetComponent<PlayerInput>(); } catch { gorp = null; }
        try { glob = GameObject.Find("Globbington").GetComponent<PlayerInput>(); } catch { glob = null; }

        if( gorp != null && glob != null) 
        {
            //PlayerInput.
            
            //gorp.user;
        }
    }

}
