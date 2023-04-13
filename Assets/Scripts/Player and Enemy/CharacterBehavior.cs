/*******************************************************************************
// File Name :         CharacterBehavior.cs
// Author(s) :         Toby Schamberger, Sky Beal
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
    public float Health;
    public float Speed;
    public bool TakeKnockback;
    public bool ImmuneToElectricity;
    
    public Vector3 DefaultPosition;
    public bool Stunned;
    public float StunLength;
    public Rigidbody2D MyRB;
    public float KnockbackForce;

    /// <summary>
    /// Sets Health to the Default
    /// </summary>
    public virtual void Start()
    {
        DefaultPosition = this.transform.position;
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
            Die();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Knocks back target away from damageSourcePosition
    /// </summary>
    /// <param name="target"></param>
    /// <param name="damageSourcePosition"></param>
    public virtual void KnockBack(GameObject target, Vector3 damageSourcePosition)
    {
        MyRB = this.GetComponent<Rigidbody2D>();
        Vector3 positionDifference = target.transform.position - damageSourcePosition; 
        //target.transform.position -= positionDifference;
        MyRB.AddForce(positionDifference * KnockbackForce, ForceMode2D.Impulse);
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
        Debug.Log("Override SetAttributes");
    }

    /// <summary>
    /// if the character is not immune to electricity they will get stunned
    /// TODO: that
    /// </summary>
    public virtual void GetElectrified()
    {
        if( ! ImmuneToElectricity ) 
        {
            //Stun
            TakeDamage(1);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag.Equals("Light"))
        {
            LayersOfLight++;
        }
        if (tag.Equals("Electricity") && !ImmuneToElectricity)
        {
            Debug.Log(collision.gameObject.name + " was zapped! idk how this code is gonna work yet tbh");
            GetElectrified();
        }
        if (tag.Equals("Flash"))
        {
            BeStunned();
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
    /// Compares the position of every gameobject with tag to point.
    /// </summary>
    /// <returns>closest distance</returns>
    public float GetDistanceOfClosestTag(Vector2 Point, string Tag)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(Tag);

        float min = -1;

        if(objs.Length > 0) // yk, javascript wouldnt make me do this
        {
            min = Vector2.Distance(Point, objs[0].transform.position);

            for (int i = 0; i < objs.Length; i++)
            {
                float dist = Vector2.Distance(Point, objs[i].transform.position);

                if (min > dist)
                    min = dist;
            }
        }
        return min;
    }

    public virtual void BeStunned()
    {
        Stunned = true;
        Invoke("BeUnStunned", StunLength);
    }

    public virtual void BeUnStunned()
    {
        Stunned = false;
    }
}
