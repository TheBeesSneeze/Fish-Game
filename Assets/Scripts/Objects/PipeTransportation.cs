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
    public float TransportationSpeed;
    private GameObject globbington;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name=="Globbington")
        {
            globbington = GameObject.Find("Globbington");
            globbington.SetActive(false);
            Invoke("Transportation", TransportationSpeed);

        }
    }

    public void Transportation()
    {
        globbington.SetActive(true);
        globbington.transform.position = OutputPosition;
    }
}
