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

using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    [Header("Light settings:")]
    public bool LightEnabled=true;
    public float LightRadius;
    private float lightRadiusDescale;
    [Tooltip("Inner = Outer / [THIS NUMBER]")]
    public float InnerRadiusDivisionFactor = 2;

    [Header("Light Sources")]
    public CircleCollider2D LightTrigger;
    public Light2D LightSource;

    [Header("Settings")]
    public float TransitionFrames = 20;
    private Coroutine lightLerpCoroutine;


    // Start is called before the first frame update
    void Start()
    {
        if (LightSource == null)
            try { LightSource = this.GetComponent<Light2D>(); } catch { }

        if (LightTrigger == null)
            try { LightTrigger = this.GetComponent<CircleCollider2D>(); } catch { }

        //just ironing things out...
        LightRadius = LightSource.pointLightOuterRadius;

        lightRadiusDescale = 1;

        if (LightTrigger != null)
        {
            lightRadiusDescale = LightTrigger.transform.parent.localScale.x;
            LightTrigger.radius = LightRadius / lightRadiusDescale;
        }
        
        //LightEnabled = (LightRadius >= 0);
        UpdateLightRadius();
    }

    /// <summary>
    /// Updates the Light2D radius over seconds
    /// </summary>
    public void UpdateLightRadius(float seconds, bool startfromZero)
    {
        if (LightSource != null)
        {
            float oldRadius = LightSource.pointLightOuterRadius;

            if (LightTrigger != null)
                LightTrigger.enabled = LightEnabled;

            if (lightLerpCoroutine != null)
                StopCoroutine(lightLerpCoroutine);

            //Clap on!
            if (LightEnabled)
            {
                if (startfromZero)
                    lightLerpCoroutine = StartCoroutine(LightLerp(0, LightRadius, seconds));
                //regular mode
                else
                    lightLerpCoroutine = StartCoroutine(LightLerp(oldRadius, LightRadius, seconds));

                if (LightTrigger != null)
                    LightTrigger.radius = LightRadius / lightRadiusDescale;

            }
            //Clap off!
            else
            {
                //gameObject.SetActive(true);

                lightLerpCoroutine = StartCoroutine(LightLerp(oldRadius, 0, seconds));

                if (LightTrigger != null)
                {
                    LightTrigger.radius = 0;
                }

            }
            //The clapper!
        }
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
    private IEnumerator LightLerp(float startRadius, float endRadius, float seconds)
    {

        if (seconds > 0)
        {
            for (int i = 0; i <= TransitionFrames; i++)
            {
                float t = i / TransitionFrames; //evil voice: the t stands for toby
                float lerp = Mathf.Lerp(startRadius, endRadius, t);

                LightSource.pointLightInnerRadius = lerp / InnerRadiusDivisionFactor;
                LightSource.pointLightOuterRadius = lerp;

                yield return new WaitForSeconds(seconds / TransitionFrames);
            }
        }
        //because sometimes its instant!
        else
        {
            LightSource.pointLightInnerRadius = endRadius/ InnerRadiusDivisionFactor;
            LightSource.pointLightOuterRadius = endRadius;
        }
        lightLerpCoroutine = null;
    }
}
