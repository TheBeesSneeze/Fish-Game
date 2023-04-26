/*******************************************************************************
// File Name :         ActivatorType.cs
// Author(s) :         Toby Schamberger
// Creation Date :     4/20/2023
//
// Brief Description : Base interface for activator objects. Designed to interract
// with ReactiveTypes. Extends ReactiveType because 
// Can be used without extensions (see Eyes and EyeBehavior)
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorType : ReactiveType
{
    [Header("Settings")]

    public List<ReactiveType> LinkedObjects = new List<ReactiveType>();

    public virtual void Start()
    {
        this.SetActivationState(GetActivationState());
    }

    /// <summary>
    /// Call this function when the thing is what it is
    /// </summary>
    public virtual void SetActivationInput(bool Active)
    {
        SetState(Active);
    }

    /// <summary>
    /// Call this function when the thing is activated
    /// </summary>
    public virtual void ActivationInput()
    {
        SetState(true);
    }

    /// <summary>
    /// Call this function when the thing is deactivated
    /// </summary>
    public virtual void DeactivationInput()
    {
        SetState(false);
    }

    /// <summary>
    /// Switches between active and inactive
    /// </summary>
    public virtual void ToggleInput()
    {
        SetState( ! GetActivationState() );
    }

    /// <summary>
    /// Sets the Activation state of this ReactionType and all items
    /// </summary>
    /// <param name="state"></param>
    private void SetState(bool state) 
    {
        foreach (ReactiveType item in LinkedObjects)
        {
            if(item.ActivatedByDefault == this.ActivatedByDefault)
                item.SetActivationState(  state );
            else
                item.SetActivationState( !state );
        }
        this.SetActivationState(state);
    }
}
