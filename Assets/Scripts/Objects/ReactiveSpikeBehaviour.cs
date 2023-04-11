using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*******************************************************************************
// File Name :         ReactiveSpikeBehavior.cs
// Author(s) :         Sky Beal
// Creation Date :     4/11/2023
//
// Brief Description : Code for spike that turns on after players step on them.
*****************************************************************************/
public class ReactiveSpikeBehaviour : MonoBehaviour
{
    [Header ("Settings")]
    public GameObject Spike;
    public float TimeUntilSpikeOn;
    public float TimeUntilSpikeOff;
    
    private bool spikeActive;
    private Coroutine toTurnSpikeOn;

    /// <summary>
    /// when step on spike, start spike coroutine
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("Player") || tag.Equals("Enemy"))
        {
            SpikeSwitch();
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
        yield return new WaitForSeconds(TimeUntilSpikeOn);
        Spike.SetActive(true);
        Debug.Log("Spike On");
        spikeActive = true;
        yield return new WaitForSeconds(TimeUntilSpikeOff);
        spikeActive = false;
        Spike.SetActive(false);
        toTurnSpikeOn = null;
        Debug.Log("Spike Off");
    }
}
