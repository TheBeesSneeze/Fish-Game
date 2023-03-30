/*******************************************************************************
// File Name :         LightController.cs
// Author(s) :         Toby Schamberger
// Creation Date :     3/29/2023
//
// Brief Description : Manages light trigger and all that. Basic code to
// be shared between Gorp, Anglers, and light sources(?)
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    [Header("Light settings:")]
    public bool LightEnabled;
    public float LightRadius;

    [Header("Light Sources")]
    public CircleCollider2D LightTrigger;
    public Light2D LightSource;

    // Start is called before the first frame update
    void Start()
    {
        LightRadius = LightSource.pointLightOuterRadius;
        LightEnabled = (LightRadius >= 0);
        UpdateLightRadius();
    }

    /// <summary>
    /// Updates the Light2D radius
    /// </summary>
    public virtual void UpdateLightRadius()
    {
        //Clap on!
        if (LightEnabled)
        {
            LightSource.pointLightInnerRadius = LightRadius / 2;
            LightSource.pointLightOuterRadius = LightRadius;

            LightTrigger.radius = LightRadius;
        }
        //Clap off!
        else
        {
            LightSource.pointLightInnerRadius = 0;
            LightSource.pointLightOuterRadius = 0;

            LightTrigger.radius = 0;
        }
        //The clapper!
    }
}
