/*******************************************************************************
// File Name :         JellyfishBehavior.cs
// Author(s) :         Toby Schamberger
// Creation Date :     4/8/2023
//
// Brief Description : Controls jellyfish enemy. Jellyfish is immune to damage until
// gorp zaps it. when gorp zaps it, it creats a big puff of electricity, damaging
// anything that isn't immune to electricity.
//
// TODO: Rumble!
*****************************************************************************/

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishBehavior : EnemyBehavior
{
    [Header("Settings")]

    [Tooltip("How far player has to be until it glows")]
    public float MinDistanceLight;
    [Tooltip("How bright light will be at its brightest")]
    public float MaxLightRadius;
    [Tooltip("How intense light will be at its intensest")]
    public float MaxIntensity;

    [Header("Unity")]
    public GameObject PassiveElectricityTrigger;
    public GameObject ElectrifyingTrigger;
    private LightController lightController;

    [Header("Debug")]
    public JellyfishState JellyState = JellyfishState.Passive;
    public enum JellyfishState
    {
        Passive,      // *casually electrucutes you*
        Electrifying, // zip zap
        Weakened,     // After electrified (open for attack)
    }

    public virtual void Start()
    {
        base.Start();

        lightController = GetComponent<LightController>();

        StartCoroutine(AdjustLight());
    }

    /// <summary>
    /// Sets JellyState = state and enables/disables appropriate triggers.
    /// Also calls/invokes some functions relating to current state.
    /// </summary>
    public void SetState(JellyfishState state)
    {
        JellyState = state;
        switch(JellyState) 
        {
            case JellyfishState.Passive:
                PassiveElectricityTrigger.SetActive(true);
                ElectrifyingTrigger.SetActive(false);
                break;

            case JellyfishState.Electrifying:
                PassiveElectricityTrigger.SetActive(false);
                ElectrifyingTrigger.SetActive(true);
                break;

            case JellyfishState.Weakened:
                PassiveElectricityTrigger.SetActive(false);
                ElectrifyingTrigger.SetActive(false);
                break;
        }
    }

    public IEnumerator AdjustLight()
    {
        while(true)
        {
            if (JellyState.Equals(JellyfishState.Passive))
            {
                float closestPlayer = GetDistanceOfClosestTag(this.transform.position, "Player");

                if(closestPlayer < MinDistanceLight && closestPlayer!=-1)
                {
                    float p = (MinDistanceLight - closestPlayer) / MinDistanceLight;
                    float radius = p * MaxLightRadius;

                    lightController.LightEnabled = true;
                    lightController.LightRadius = radius;
                    lightController.LightSource.intensity = (p * MaxIntensity);
                    lightController.UpdateLightRadius(0.3f, false);
                    yield return new WaitForSeconds(0.35f);
                }
                else
                {
                    lightController.LightEnabled = false;
                    lightController.UpdateLightRadius(0.1f, false);
                    yield return new WaitForSeconds(1);
                }
                
            }
            else
                //i am putting the fish in effishency right now
                yield return new WaitForSeconds(0.5f);
        }
    }

    /// <summary>
    /// Jellyfish have different behaviors during different states.
    /// It can only be hit with flash during passive.
    /// It can only be damaged when weakened.
    /// Nothing happens during electrifying.
    /// </summary>
    public override void OnTriggerEnter2D(Collider2D collider) //Collider2D collision)
    {
        string tag = collider.tag;

        // Start electric blast
        if( JellyState.Equals( JellyfishState.Passive ) )
        {
            if(tag.Equals("Flash") || tag.Equals("Electricity")) //2nd part for debug
            {
                Debug.Log("Electric avenue");
                SetState(JellyfishState.Electrifying);
                KnockBack(collider.GetComponentInParent<GameObject>(), transform.position); // knocks the player back
            }
        }
    }

    
}
