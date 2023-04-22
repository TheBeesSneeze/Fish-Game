/*******************************************************************************
// File Name :         ReactiveType.cs
// Author(s) :         Toby Schamberger
// Creation Date :     4/20/2023
//
// Brief Description : Can be activated or deactivated.
// Base interface for reactive objects. Designed to interract with ActivatorTypes
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveType : MonoBehaviour
{
    [Header("Settings")]

    [Tooltip("If this object is in its activated state")]
    public bool ActivatedByDefault;
    [Tooltip("Seconds between changes")]
    public float Delay;

    private SpriteRenderer spriteRenderer;
    [Tooltip("Not recquired")]
    public Sprite ActiveSprite;
    [Tooltip("Not recquired")]
    public Sprite DeactiveSprite;

    [Header("Debug")]
    private bool _activated;
    private Coroutine activateCoroutine;

    /// <summary>
    /// Sets this objects activation state to default
    /// </summary>
    public virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        _activated = ActivatedByDefault;

        SetActivationState(_activated);
    }

    /// <summary>
    /// Activates / Deactivates this object.
    /// Automatically calls Activate or Deactivate function.
    /// </summary>
    /// <param name="activated"></param>
    public void SetActivationState(bool activated)
    {
        _activated = activated;

        //cancels activation coroutine if its already happening
        if(activateCoroutine != null ) 
        {
            StopCoroutine(activateCoroutine);
            activateCoroutine = null;
        }

        activateCoroutine = StartCoroutine(SetState());
    }

    /// <summary>
    /// a GETTER????
    /// </summary>
    public bool GetActivationState()
    {
        return _activated;
    }

    /// <summary>
    /// Calls appropriate active / deactive functions after delay.
    /// </summary>
    private IEnumerator SetState()
    {
        yield return new WaitForSeconds(Delay);

        if (_activated)
        {
            OnActivate();
            if (ActiveSprite != null)
                spriteRenderer.sprite = ActiveSprite;
        }

        else
        {
            OnDeactivate();
            if(DeactiveSprite != null)
                spriteRenderer.sprite = DeactiveSprite;
        }

        activateCoroutine = null;
    }

    /// <summary>
    /// Overwrite me!!!
    /// </summary>
    public virtual void OnActivate() { }

    /// <summary>
    /// Overwrite me!!!
    /// </summary>
    public virtual void OnDeactivate() { }
}
