using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*******************************************************************************
// File Name :         UpDownSpikeBehavior.cs
// Author(s) :         Sky Beal
// Creation Date :     4/11/2023
//
// Brief Description : Code for spikes that constantly turn on and off.
*****************************************************************************/

public class UpDownSpikeBehaviour : MonoBehaviour
{
    [Header ("Settings")]
    public GameObject Spike;
    public float SpikeOn;
    public float SpikeOff;

    /// <summary>
    /// calls the coroutine
    /// </summary>
    void Start()
    {
        StartCoroutine(SpikeGoUpDown());
    }

    /// <summary>
    /// coroutine that constantly turns spike on and off
    /// </summary>
    /// <returns></returns>
    public IEnumerator SpikeGoUpDown()
    {
        while (true)
        {
            Spike.SetActive(false);
            yield return new WaitForSeconds(SpikeOff);
            Spike.SetActive(true);
            yield return new WaitForSeconds(SpikeOn);
        }
    }
}
