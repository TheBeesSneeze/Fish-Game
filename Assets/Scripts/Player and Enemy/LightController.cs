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
    public bool LightEnabled = true;
    public float LightRadius;

    [Header("Light Sources")]
    public CircleCollider2D LightTrigger;
    public Light2D LightSource;

    // Start is called before the first frame update
    void Start()
    {
        LightRadius = LightSource.pointLightOuterRadius;
        UpdateLightRadius();
    }

    public virtual void UpdateLightRadius()
    {
        LightTrigger.radius = LightRadius;
    }
}
