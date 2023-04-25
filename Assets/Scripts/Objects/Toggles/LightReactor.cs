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
    private Animator lightAnimator;

    private void Start()
    {
        lightAnimator = GetComponent<Animator>();
    }
    /// <summary>
    /// Light Turns on.
    /// </summary>
    public override void OnActivate()
    {
        lightAnimator.SetBool("Enabled", true);
        LightTrigger.enabled = true;
        Light.gameObject.SetActive(true);
        Light.enabled = true;
    }

    /// <summary>
    /// Light turns off
    /// </summary>
    public override void OnDeactivate()
    {
        lightAnimator.SetBool("Enabled", false);
        LightTrigger.enabled = false;
        Light.gameObject.SetActive(false);
        Light.enabled = false;
    }
}
