/*******************************************************************************
// File Name :         CameraController.cs
// Author(s) :         Toby Schamberger
// Creation Date :     4/12/2023
//
// Brief Description : Moves the camera so that it is in the middle (or average)
//of gorp and globbington.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    [Header("Dont touch any of these variables")]

    [Tooltip("If the camera will even move")]
    public bool MoveCamera;

    public Vector2 CameraCenterPos;

    [Tooltip("Units up the camera can travel")]
    public float CamUp;
    [Tooltip("Units down the camera can travel")]
    public float CamDown;
    [Tooltip("Units left the camera can travel")]
    public float CamLeft;
    [Tooltip("Units right the camera can travel")]
    public float CamRight;

    /// <summary>
    /// Sets all of the cameras perameters. percameters.
    /// If all percameters are 0, the camera wont even bother moving
    /// </summary>
    public void UpdateCamera(float Up, float Down, float Left, float Right)
    {
        CamUp = Up;
        CamDown = Down;
        CamLeft = Left;
        CamRight = Right;

        if (CamUp == 0 && CamDown == 0 && CamLeft == 0 && CamRight == 0)
            MoveCamera = false;


    }

    /// <summary>
    /// Updates camera so that is in the middle of both players.
    /// Prioritizes sticking within bounds however.
    /// </summary>
    /// <returns></returns>
    private IEnumerator AdjustCameraPosition()
    {
        while(MoveCamera) 
        {
            yield return null;
        }
        
    }
}
