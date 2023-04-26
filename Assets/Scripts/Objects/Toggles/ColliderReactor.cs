/*******************************************************************************
// File Name :         SpikeReactor.cs
// Author(s) :         Toby Schamberger; Sky Beal
// Creation Date :     4/20/2023
//
// Brief Description : Can be activated or deactivated.
// Activated:   Spike go up
// Deactivated: Spike go down
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderReactor : ReactiveType
{
    private Collider2D myCollider;

    /// <summary>
    /// gets collider
    /// </summary>
    public virtual void Start()
    {
        myCollider = GetComponent<Collider2D>();
    }

    /// <summary>
    /// Gate goes up.
    /// </summary>
    public override void OnActivate()
    {
        myCollider.enabled = true;
    }

    /// <summary>
    /// Gate go down
    /// </summary>
    public override void OnDeactivate()
    {
        myCollider.enabled = false;
    }
}
