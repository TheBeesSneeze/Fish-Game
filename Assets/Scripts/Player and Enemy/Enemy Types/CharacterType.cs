/*******************************************************************************
// File Name :         CharacterType.cs
// Author :            Toby Schamberger
// Creation Date :     4/6/2023
//
// Brief Description : Code that stores attributes about gorp, globbington,
// enemies extend this script
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterType", menuName = "Character Type")]

public class CharacterType : ScriptableObject
{
    [Header("Settings")]

    [Tooltip("How much health the character has by default")]
    public int Health;
    [Tooltip("Speed of character. 0 means no movement")]
    public float Speed;
    [Tooltip("Weight determines distance knocked back. 0 = no knockback. 10 = across the room.less than 0 is funny.")]
    public float Weight; //REMOVE
    [Tooltip("If damage knocks character back")]
    public bool TakeKnockback;
    [Tooltip("If the character takes damage from electricity")]
    public bool ImmuneToElectricity; //TODO*
    [Tooltip("How long character will be stunned for")]
    public float StunDuration;
    [Tooltip("How much to multiply knockback by")]
    public float KnockBackForce;
    [Tooltip("PLAYER EXCLUSIVE How much to multiply dash by")]
    public float DashForce; //TODO
}
