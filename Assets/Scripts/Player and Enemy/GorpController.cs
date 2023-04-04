/*******************************************************************************
// File Name :         GorpController.cs
// Author(s) :         Toby Schamberger
// Creation Date :     4/3/2023
//
// Brief Description : this code was written to optimize like 2 lines of code 
// and let me tell you it was worth it.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GorpController : PlayerController
{
    [Header("Input")]
    public InputAction ToggleLightAction;
    public InputAction IncreaseLight;
    public InputAction DecreaseLight;

    [Header("Settings")]
    public float FlashChargeTime = 1.0f;

    public float LightIncrement = 0.1f;
    public float LightIncrementDelay = 0.1f;
    public float ToggleLightTime = 0.5f;

    public float MinLight = 1f;
    public float MaxLight = 10f;

    // Min/Max Light are the radius of Gorp's silly light
    [Header("Debug")]
    private bool togglingLight;

    //A mysetrious, much more sinister, second thing
    private bool currentlyIncrementing;
    private float secretIncrement;
    private Coroutine incrementCoroutine;
    private Coroutine decrementCoroutine; // i added a lot of unneccessary variables for a simple problem that was already working okay. but i want this game to be good man -Toby
    private LightController lightController;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //didnt know where else to put this line:
        this.gameObject.name = "Gorp";

        lightController = GetComponent<LightController>();

        if (lightController.LightEnabled)
            LayersOfLight = 1;

        //fishLight = this.gameObject.transform.GetChild(0).GetComponent<Light2D>(); //Weird syntax but I think its more legible?
        lightController.LightRadius = lightController.LightSource.pointLightOuterRadius;

        MyPlayerInput.actions.Enable();
        ToggleLightAction = MyPlayerInput.actions.FindAction("Toggle Light");
        IncreaseLight = MyPlayerInput.actions.FindAction("Increase Light");
        DecreaseLight = MyPlayerInput.actions.FindAction("Decrease Light");

        //All the input functions!!!
        ToggleLightAction.started += ToggleLight;
        ToggleLightAction.canceled += ReleaseToggle;

        IncreaseLight.started += IncreaseLight_started;
        IncreaseLight.canceled += IncreaseLight_canceled;

        DecreaseLight.started += DecreaseLight_started;
        DecreaseLight.canceled += DecreaseLight_canceled;
    }

    private void ToggleLight(InputAction.CallbackContext obj)
    {
        lightController.LightEnabled = !lightController.LightEnabled;

        //Because this wont work the way it's supposed to for some FUCKING reason
        if (lightController.LightEnabled)
            LayersOfLight++;

        else
            LayersOfLight--;

        Invoke("AttemptFlash", FlashChargeTime);
        lightController.UpdateLightRadius(ToggleLightTime);

        if(Rumble)
            MyGamepad.SetMotorSpeeds(0.20f, 0.25f);
    }

    private void ReleaseToggle(InputAction.CallbackContext obj)
    {
        if (Rumble)
            MyGamepad.SetMotorSpeeds(0f,0f);
    }

    private void AttemptFlash() //theres an easy joke here
    {

    }

    /// <summary>
    /// Adds/subtracts LightIncrement to LightRadius, depending on 
    /// Applies changes to Gorp's light
    /// </summary>
    public IEnumerator AdjustLight()
    {
        Debug.Log("adjusting...");

        while (currentlyIncrementing)
        {
            if (lightController.LightEnabled) //not part of while loop so player can turn on light while holding button and it will work
            {
                lightController.LightRadius = Mathf.Clamp(lightController.LightRadius + secretIncrement, MinLight, MaxLight);
                lightController.UpdateLightRadius(LightIncrementDelay);
            }

            yield return new WaitForSeconds(LightIncrementDelay);
        }
    }

    private void IncreaseLight_started(InputAction.CallbackContext obj)
    {
        currentlyIncrementing = true;
        secretIncrement = LightIncrement;

        if (incrementCoroutine != null)
            StopCoroutine(incrementCoroutine);
        incrementCoroutine = StartCoroutine(AdjustLight());

        if (Rumble)
            MyGamepad.SetMotorSpeeds(0.1f, 0.15f);
    }

    private void DecreaseLight_started(InputAction.CallbackContext obj)
    {
        currentlyIncrementing = true;
        secretIncrement = -LightIncrement;

        if (incrementCoroutine != null)
            StopCoroutine(incrementCoroutine);

        decrementCoroutine = StartCoroutine(AdjustLight());

        if (Rumble)
            MyGamepad.SetMotorSpeeds(0.1f, 0.15f);
    }

    private void IncreaseLight_canceled(InputAction.CallbackContext obj)
    {
        if (incrementCoroutine != null)
            StopCoroutine(incrementCoroutine);

        currentlyIncrementing = false;

        if (Rumble)
            MyGamepad.SetMotorSpeeds(0f, 0f);
    }

    private void DecreaseLight_canceled(InputAction.CallbackContext obj)
    {
        if (decrementCoroutine != null)
            StopCoroutine(decrementCoroutine);

        currentlyIncrementing = false;

        if (Rumble)
            MyGamepad.SetMotorSpeeds(0f, 0f);
    }
}
