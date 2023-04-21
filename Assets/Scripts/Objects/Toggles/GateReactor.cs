/*******************************************************************************
// File Name :         GateReactor.cs
// Author(s) :         Toby Schamberger
// Creation Date :     4/20/2023
//
// Brief Description : Extends ReactiveType. Gate can be activated and deactivated.
// Activated:   Gate up, collider activated.
// Deactivated: Gate down, players can go through.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateReactor : ReactiveType
{
    /// <summary>
    /// Gate goes up.
    /// </summary>
    public override void OnActivate()
    {
        //literally just put code that activates some collider here
        //maybe just change the color bc we dont have a sprite yet
    }

    /// <summary>
    /// Gate go down
    /// </summary>
    public override void OnDeactivate()
    {
        //literally just put code that deactivates some collider here
    }
}
