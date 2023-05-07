using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*******************************************************************************
// File Name :         ReactiveSpikeBehavior.cs
// Author(s) :         Sky Beal, Toby Schamberger
// Creation Date :     4/11/2023
//
// Brief Description : Code for spike that turns on after players step on them.
// Can also be activated by light (Toby did that hes sooooo cool)
*****************************************************************************/
public class PressureSpikeBehaviour : MonoBehaviour
{
    [Header ("Settings")]
    public GameObject Spike;
    public float TimeUntilSpikeOn;
    public float TimeUntilSpikeOff;

    public ActivationType Activator;
    public enum ActivationType
    {
        Character, Light
    }
    
    private bool spikeActive;
    private Coroutine toTurnSpikeOn;

    /// <summary>
    /// when step on spike, start spike coroutine
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;

        if (Activator == ActivationType.Character)
        {
            if (tag.Equals("Player") || tag.Equals("Enemy"))
                SpikeSwitch();
        }
        else if (Activator == ActivationType.Light)
        {
            if (tag.Equals("Light"))
                Spike.SetActive(true);
        }
    }

    /// <summary>
    /// turn the light on after light
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (Activator == ActivationType.Light)
        {
            if (tag.Equals("Light"))
                StartCoroutine(TurnOnSpike());
        }
    }

    /// <summary>
    /// starts coroutine if spike isn't on and coroutine isn't running
    /// </summary>
    public void SpikeSwitch()
    {
        if (!spikeActive && toTurnSpikeOn == null)
        {
            toTurnSpikeOn = StartCoroutine(TurnOnSpike());
        }
    }

    /// <summary>
    /// coroutine to turn spike on and off
    /// </summary>
    /// <returns></returns>
    public IEnumerator TurnOnSpike()
    {
        if(Activator == ActivationType.Character)
        {
            yield return new WaitForSeconds(TimeUntilSpikeOn);
            Spike.SetActive(true);
            Debug.Log("Spike On");
            spikeActive = true;
        }
        
        yield return new WaitForSeconds(TimeUntilSpikeOff);
        spikeActive = false;
        Spike.SetActive(false);
        toTurnSpikeOn = null;
        Debug.Log("Spike Off");
    }
}
