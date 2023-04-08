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

    public bool  RequiredToKill     ; //Not used in any EnemyScripts, is checked in RoomBehavior tho
    public bool  StunnedByFlash     ; //TODO
    public bool  HurtByFlash        ; //TODO
    public float StunDuration       ; //TODO

    public bool  ImmuneToElectricity; //TODO*

    [Header("Detection:")]

    public float SightDistance      ;
    public float UnsightDistance    ;
    public float PursueDelay        ;
    public bool  NightVision        ; 

    

}