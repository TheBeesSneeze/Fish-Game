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
using Unity.VisualScripting;
using UnityEngine;

public class CharacterBehavior : MonoBehaviour
{    
    
    
    [Header("Debug (don't touch in editor)")]

    public int LayersOfLight;
    public int Health;
    public float Speed;
    public bool TakeKnockback; 
    
    public float Weight; 
    public Vector3 DefaultPosition;

    /// <summary>
    /// Sets Health to the Default
    /// </summary>
    public virtual void Start()
    {
        SetAttributes();
        DefaultPosition = this.transform.position;
    }



    /// <summary>
    /// Decreases the enemies health
    /// TODO: can't be done until globbingtons attack done.
    /// </summary>
    /// <param name="damage">Amt of damage taken</param>
    /// <param name="takeKnockback">Whether the enemy moves away from damageSourcePosition</param>
    /// <param name="damageSourcePosition">Ideally the players transform</param>
    public virtual void TakeDamage(int damage, Vector3 damageSourcePosition)
    {
        Health -= damage;

        if(Health <= 0) 
            Die();

        else if(TakeKnockback)
            KnockBack(this.gameObject, damageSourcePosition);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        
        if (tag.Equals("Light"))
        {
            LayersOfLight++;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("Light"))
        {
            LayersOfLight--;
        }
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
        Debug.Log(this.gameObject.name + " has died! if youre reading this text, you will soon! override this function!");
    }

    public virtual void Respawn()
    {
        this.gameObject.SetActive(true);
        this.transform.position = DefaultPosition;
        SetAttributes();
    }

    /// <summary>
    /// Sets variables to those in EnemyData
    /// </summary>
    public virtual void SetAttributes()
    {
        Debug.Log("Override this function!!!!");
    }
}
