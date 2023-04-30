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
/* Zach Note
  * Interface is a weird term for this. That's a whole thing so just a vocab thing to look out for. That 
  * being said, this is cool as heck. 
  */
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
        SetStateOfObjects(Active);
    }

    /// <summary>
    /// Call this function when the thing is activated
    /// </summary>
    public virtual void ActivationInput()
    {
        SetStateOfObjects(true);
    }

    /// <summary>
    /// Call this function when the thing is deactivated
    /// </summary>
    public virtual void DeactivationInput()
    {
        SetStateOfObjects(false);
    }

    /// <summary>
    /// Switches between active and inactive
    /// </summary>
    public virtual void ToggleInput()
    {
        foreach (ReactiveType item in LinkedObjects)
        {
            item.SetActivationState(!item.GetActivationState());
        }
        this.SetActivationState(!GetActivationState());
        //SetStateOfObjects( ! GetActivationState() );
    }

    /// <summary>
    /// Sets the Activation state of this ReactionType and all items
    /// </summary>
    /// <param name="state"></param>
    private void SetStateOfObjects(bool state) 
    {
        bool stateChanged = state != this.GetActivationState();

        if(stateChanged) 
        {
            foreach (ReactiveType item in LinkedObjects)
            {
                item.ToggleState();
            }
            this.SetActivationState(state);
        }
        
    }
}
