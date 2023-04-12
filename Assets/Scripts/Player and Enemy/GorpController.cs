/*******************************************************************************
// File Name :         GorpController.cs
// Author(s) :         Toby Schamberger, Sky Beal, Jay Embry
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
    public float FlashChargeTime;
    public float FlashLength;

    public float LightIncrement;
    public float LightIncrementDelay;
    public float ToggleLightTime;

    public float MinLight;
    public float MaxLight;

    // Min/Max Light are the radius of Gorp's silly light
    [Header("Debug")]
    private bool togglingLight;

    //A mysterious, much more sinister, fourth thing
    private bool currentlyIncrementing;
    private float secretIncrement;
    private Coroutine incrementCoroutine;
    private Coroutine decrementCoroutine; // i added a lot of unneccessary variables for a simple problem that was already working okay. but i want this game to be good man -Toby
    private LightController lightController;
    public LightController FishChargeLight;
    
    private bool previousLightEnabled;
    public  GameObject FlashTrigger;
    private Coroutine flashCoroutine;
    private Coroutine secondFlashCoroutine; // the long awaited sequel
    private bool flashedSuccessfully;

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

        FishChargeLight.LightRadius = 2.5f;
        FishChargeLight.LightEnabled = false;
        FishChargeLight.UpdateLightRadius(0,true);

        MyPlayerInput.actions.Enable();
        ToggleLightAction = MyPlayerInput.actions.FindAction("Toggle Light");
        IncreaseLight = MyPlayerInput.actions.FindAction("Increase Light");
        DecreaseLight = MyPlayerInput.actions.FindAction("Decrease Light");

        //All the input functions!!!
        ToggleLightAction.started += ToggleLight_started;
        ToggleLightAction.canceled += Toggle_canceled;

        IncreaseLight.started += IncreaseLight_started;
        IncreaseLight.canceled += IncreaseLight_canceled;

        DecreaseLight.started += DecreaseLight_started;
        DecreaseLight.canceled += DecreaseLight_canceled;
    }

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

    /// <summary>
    /// Begins the players light switch journey!
    /// </summary>
    private void ToggleLight_started(InputAction.CallbackContext obj)
    {
        previousLightEnabled = lightController.LightEnabled;

        togglingLight = true;

        flashCoroutine = StartCoroutine( AttemptFlash() );

        if(lightController.LightEnabled)
        {
            lightController.LightEnabled = false;
            lightController.UpdateLightRadius(ToggleLightTime,false);
        }

        FishChargeLight.LightRadius = 2.5f;
        FishChargeLight.LightEnabled = true;
        FishChargeLight.UpdateLightRadius(FlashChargeTime, true);

        if (Rumble)
            MyGamepad.SetMotorSpeeds(0.20f, 0.25f);
    }

    /// <summary>
    /// Toggles light if no flash.
    /// </summary>
    private void Toggle_canceled(InputAction.CallbackContext obj)
    {
        togglingLight = false;
        StopCoroutine(flashCoroutine);

        FishChargeLight.LightEnabled = false;
        FishChargeLight.UpdateLightRadius(0, true);

        // Normal input (not held down)
        if ( ! flashedSuccessfully)
        {
            lightController.LightEnabled = !previousLightEnabled;

            //Because this wont work the way it's supposed to for some FUCKING reason
            if (lightController.LightEnabled)
                LayersOfLight++;

            else //off
                LayersOfLight--;

            Invoke("AttemptFlash", FlashChargeTime);

            if(! previousLightEnabled) //because it already happened
                lightController.UpdateLightRadius(ToggleLightTime, false);

            if (Rumble)
                MyGamepad.SetMotorSpeeds(0f, 0f);
        }
    }

    /// <summary>
    /// fish flashes if the player is still holding the 
    /// toggle light button.
    /// </summary>
    private IEnumerator AttemptFlash() //theres an easy joke here
    {
        flashedSuccessfully = false;

        yield return new WaitForSeconds(FlashChargeTime);

        //Successful flash:
        if ( togglingLight ) 
        {
            FishFlash();
            StartCoroutine( SetRumble(0, 0, 0.4f) );
            flashedSuccessfully = true;
            lightController.LightEnabled = false;
            lightController.UpdateLightRadius(0.1f);

            FishChargeLight.LightEnabled = true;
            FishChargeLight.LightRadius = 7.5f;
            FishChargeLight.UpdateLightRadius(0.1f, true);
        }
    }

    /// <summary>
    /// mostly just for when you need to Invoke a rumble after seconds
    /// </summary>
    private IEnumerator SetRumble(float min, float max, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (Rumble)
            MyGamepad.SetMotorSpeeds(min, max);
    }

    /// <summary>
    /// Fishflash without inputaction context
    /// </summary>
    private void FishFlash()
    {
        FlashTrigger.SetActive(true);
        FlashTrigger.GetComponent<Collider2D>().enabled = true;

        if (Rumble)
        {
            //InputDevice a = MyPlayerInput.devices[0];
            MyGamepad.SetMotorSpeeds(0.20f, 0.30f);
        }

        Invoke("StopFlash", FlashLength);
    }

    private void StopFlash()
    {
        FlashTrigger.SetActive(false);

        FishChargeLight.LightEnabled = false;
        FishChargeLight.UpdateLightRadius(0.1f, true);
    }

    /// <summary>
    /// Adds/subtracts LightIncrement to LightRadius, depending on 
    /// Applies changes to Gorp's light
    /// </summary>
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

    private void OnDestroy()
    {
        ToggleLightAction.started -= ToggleLight_started;
        ToggleLightAction.canceled -= Toggle_canceled;

        IncreaseLight.started -= IncreaseLight_started;
        IncreaseLight.canceled -= IncreaseLight_canceled;

        DecreaseLight.started -= DecreaseLight_started;
        DecreaseLight.canceled -= DecreaseLight_canceled;
    }
}
