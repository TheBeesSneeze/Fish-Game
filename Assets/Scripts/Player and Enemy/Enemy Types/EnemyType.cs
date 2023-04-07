using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyType", menuName = "Enemy Type")]

public class EnemyType : CharacterType
{
    [Header("Enemy Exclusive")]
    public float SightDistance; //TODO
    public float UnsightDistance; //TODO

    public bool NightVision; //TODO
    public bool StunnedByFlash; //TODO
    public bool HurtByFlash; //TODO
    public float StunDuration; //TODO
    public bool ImmuneToElectricity; //TODO

}