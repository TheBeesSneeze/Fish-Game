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
    public GameObject FlashTrigger;

    [Header("Settings")]
    public float FlashChargeTime;
    public float FlashLength;

    public float LightIncrement;
    public float LightIncrementDelay;
    public float ToggleLightTime;

    public float MinLight;
    public float MaxLight;

    [Header("Input")]
    public InputAction ToggleLightAction;
    public InputAction FlashAction;
    public InputAction IncreaseLight;
    public InputAction DecreaseLight;

    [Header("Debug")]
    private bool currentlyIncrementing;
    private float secretIncrement;
    private Coroutine incrementCoroutine;
    private Coroutine decrementCoroutine; // i added a lot of unneccessary variables for a simple problem that was already working okay. but i want this game to be good man -Toby
    private LightController lightController;
    public LightController FishChargeLight;

    private bool HoldingFlash;
    private Coroutine FlashCoroutine;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        LayersOfLight = 1;

        //didnt know where else to put this line:
        this.gameObject.name = "Gorp";

        FishChargeLight.LightRadius = 2.5f;
        UpdateLightEnabled(FishChargeLight, false);
        FishChargeLight.UpdateLightRadius(0, true);

        lightController = GetComponent<LightController>();

        //if (lightController.LightEnabled)
        //    LayersOfLight = 1;

        //fishLight = this.gameObject.transform.GetChild(0).GetComponent<Light2D>(); //Weird syntax but I think its more legible?
        lightController.LightRadius = lightController.LightSource.pointLightOuterRadius;

        MyPlayerInput.actions.Enable();
        ToggleLightAction = MyPlayerInput.actions.FindAction("Toggle Light");
        IncreaseLight     = MyPlayerInput.actions.FindAction("Increase Light");
        DecreaseLight     = MyPlayerInput.actions.FindAction("Decrease Light");
        FlashAction       = MyPlayerInput.actions.FindAction("Flash");

        //All the input functions!!!
        ToggleLightAction.started += Toggle_started;
        ToggleLightAction.canceled += Toggle_canceled;

        FlashAction.started += Flash_started;
        FlashAction.canceled += Flash_canceled;

        IncreaseLight.started += IncreaseLight_started;
        IncreaseLight.canceled += IncreaseLight_canceled;

        DecreaseLight.started += DecreaseLight_started;
        DecreaseLight.canceled += DecreaseLight_canceled;
    }

    public override void Respawn()
    {
        base.Respawn();
        GameMasterInstance.CurrentRoom.RespawnAllObjects();
    }

    /// <summary>
    /// Sets LightEnabled on LightCtrl = Enabled.
    /// Makes checks that account for LayersOfLight and increments as necessary.
    /// </summary>
    /// <param name="Light"></param>
    /// <param name="Enabled"></param>
    private void UpdateLightEnabled(LightController LightCtrl, bool Enabled)
    {
        if (LightCtrl.LightEnabled && !Enabled)
            LayersOfLight--;
        else if (!LightCtrl.LightEnabled && Enabled)
            LayersOfLight++;

        LightCtrl.LightEnabled = Enabled;
    }

    /// <summary>
    /// mostly just for when you need to Invoke a rumble after seconds
    /// </summary>
    private IEnumerator SetRumble(float min, float max, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (GameMasterInstance.Rumble && MyGamepad != null)
            MyGamepad.SetMotorSpeeds(min, max);
    }


    /// <summary>
    /// Begins the players light switch journey!
    /// </summary>
    private void Toggle_started(InputAction.CallbackContext obj)
    {
        UpdateLightEnabled(lightController, !lightController.LightEnabled);

        lightController.UpdateLightRadius(ToggleLightTime, false);

        if (GameMasterInstance.Rumble && MyGamepad!=null)
            MyGamepad.SetMotorSpeeds(0.20f, 0.25f);
    }

    /// <summary>
    /// Toggles light if no flash.
    /// </summary>
    private void Toggle_canceled(InputAction.CallbackContext obj)
    {
        if (GameMasterInstance.Rumble && MyGamepad!= null)
            MyGamepad.SetMotorSpeeds(0f, 0f);
    }

    /// <summary>
    /// fish flashes if the player is still holding the 
    /// toggle light button.
    /// </summary>
    private void Flash_started(InputAction.CallbackContext obj) 
    {
        HoldingFlash = true;

        //turn on yellow light
        FishChargeLight.gameObject.SetActive(true);
        FishChargeLight.LightRadius = 2.5f;
        UpdateLightEnabled(FishChargeLight, true);
        FishChargeLight.UpdateLightRadius(FlashChargeTime, true);

        FlashCoroutine = StartCoroutine(AttemptFlash());    
    }

    private void Flash_canceled(InputAction.CallbackContext obj)
    {
        if(FlashCoroutine != null)
        {
            //oh? youre not null? guess what champ
            StopCoroutine(FlashCoroutine);
            FlashCoroutine = null;
        }

        StartCoroutine(StopFlash(0));

        HoldingFlash = false;
    }

    /// <summary>
    /// Fish flashes if player is still holding button down
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttemptFlash() //theres an easy joke here
    {
        
        yield return new WaitForSeconds(FlashChargeTime);

        //Successful flash:
        if (HoldingFlash)
        {
            FishFlash(); // <= its right there

            StartCoroutine(SetRumble(0, 0, 0.4f));

            //code that turns off the light:
            //UpdateLightEnabled(lightController, false);
            //lightController.UpdateLightRadius(0.1f);

            FishChargeLight.LightRadius = 7.5f;
            UpdateLightEnabled(FishChargeLight, true);
            FishChargeLight.UpdateLightRadius(0.1f, true);
        }

        FlashCoroutine = null;
    }

    
    /// <summary>
    /// Fishflash without inputaction context
    /// </summary>
    private void FishFlash()
    {
        FlashTrigger.SetActive(true);
        FlashTrigger.GetComponent<Collider2D>().enabled = true;

        if (GameMasterInstance.Rumble && MyGamepad != null)
        {
            //InputDevice a = MyPlayerInput.devices[0];
            MyGamepad.SetMotorSpeeds(0.20f, 0.30f);
        }

        StartCoroutine(StopFlash(FlashLength));
    }

    /// <summary>
    /// Does not require flash to be successful.
    /// Code that disables the flash trigger and light.
    /// Runs after FlashLength seconds have elapsed.
    /// Code runs after Delay
    /// </summary>
    private IEnumerator StopFlash(float Delay)
    {
        yield return new WaitForSeconds(Delay);

        FlashTrigger.SetActive(false);

        //turn off yellow light
        UpdateLightEnabled(FishChargeLight, false);
        FishChargeLight.UpdateLightRadius(0, true);
    }

    public IEnumerator AdjustLight()
    {
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

        if (GameMasterInstance.Rumble)
            MyGamepad.SetMotorSpeeds(0.1f, 0.15f);
    }

    private void DecreaseLight_started(InputAction.CallbackContext obj)
    {
        currentlyIncrementing = true;
        secretIncrement = -LightIncrement;

        if (incrementCoroutine != null)
            StopCoroutine(incrementCoroutine);

        decrementCoroutine = StartCoroutine(AdjustLight());

        if (GameMasterInstance.Rumble)
            MyGamepad.SetMotorSpeeds(0.1f, 0.15f);
    }

    private void IncreaseLight_canceled(InputAction.CallbackContext obj)
    {
        if (incrementCoroutine != null)
            StopCoroutine(incrementCoroutine);

        currentlyIncrementing = false;

        if (GameMasterInstance.Rumble)
            MyGamepad.SetMotorSpeeds(0f, 0f);
    }

    private void DecreaseLight_canceled(InputAction.CallbackContext obj)
    {
        if (decrementCoroutine != null)
            StopCoroutine(decrementCoroutine);

        currentlyIncrementing = false;

        if (GameMasterInstance.Rumble)
            MyGamepad.SetMotorSpeeds(0f, 0f);
    }

    public override void OnDestroy()
    {
        

        ToggleLightAction.started -= Toggle_started;
        ToggleLightAction.canceled -= Toggle_canceled;

        IncreaseLight.started -= IncreaseLight_started;
        IncreaseLight.canceled -= IncreaseLight_canceled;

        DecreaseLight.started -= DecreaseLight_started;
        DecreaseLight.canceled -= DecreaseLight_canceled;

        FlashAction.started -= Flash_started;
        FlashAction.canceled -= Flash_canceled;

        base.OnDestroy();

    }

    
}
