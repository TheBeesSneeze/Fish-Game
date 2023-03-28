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
    public float Speed; //if speed = 0, enemy wont move at all

    [Header("Unity Jargain")]
    private EnemyDetection enemyDetection;
    private GameObject gorp;
    private GameObject globbington;

    // Start is called before the first frame update
    void Start()
    {
        Health = DefaultHealth;
        enemyDetection = gameObject.GetComponent<EnemyDetection>();
        gorp        = GameObject.Find("Gorp");
        globbington = GameObject.Find("Globbington");
    }

    /// <summary>
    /// Kills the enemy!
    /// </summary>
    public override void Die()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if(tag.Equals("Attack"))
        {
            TakeDamage(1, true,collision.gameObject.transform.position);
        }
    }
}
