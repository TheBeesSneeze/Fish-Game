using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "Enemy Type")]

public class EnemyType : CharacterType
{
    [Header("Enemy Exclusive")]
    public bool NightVision;
    public bool StunnedByLight;
}