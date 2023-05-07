using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*******************************************************************************
// File Name :         SpikeBehavior.cs
// Author(s) :         Sky Beal, Toby Schamberger
// Creation Date :     4/11/2023
//
// Brief Description : Basic behaviour for spikes.
*****************************************************************************/

public class SpikeBehaviour : MonoBehaviour
{
    /// <summary>
    /// touch spike ouchie
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("Player") || tag.Equals("Enemy"))
        {
            collision.GetComponent<CharacterBehavior>().TakeDamage(1, this.transform.position);
        }
    }

    /// <summary>
    /// collide with spike
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnter2D(collision.collider);
    }
}
