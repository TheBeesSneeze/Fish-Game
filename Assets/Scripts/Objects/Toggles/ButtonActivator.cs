/*******************************************************************************
// File Name :         ButtonActivator.cs
// Author(s) :         Toby Schamberger, Jay Embry
// Creation Date :     4/20/2023
//
// Brief Description : Extends ActivatorType. Activates objects when globbington
// steps on it
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivator : ActivatorType
{

    public AudioClip Button;
    public AudioSource ButtonAudio;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Globbington")
        {
            DeactivationInput();

            if(ButtonAudio != null)
            {

                ButtonAudio.Play();

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Globbington")
        {
            
            ActivationInput();

            if (ButtonAudio != null)
            {

                ButtonAudio.Play();

            }
        }
    }
}
