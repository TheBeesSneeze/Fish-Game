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
    public float ElectrifyingDelay;
    public float ElectrifyingTime;
    public float WeakenedTime;

    [Header("Unity")]
    public GameObject PassiveElectricityTrigger;
    public GameObject ElectrifyingTrigger;
    private LightController lightController;
    private Animator animator;
    private ActivatorType activator;

    [Header("Audio")]
    public AudioClip PassiveElectricitySound;
    public AudioClip ElectrifyingSound;

    [Header("Debug")]
    public JellyfishState JellyState = JellyfishState.Passive;
    public enum JellyfishState
    {
        Passive,      // *casually electrucutes you*            0
        Electrifying, // zip zap                                1
        Weakened,     // After electrified (open for attack)    2
    }

    /// <summary>
    /// get variables at start
    /// </summary>
    public override void Start()
    {
        base.Start();

        lightController = GetComponent<LightController>();
        animator = GetComponent<Animator>();

        if(gameObject.activeSelf)
            StartCoroutine(AdjustLight());

        activator = GetComponent<ActivatorType>();

        StartCoroutine(SetState(JellyState,0));
    }

    /// <summary>
    /// sets jellyfish to passive
    /// </summary>
    private void OnEnable()
    {
        SetState(JellyfishState.Passive);
    }

    /// <summary>
    /// Sets JellyState = state and enables/disables appropriate triggers.
    /// Also calls/invokes some functions relating to current state.
    /// </summary>
    public IEnumerator SetState(JellyfishState state, float Delay)
    {
        yield return new WaitForSeconds(Delay);

        // this would be redundant!
        if (JellyState == state)
            yield return null;

        JellyState = state;
        animator.SetInteger("JellyEnum", (int)state);

        switch(JellyState) 
        {
            case JellyfishState.Passive:
                PassiveElectricityTrigger.SetActive(true);
                ElectrifyingTrigger.SetActive(false);
                activator.DeactivationInput();

                if(GameManagerInstance.SFX)
                {
                    MyAudioSource.Stop();
                    MyAudioSource.loop = true;
                    MyAudioSource.clip = PassiveElectricitySound;
                    MyAudioSource.Play();
                }
                break;

            case JellyfishState.Electrifying:
                PassiveElectricityTrigger.SetActive(false);
                ElectrifyingTrigger.SetActive(true);
                activator.ActivationInput();

                if (GameManagerInstance.SFX)
                {
                    MyAudioSource.Stop();
                    MyAudioSource.loop = false;
                    MyAudioSource.clip = ElectrifyingSound;
                    MyAudioSource.Play();
                }

                StartCoroutine( StopElectrifying() );
                break;
                
            case JellyfishState.Weakened:
                PassiveElectricityTrigger.SetActive(false);
                ElectrifyingTrigger.SetActive(false);
                activator.ActivationInput();

                if (GameManagerInstance.SFX)
                {
                    MyAudioSource.Stop();
                    MyAudioSource.loop = false;
                }

                StartCoroutine( StopBeingWeak() );
                break;
        }
    }

    /// <summary>
    /// sets jellyfish state
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public IEnumerator SetState(JellyfishState state)
    {
        StartCoroutine(SetState(state, 0));
        yield return null;
    }

    /// <summary>
    /// adjusts light
    /// </summary>
    /// <returns></returns>
    public IEnumerator AdjustLight()
    {
        while(true)
        {
            if (JellyState.Equals(JellyfishState.Passive) && this.gameObject.activeSelf)
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
        if (JellyState.Equals(JellyfishState.Passive))
        {
            if (tag.Equals("Flash") || tag.Equals("Electricity")) //2nd part for debug
            {
                StartCoroutine(SetState(JellyfishState.Electrifying, ElectrifyingDelay));
                //KnockBack(collider.GetComponentInParent<GameObject>(), transform.position); // knocks the player back
            }
        }
        else if (JellyState.Equals(JellyfishState.Weakened))
        {
            if (tag.Equals("Attack")) //2nd part for debug
            {
                TakeDamage(1);
            }
        }
    }

    /// <summary>
    /// Function to run some time after electrifiny
    /// </summary>
    public IEnumerator StopElectrifying()
    {
        yield return new WaitForSeconds(ElectrifyingTime);
        StartCoroutine(SetState(JellyfishState.Weakened));
    }

    /// <summary>
    /// Function to run some time after being weakened
    /// </summary>
    public IEnumerator StopBeingWeak()
    {
        yield return new WaitForSeconds(WeakenedTime);
        StartCoroutine(SetState(JellyfishState.Passive));
    }

    /// <summary>
    /// sets jellyfish as passive on respawn
    /// </summary>
    public  override void Respawn()
    {
        base.Respawn();
        SetState(JellyfishState.Passive);
    }
}
