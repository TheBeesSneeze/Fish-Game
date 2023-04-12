/*******************************************************************************
// File Name :         EnemyType.cs
// Author(s) :         Toby Schamberger
// Creation Date :     4/7/2023
//
// Brief Description : Contains a lot of configurations for different enemy types!
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "Enemy Type")]

public class EnemyType : CharacterType
{
    [Header("Enemy Exclusive:")]

    [Tooltip("If the door will open ")]
    public bool  RequiredToKill     ; //Not used in any EnemyScripts, is checked in RoomBehavior tho
    [Tooltip("If Enemy is stunned from flash")]
    public bool  StunnedByFlash     ;
    [Tooltip("If Enemy takes damage from flash")]
    public bool  HurtByFlash        ;
    [Tooltip("DPS enemy takes while inside light. 0 means damage is ignored. Negative means healing")]
    public float LightDamagePerSec  ;

    [Header("Detection:")]

    [Tooltip("Space until enemy can see you")]
    public float SightDistance      ;
    [Tooltip("Space until enemy cant see you")]
    public float UnsightDistance    ;
    [Tooltip("Seconds until the enemy realizes its looking at you")]
    public float PursueDelay        ; //IS this done?
    [Tooltip("If the enemy can pursue without light.")]
    public bool  NightVision        ;
    [Tooltip("If the enemy can pursue while in light.")]
    public bool  BlindedByTheLight  ; //TODO
    

}