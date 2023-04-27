/*******************************************************************************
// File Name :         PipeTransportation.cs
// Author(s) :         Jay Embry, Toby Schamberger
// Creation Date :     3/28/2023
//
// Brief Description : Code that detects when Globbington interacts with a pipe
// and teleports him accordingly. Pipe teleports globbington to brazil, waits
// and outputs him
//
// GOALS: Add velocity?
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeBehaviour : MonoBehaviour
{
    /// <summary>
    /// Public variables allow us to edit coordinates for each pipe
    /// </summary>
    public Transform OutputPosition;
    [Tooltip("Seconds until globby is spat back out")]
    public float TransportationSpeed;

    GameObject globbington;

    /// <summary>
    /// Detects when Globbington interacts with pipe
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name=="Globbington")
        {
            globbington = collision.gameObject;

            globbington.transform.position = new Vector2(9999, 9999);
            Invoke("Transportation", TransportationSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnter2D(collision.collider);
    }

    /// <summary>
    /// Drops Globbington off at proper location
    /// </summary>
    public void Transportation()
    {
        globbington.transform.position = OutputPosition.position;
    }
}
