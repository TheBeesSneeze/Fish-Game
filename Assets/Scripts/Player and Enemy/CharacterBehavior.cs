/*******************************************************************************
// File Name :         CharacterBehavior.cs
// Author(s) :         Toby Schamberger, Sky Beal, Jay Embry
// Creation Date :     3/28/2023
//
// Brief Description : Basic code that is shared between players and enemies.
// Includes stuff like taking damage, knockback, etc
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehavior : ObjectType
{    
    public float Health;
    public float Speed;
    public bool TakeKnockback;
    public bool ImmuneToElectricity;
    
    public bool Stunned;
    public float StunLength;
    public Rigidbody2D MyRB;
    public float KnockbackForce;


    /// <summary>
    /// Hot potato!
    /// </summary>
    public override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// Decreases the enemies health
    /// TODO: can't be done until globbingtons attack done.
    /// </summary>
    /// <param name="damage">Amt of damage taken</param>
    /// <param name="damageSourcePosition">Ideally the players transform</param>
    /// <returns>true if character died</returns>
    public virtual bool TakeDamage(float damage, Vector3 damageSourcePosition)
    {
        if ( TakeDamage(damage) )
            return true;

        if(TakeKnockback)
            KnockBack(this.gameObject, damageSourcePosition);

        return false;
    }

    /// <summary>
    /// Takes damage without any knockback
    /// </summary>
    /// <param name="damage">Amt of damage taken</param>
    /// <returns>true if character died</returns>
    public virtual bool TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Despawn();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Knocks back target away from damageSourcePosition
    /// </summary>
    /// <param name="target">who is getting knockedback</param>
    /// <param name="damageSourcePosition">where they are getting knocked back from</param>
    public virtual void KnockBack(GameObject target, Vector3 damageSourcePosition)
    {
        KnockBack(target, damageSourcePosition, KnockbackForce);
    }

    /// <summary>
    /// Knockback override to include knockbackforce override
    /// </summary>
    /// <param name="target">who is getting knockedback</param>
    /// <param name="damageSourcePosition">where they are getting knocked back from</param>
    /// <param name="Force">how much to multiply knockback by</param>
    public virtual void KnockBack(GameObject target, Vector3 damageSourcePosition, float Force)
    {
        MyRB = this.GetComponent<Rigidbody2D>();
        Vector3 positionDifference = target.transform.position - damageSourcePosition;
        MyRB.AddForce(positionDifference * Force, ForceMode2D.Impulse);
    }

    /// <summary>
    /// sets attributes on respawn
    /// </summary>
    public override void Respawn()
    {
        base.Respawn();
        SetAttributes();
    }

    /// <summary>
    /// Sets variables to those in EnemyData
    /// </summary>
    public virtual void SetAttributes()
    {
        Debug.Log("Override SetAttributes");
    }

    /// <summary>
    /// if the character is not immune to electricity they will get stunned
    /// TODO: that
    /// </summary>
    public virtual void GetElectrified()
    {
        GetElectrified(StunLength);
    }

    /// <summary>
    /// Electricity override that configures time stunned
    /// </summary>
    /// <param name="Seconds">Time stunned</param>
    public virtual void GetElectrified(float Seconds)
    {
        if (!ImmuneToElectricity)
        {
            Stunned = true;
            BeStunned();
            TakeDamage(1);
        }
    }
    /// <summary>
    /// collisions for the characters
    /// </summary>
    /// <param name="collision"></param>
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag.Equals("Light"))
        {
            LayersOfLight++;
        }
        if (tag.Equals("Electricity") && !ImmuneToElectricity)
        {
            GetElectrified();
        }
        if (tag.Equals("Flash"))
        {
            BeStunned();
        }
    }

    /// <summary>
    /// Stuns character for their StunLength
    /// </summary>
    public virtual void BeStunned()
    {
        BeStunned(StunLength);
    }

    /// <summary>
    /// Stuns character for Seconds
    /// </summary>
    /// <param name="Seconds">Seconds to be stunned</param>
    public virtual void BeStunned(float Seconds)
    {
        Stunned = true;
        Invoke("BeUnStunned", Seconds);
    }

    /// <summary>
    /// become unstunned
    /// </summary>
    public virtual void BeUnStunned()
    {
        Stunned = false;
    }

    
}
