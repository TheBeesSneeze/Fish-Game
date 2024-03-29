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
using UnityEngine.Rendering.Universal;

public class GateReactor : ColliderReactor //Exact same behavior, just changes the layer
{
    ShadowCaster2D shadowCaster;

    /// <summary>
    /// shadow caster heheh
    /// </summary>
    public override void Start()
    {
        base.Start();
        shadowCaster = GetComponent<ShadowCaster2D>();
    }
    /// <summary>
    /// Gate goes up.
    /// </summary>
    public override void OnActivate()
    {
        MySpriteRenderer.sortingOrder = 5;
        if(shadowCaster != null)
            shadowCaster.enabled =true;
        base.OnActivate();
       
    }

    /// <summary>
    /// Gate go down
    /// </summary>
    public override void OnDeactivate()
    {
        MySpriteRenderer.sortingOrder = -1;
        if (shadowCaster != null)
            shadowCaster.enabled = false;
        base.OnDeactivate();
    }
}
