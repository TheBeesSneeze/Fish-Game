/*******************************************************************************
// File Name :         ButtonActivator.cs
// Author(s) :         Toby Schamberger, Jay Embry
// Creation Date :     4/20/2023
//
// Brief Description : Extends ActivatorType. Activates objects when globbington
// steps on it
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivator : ActivatorType
{
    /// <summary>
    /// button go down
    /// </summary>
    /// <param name="collider"></param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Globbington")
        {
            DeactivationInput();
        }
    }

    /// <summary>
    /// button go up
    /// </summary>
    /// <param name="collider"></param>
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Globbington")
        {
            ActivationInput();
        }
    }
}
