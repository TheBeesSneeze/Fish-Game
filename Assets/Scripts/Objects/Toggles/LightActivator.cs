/*******************************************************************************
// File Name :         LightActivator.cs
// Author(s) :         Toby Schamberger
// Creation Date :     4/20/2023
//
// Brief Description : Extends ActivatorType. Activates objects when any light
// source hits it
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightActivator : ActivatorType
{
    public int LayersOfLight;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Light"))
        {
            LayersOfLight++;
            
            if(LayersOfLight == 1)
                ActivationInput();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Light"))
        {
            LayersOfLight--;

            if(LayersOfLight == 0) 
            {
                DeactivationInput();
            }
            
        }
    }
}
