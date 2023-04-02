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
using Unity.Mathematics;
using Unity.VisualScripting;
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

    [Header("Settings")]
    public float TransitionFrames = 20;

    // Start is called before the first frame update
    void Start()
    {
        //just ironing things out...
        LightRadius = LightSource.pointLightOuterRadius;
        LightTrigger.radius = LightRadius;
        LightEnabled = (LightRadius >= 0);
        UpdateLightRadius();
    }

    /// <summary>
    /// Updates the Light2D radius over seconds
    /// </summary>
    public void UpdateLightRadius(float seconds, bool startfromZero)
    {
        float oldRadius = LightSource.pointLightOuterRadius;

        LightTrigger.enabled = LightEnabled;

        //Clap on!
        if (LightEnabled)
        {
            if(startfromZero)
                StartCoroutine(LightLerp(0, LightRadius, seconds));
            //regular mode
            else
                StartCoroutine(LightLerp(oldRadius, LightRadius, seconds));

            LightTrigger.radius = LightRadius;
        }
        //Clap off!
        else
        {
            StartCoroutine(LightLerp(oldRadius, 0, seconds));

            LightTrigger.radius = 0;
        }
        //The clapper!

        
    }

    /// <summary>
    /// Just snaps the light in place! wow!
    /// </summary>
    public void UpdateLightRadius()
    {
        this.UpdateLightRadius(0,false);
    }

    /// <summary>
    /// Just snaps the light in place! wow!
    /// </summary>
    public void UpdateLightRadius(float seconds)
    {
        this.UpdateLightRadius(seconds, false);
    }

    /// <summary>
    /// smooth transitions the light!
    /// </summary>
    /// <param name="startRadius"></param>
    /// <param name="endRadius"></param>
    /// <param name="seconds"></param>
    /// <returns></returns>
    private IEnumerator LightLerp(float startRadius, float endRadius, float seconds)
    {
        if (seconds > 0)
        {
            for (int i = 0; i <= TransitionFrames; i++)
            {
                float t = i / TransitionFrames; //evil voice: the t stands for toby
                float lerp = Mathf.Lerp(startRadius, endRadius, t);

                LightSource.pointLightInnerRadius = lerp / 2;
                LightSource.pointLightOuterRadius = lerp;

                yield return new WaitForSeconds(seconds / TransitionFrames);
            }
        }
        //because sometimes its instant!
        else
        {
            LightSource.pointLightInnerRadius = endRadius/2;
            LightSource.pointLightOuterRadius = endRadius;
        }
    }
}
