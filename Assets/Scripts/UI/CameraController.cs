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
    [Header("Settings")]

    //[Tooltip("If the camera will zoom out when players get far away enough")]
    //public bool ZoomOut;
    //private float MinZoom;

    [Header("Don't touch any of these variables")]

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

    private Transform gorp;
    private Transform glob;

    /// <summary>
    /// Sets all of the cameras perameters. percameters.
    /// If all percameters are 0, the camera wont even bother moving
    /// </summary>
    public void UpdateCamera(Vector2 CameraCenter, float Up, float Down, float Left, float Right)
    {
        MoveCamera = true;

        gorp = GameObject.Find("Gorp").transform;
        try { glob = GameObject.Find("Globbington").transform; } catch { glob = null; }

        CamUp    = CameraCenter.y + Up      ;
        CamDown  = CameraCenter.y - Down    ;
        CamRight = CameraCenter.x + Right   ;
        CamLeft  = CameraCenter.x - Left    ;
        
        if (Up == 0 && Down == 0 && Left == 0 && Right == 0)
            MoveCamera = false;

        StartCoroutine(AdjustCameraPosition());
    }

    /// <summary>
    /// updates the camera position
    /// </summary>
    public void UpdateCamera()
    {
        StartCoroutine(AdjustCameraPosition());
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
            Vector2 newPos = AveragePlayerPostion();
            transform.position = CameraClamp(newPos);

            yield return null;
        }
        
    }

    /// <summary>
    /// Returns average position of both players.
    /// Returns gorps position if globbington is null.
    /// </summary>
    /// <returns></returns>
    public Vector2 AveragePlayerPostion()
    {
        //no checks for gorp null bc this function shouldnt run w/o gorp in theory?
        if (gorp != null && glob == null)
            return gorp.position;

        return (gorp.position + glob.position) / 2;
    }

    /// <summary>
    /// bounds CameraPos to Up,Down,Left,Right variables 
    /// </summary>
    /// <returns>Adjusted postion</returns>
    private Vector3 CameraClamp(Vector2 CameraPos)
    {
        float x = Mathf.Clamp(CameraPos.x, CamLeft, CamRight);
        float y = Mathf.Clamp(CameraPos.y, CamDown, CamUp);

        return new Vector3 (x, y, -15);
    }
}
