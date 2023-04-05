/*******************************************************************************
// File Name :         GameManager.cs
// Author(s) :         Jay Embry
// Creation Date :     3/30/2023
//
// Brief Description : Code that keeps track of game state
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    public float CameraLerpSamples;
    public float CameraLerpSeconds;

    [Header("Unity Stuff")]
    public RoomBehavior CurrentRoom;

    private Camera camera;
    
    public IEnumerator SlideCamera()
    {

        yield return new WaitForSeconds(CameraLerpSeconds);
    }

}
