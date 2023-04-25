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

public class GateReactor : ColliderReactor //Exact same behavior, just changes the layer
{

    /// <summary>
    /// Gate goes up.
    /// </summary>
    public override void OnActivate()
    {
        MySpriteRenderer.sortingOrder = 5;
        base.OnActivate();
       
    }

    /// <summary>
    /// Gate go down
    /// </summary>
    public override void OnDeactivate()
    {
        Debug.Log("he");
        MySpriteRenderer.sortingOrder = -1;
        base.OnDeactivate();
    }
}
