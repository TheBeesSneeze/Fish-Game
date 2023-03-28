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

public class EnemyBehavior : MonoBehaviour
{
    [Header("Enemy Attributes")]
    public bool NightVision;
    public bool Weight; // Weight determines distance knocked back. 0 = no knockback. 10 = across the room
    public float MaxHealth;
    public float Speed; //if speed = 0, enemy wont move at all

    [Header("Unity Jargain")]
    private EnemyDetection enemyDetection;
    private GameObject gorp;
    private GameObject globbington;

    [Header("Debug (don't touch in editor)")]
    public float Health;

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        enemyDetection = gameObject.GetComponent<EnemyDetection>();
        gorp        = GameObject.Find("Gorp");
        globbington = GameObject.Find("Globbington");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
