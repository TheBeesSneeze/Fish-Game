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
    public int Health;
    public float Speed;
    public float Weight;

    public bool TakeKnockback;
}
