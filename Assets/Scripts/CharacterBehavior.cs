/*******************************************************************************
// File Name :         CharacterBehavior.cs
// Author(s) :         Toby Schamberger
// Creation Date :     3/28/2023
//
// Brief Description : Basic code that is shared between players and enemies.
// Includes stuff like taking damage, knockback, etc
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehavior : MonoBehaviour
{
    [Header("Attributes")]
    public bool Weight; // Weight determines distance knocked back. 0 = no knockback. 10 = across the room
    public int DefaultHealth;

    [Header("Debug (don't touch in editor)")]
    public int Health;

    /// <summary>
    /// Sets Health to the Default
    /// </summary>
    void Start()
    {
        Health = DefaultHealth;
    }

    /// <summary>
    /// Decreases the enemies health
    /// TODO: can't be done until globbingtons attack done.
    /// </summary>
    /// <param name="damage">Amt of damage taken</param>
    /// <param name="takeKnockback">Whether the enemy moves away from damageSourcePosition</param>
    /// <param name="damageSourcePosition">Ideally the players transform</param>
    public virtual void TakeDamage(int damage, bool takeKnockback, Vector3 damageSourcePosition)
    {
        Health -= damage;

        if(Health <= 0) 
            Die();

        else if(takeKnockback)
            KnockBack(this.gameObject, damageSourcePosition);
    }

    /// <summary>
    /// Knocks back target away from damageSourcePosition
    /// </summary>
    /// <param name="target"></param>
    /// <param name="damageSourcePosition"></param>
    public virtual void KnockBack(GameObject target, Vector3 damageSourcePosition)
    {
        Vector3 positionDifference = damageSourcePosition - this.gameObject.transform.position;
        this.transform.position -= positionDifference;
    }

    /// <summary>
    /// Kills the enemy!
    /// </summary>
    public virtual void Die()
    {
        Debug.Log(this.gameObject.name + " has died! if youre reading this text! you will soon! override this function!");
    }
}
