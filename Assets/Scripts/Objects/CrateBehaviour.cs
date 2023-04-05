using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*******************************************************************************
// File Name :         CrateBehaviour.cs
// Author(s) :         Sky Beal
// Creation Date :     3/28/2023
//
// Brief Description : Code for crate break !!
// PUSH GOAL: key in crates ??? (instantiate ??)
*****************************************************************************/

public class CrateBehaviour : MonoBehaviour
{
    [Header("Settings")]

    public Vector3 CrateStartingPosition;

    /// <summary>
    /// starting position oooooo
    /// </summary>
    private void Start()
    {
        CrateStartingPosition = this.transform.position;
    }

    /// <summary>
    /// if sword swing hits it BOOM dead
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("Attack"))
        {
            Destroy(gameObject);
        }
    }

}
