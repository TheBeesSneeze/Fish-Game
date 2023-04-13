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
using UnityEditor.U2D.Animation;
using UnityEngine;

public class EnemyBehavior : CharacterBehavior
{
    [Header("Attributes")]
    public bool DespawnOnStart = true;
    public EnemyType EnemyData;

    [Header("Unity Jargain")]
    private EnemyDetection enemyDetection;
    private Collider2D enemyCollider;

    [Header("You don't need to touch this:")]
    public GameObject Gorp;
    public GameObject Globbington;
    private float lightDPS;

    /// <summary>
    /// Initializes variables for enemy respwning and finds unity components.
    /// </summary>
    public override void Start()
    {
        SetAttributes();
        DefaultPosition = this.transform.position;

        //Unity moment
        enemyDetection = gameObject.GetComponent<EnemyDetection>();
        enemyCollider  = gameObject.GetComponent<Collider2D>();

        Gorp        = GameObject.Find("Gorp");
        Globbington = GameObject.Find("Globbington");

        this.gameObject.SetActive(!DespawnOnStart);
    }

    /// <summary>
    /// Kills the enemy! (Actually just SetActive(false)
    /// </summary>
    public override void Die()
    {
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Basically resets the enemy. It's like how it was at the start of the scene!
    /// </summary>
    public override void Respawn()
    {
        base.Respawn();
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

        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if(tag.Equals("Attack"))
        {
            //if enemy needs to be stunned
            if( (EnemyData.ProtectedUntilFlash && Stunned) || ! EnemyData.ProtectedUntilFlash )
                TakeDamage(1, collision.gameObject.transform.position);
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

    public override void BeStunned()
    {
        base.BeStunned();
        Stunned = true;
        Speed = 0;
        enemyCollider.enabled = false;
    }
    public override void BeUnStunned()
    {
        Stunned = false;
        Speed = EnemyData.Speed;
        enemyCollider.enabled = true;
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
