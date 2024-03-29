/*******************************************************************************
// File Name :         EnemyBehavior.cs
// Author(s) :         Toby Schamberger
// Creation Date :     3/27/2023
//
// Brief Description : Basic code for enemy management. Specific enemy scripts
// should extend this script.
// If you're not looking for a setting that isn't here. It's probably in the
// Enemy Detection script!
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : CharacterBehavior
{
    [Header("Attributes")]

    [Tooltip("If the enemy will despawn on start. Enemy should be spawning when enterring the room")]
    public EnemyType EnemyData;

    [Header("Unity Jargain")]
    public EnemyDetection enemyDetection;
    private Collider2D enemyCollider;
    public AudioClip EnemyDamage;

    [Header("You don't need to touch this:")]
    public GameObject Gorp;
    public GameObject Globbington;
    public bool Dead;
    public bool DisableMovement;
    private float lightDPS;

    /// <summary>
    /// Initializes variables for enemy respwning and finds unity components.
    /// </summary>
    public override void Start()
    {
        base.Start();
        SetAttributes();
        DefaultPosition = this.transform.position;

        //Unity moment
        enemyDetection = gameObject.GetComponent<EnemyDetection>();
        enemyCollider  = gameObject.GetComponent<Collider2D>();

        Gorp        = GameObject.Find("Gorp");
        Globbington = GameObject.Find("Globbington");

        Dead = DespawnOnStart;
        this.gameObject.SetActive(!DespawnOnStart);
    }

    /// <summary>
    /// Kills the enemy! (Actually just SetActive(false)
    /// </summary>
    public override void Despawn()
    {
        Dead = true;

        this.gameObject.SetActive(false);

        if (MyRoom != null)
            MyRoom.UpdateRoomStatus();

    }

    /// <summary>
    /// Basically resets the enemy. It's like how it was at the start of the scene!
    /// </summary>
    public override void Respawn()
    {
        this.gameObject.SetActive(true);
        base.Respawn();

        Dead = false;
        SetAttributes();

        Gorp = GameObject.Find("Gorp");
        Globbington = GameObject.Find("Globbington");

        //Enemy exclusive code:
        if (enemyDetection != null)
        {
            enemyDetection.CurrentTarget = null;
            enemyDetection.Gorp = Gorp;
            enemyDetection.Globbington = Globbington;
            StartCoroutine(enemyDetection.SearchForPlayer());
        }

        try { this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero; } catch { }
    }

    /// <summary>
    /// triggers for enemies
    /// </summary>
    /// <param name="collision"></param>
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if(tag.Equals("Attack"))
        {
            //if enemy needs to be stunned
            if( (EnemyData.ProtectedUntilFlash && Stunned) || ! EnemyData.ProtectedUntilFlash )
            {
                TakeDamage(1, collision.gameObject.transform.position);

                if(Health != 0)
                    StartCoroutine(DisableMovementCoroutine());

                if (MyAudioSource != null && GameManagerInstance.SFX)
                {
                    GameManagerInstance.AudioCEO.clip = EnemyDamage;
                    GameManagerInstance.AudioCEO.Stop();
                    GameManagerInstance.AudioCEO.Play();
                }
            }
                
        }
        else if (tag.Equals("Light"))
        {
            LayersOfLight++;
            if (LayersOfLight == 1 && lightDPS != 0) //only on 1st LOL to avoid too many coroutines at once
                StartCoroutine(TakeLightDamage());

        }
        if (tag.Equals("Flash"))
        {
            if(EnemyData.HurtByFlash)
                TakeDamage(1,collision.transform.position);

            if(EnemyData.StunnedByFlash)
                BeStunned();
        }
    }

    /// <summary>
    /// exiting light
    /// </summary>
    /// <param name="collision"></param>
    public override void OnTriggerExit2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("Light"))
        {
            LayersOfLight--;

            if (enemyDetection != null)
            {
                if (enemyDetection.CurrentTarget != null) //so many if statements that have to be separate
                {
                    if (enemyDetection.CurrentTarget.GetComponent<CharacterBehavior>().LayersOfLight <= 0)
                        enemyDetection.CurrentTarget = null;
                }  
            }
                
        }
    }


    /// <summary>
    /// become stunned
    /// </summary>
    public override void BeStunned()
    {
        base.BeStunned();
        Stunned = true;
        Speed = 0;
        //enemyCollider.enabled = false;
    }
    /// <summary>
    /// become UN stunned
    /// </summary>
    public override void BeUnStunned()
    {
        Stunned = false;
        Speed = EnemyData.Speed;
        //enemyCollider.enabled = true;
    }

    /// <summary>
    /// Sets variables to those in EnemyData
    /// </summary>
    public override void SetAttributes()
    {
        Health = EnemyData.Health;
        Speed = EnemyData.Speed;
        TakeKnockback = EnemyData.TakeKnockback;
        ImmuneToElectricity = EnemyData.ImmuneToElectricity;
        StunLength = EnemyData.StunDuration;
        lightDPS = EnemyData.LightDamagePerSec;
        KnockbackForce = EnemyData.KnockBackForce;
    }

    private IEnumerator DisableMovementCoroutine()
    {
        DisableMovement = true;
        yield return new WaitForSeconds(0.1f);
        DisableMovement = false;
    }

    /// <summary>
    /// Every third of a second, enemy takes lightDamagePerSecond/3.
    /// </summary>
    /// <returns></returns>
    public IEnumerator TakeLightDamage()
    {
        while(LayersOfLight > 0)
        {
            yield return new WaitForSeconds(1/3);
            TakeDamage(lightDPS/3);
        }
    }
}
