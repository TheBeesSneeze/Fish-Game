/*******************************************************************************
// File Name :         LeverActivator.cs
// Author(s) :         Toby Schamberger, Jay Embry
// Creation Date :     4/20/2023
//
// Brief Description : Extends ActivatorType. Activates objects when globbington
// hits it.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverActivator : ActivatorType
{
    /// <summary>
    /// toggle lever
    /// </summary>
    /// <param name="collider"></param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Attack") )
        {
            ToggleInput();
        }
    }
}
