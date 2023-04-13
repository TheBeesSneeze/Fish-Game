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

    //TEMPORARY
    public GameObject instructions;

    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");

        //TEMPORARY
        instructions = GameObject.Find("Instructions");
        instructions.SetActive(false);

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
    }

}
