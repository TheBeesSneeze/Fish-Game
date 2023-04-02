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
    [Header("Enemy Attributes")]
    public bool NightVision;

    [Header("Unity Jargain")]
    private EnemyDetection enemyDetection;
    private GameObject gorp;
    private GameObject globbington;

    /// <summary>
    /// Initializes variables for enemy respwning and finds unity components.
    /// </summary>
    void Start()
    {
        //Stats
        Health = DefaultHealth;
        DefaultPosition = this.gameObject.transform.position;

        //Unity moment
        enemyDetection = gameObject.GetComponent<EnemyDetection>();
        gorp        = GameObject.Find("Gorp");
        globbington = GameObject.Find("Globbington");
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
        this.gameObject.SetActive(true);
        Health = DefaultHealth;
        this.transform.position = DefaultPosition;

        //Enemy exclusive code:
        enemyDetection.CurrentTarget = null;
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

            if(enemyDetection!=null)
                enemyDetection.InsideLight = true;
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
                enemyDetection.InsideLight = false;

                if (enemyDetection.CurrentTarget != null) //so many if statements that have to be separate
                {
                    if (enemyDetection.CurrentTarget.GetComponent<CharacterBehavior>().LayersOfLight <= 0)
                        enemyDetection.CurrentTarget = null;
                }  
                    
            }
                
        }
    }
}
