/*******************************************************************************
// File Name :         PipeTransportation.cs
// Author(s) :         Jay Embry
// Creation Date :     3/28/2023
//
// Brief Description : Code that detects when Globbington interacts with a pipe
and teleports him accordingly.
//
// GOALS: Add velocity?
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTransportation : MonoBehaviour
{
    /// <summary>
    /// Public variables allow us to edit coordinates for each pipe
    /// </summary>
    public Vector2 OutputPosition;
    public Vector2 Brazil;
    public float TransportationSpeed;

    GameObject globbington;
    /// <summary>
    /// Detects when Globbington interacts with pipe
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.name=="Globbington")
        {

            globbington = collision.gameObject;
            Invoke("Wait", TransportationSpeed);

        }
    }
    /// <summary>
    /// Gives the illusion of Globbington going offscreen
    /// </summary>
    public void Wait()
    {

        globbington.transform.position = Brazil;
        Invoke("Transportation", TransportationSpeed);

    }
    /// <summary>
    /// Drops Globbington off at proper location
    /// </summary>
    public void Transportation()
    {

        globbington.transform.position = OutputPosition;

    }
}
