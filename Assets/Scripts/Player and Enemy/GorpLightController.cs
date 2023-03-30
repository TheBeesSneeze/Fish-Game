/*******************************************************************************
// File Name :         GorpLightController.cs
// Author(s) :         Toby Schamberger
// Creation Date :     3/23/2023
//
// Brief Description : Manages player input that controls Gorp's light.
// Allows for tight control over light strength, as well as the ability to
// toggle the light completely. 
// Turning down the light all the way won't toggle it.
// TODO: Toggle
*****************************************************************************/

using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class GorpLightController : LightController
{
    [Header("Settings")]
    public float LightIncrement = 0.1f;
    public float LightIncrementDelay = 0.1f;

    // Min/Max Light are the radius of Gorp's silly light
    public float MinLight = 1f;
    public float MaxLight = 10f;

    [Header("Input")]
    public PlayerInput MyPlayerInput;

    public InputAction ToggleLight;
    public InputAction IncreaseLight;
    public InputAction DecreaseLight;

    //A mysetrious, much more sinister, fourth thing
    private bool currentlyIncrementing;

    // Start is called before the first frame update
    void Start()
    {
        //didnt know where else to put this line:
        this.gameObject.name = "Gorp";

        //fishLight = this.gameObject.transform.GetChild(0).GetComponent<Light2D>(); //Weird syntax but I think its more legible?
        LightRadius = LightSource.pointLightOuterRadius;

        MyPlayerInput.actions.Enable();
        ToggleLight = MyPlayerInput.actions.FindAction("Toggle Light");
        IncreaseLight = MyPlayerInput.actions.FindAction("Increase Light");
        DecreaseLight = MyPlayerInput.actions.FindAction("Decrease Light");

        //All the input functions!!!
        IncreaseLight.started += IncreaseLight_started;
        IncreaseLight.canceled += IncreaseLight_canceled;

        DecreaseLight.started += DecreaseLight_started;
        DecreaseLight.canceled += DecreaseLight_canceled;
    }

    /// <summary>
    /// Updates the Light2D's values to match the values in this script
    /// (Renders it in game)
    /// </summary>
    public override void UpdateLightRadius()
    {
        base.UpdateLightRadius();

        LightSource.pointLightOuterRadius = LightRadius;
        LightSource.pointLightInnerRadius = LightRadius/2;
    }

    /// <summary>
    /// Adds/subtracts LightIncrement to LightRadius, depending on 
    /// Applies changes to Gorp's light
    /// </summary>
    public IEnumerator AdjustLight(float increment)
    {
        Debug.Log("adjusting...");
        while (currentlyIncrementing)
        {
            LightRadius = Mathf.Clamp(LightRadius + increment, MinLight, MaxLight);
            UpdateLightRadius();

            yield return new WaitForSeconds(LightIncrementDelay);
        }
    }

    private void IncreaseLight_started(InputAction.CallbackContext obj)
    {
        currentlyIncrementing = true;
        StartCoroutine(AdjustLight(LightIncrement));
    }

    private void DecreaseLight_started(InputAction.CallbackContext obj)
    {
        currentlyIncrementing = true;
        StartCoroutine(AdjustLight(-LightIncrement));
    }

    private void IncreaseLight_canceled(InputAction.CallbackContext obj)
    {
        currentlyIncrementing = false;
    }

    private void DecreaseLight_canceled(InputAction.CallbackContext obj)
    {
        currentlyIncrementing = false;
    }
}
