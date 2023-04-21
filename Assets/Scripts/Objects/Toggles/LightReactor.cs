/*******************************************************************************
// File Name :         LightReactor.cs
// Author(s) :         Toby Schamberger
// Creation Date :     4/20/2023
//
// Brief Description : Can be activated or deactivated.
// Activated:   Light Activates
// Deactivated: Light Deactivates
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightReactor : ReactiveType
{
    public Collider2D LightTrigger;
    public Light2D Light;

    /// <summary>
    /// Light Turns on.
    /// </summary>
    public override void OnActivate()
    {
        Debug.Log("Clap on!");
        LightTrigger.enabled = true;
        Light.enabled = true;
    }

    /// <summary>
    /// Light turns off
    /// </summary>
    public override void OnDeactivate()
    {
        Debug.Log("Clap off!");
        LightTrigger.enabled = false;
        Light.enabled = false;
    }
}
