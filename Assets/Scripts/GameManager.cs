/*******************************************************************************
// File Name :         GameManager.cs
// Author(s) :         Jay Embry, Toby Schamberger
// Creation Date :     3/30/2023
//
// Brief Description : Code that keeps track of game state. Slides camera when
// enterring new rooms.
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

    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
    }
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

}
