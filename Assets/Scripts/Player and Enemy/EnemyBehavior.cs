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
    private bool stunnedByLight;

    [Header("Unity Jargain")]
    private EnemyDetection enemyDetection;

    [Header("You don't need to touch this:")]
    public GameObject Gorp;
    public GameObject Globbington;

    /// <summary>
    /// Initializes variables for enemy respwning and finds unity components.
    /// </summary>
    public override void Start()
    {
        SetAttributes();
        DefaultPosition = this.transform.position;

        //Unity moment
        enemyDetection = gameObject.GetComponent<EnemyDetection>();
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
            TakeDamage(1, collision.gameObject.transform.position);
        }
        else if (tag.Equals("Light"))
        {
            LayersOfLight++;
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

    /// <summary>
    /// Sets variables to those in EnemyData
    /// </summary>
    public override void SetAttributes()
    {
        Health = EnemyData.Health;
        Speed = EnemyData.Speed;
        Weight = EnemyData.Weight;
        TakeKnockback = EnemyData.TakeKnockback;

    }
}
