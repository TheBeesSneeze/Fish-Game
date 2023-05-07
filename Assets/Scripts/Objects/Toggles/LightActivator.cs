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
using static UnityEngine.GraphicsBuffer;

public class LightActivator : ActivatorType
{
    public int LayersOfLight;
    public LayerMask LM;
    private GameObject gorp;
    public bool GorpVisible;

    /// <summary>
    /// light activator detect gorp
    /// </summary>
    /// <param name="collider"></param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Light"))
        {
            LayersOfLight++;

            if (LayersOfLight == 1)
                StartCoroutine(CheckForGorpVisibility());
        }
    }

    /// <summary>
    /// no detect gorp
    /// </summary>
    /// <param name="collider"></param>
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Light"))
        {
            LayersOfLight--;

            if(LayersOfLight == 0) 
            {
                GorpVisible = false;
                DeactivationInput();
            }
        }
    }

    /// <summary>
    /// checking for gorp
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckForGorpVisibility ()
    {
        while (gorp == null)
        {
            try { gorp = GameObject.Find("Gorp"); } catch { }
            yield return new WaitForSeconds(1);
        }

        while (LayersOfLight > 0)
        {
            Debug.Log("WHERE THE FUCK IS HE");
            Vector3 origin = gameObject.transform.position;
            Vector3 direction = gorp.transform.position - origin;

            // Cast the raycast and get the hit information
            var hit = Physics2D.Raycast(origin, direction, 20, LM);


            //Searching for a victim!
            if (hit)
            {
                string hitName = hit.collider.gameObject.name;
                Debug.Log(hitName);

                if (hitName.Equals("Gorp"))
                {
                    Debug.DrawLine(origin, gorp.transform.position, Color.green, 0.5f);

                    if(!GorpVisible) 
                    {
                        ActivationInput();
                        GorpVisible = true;
                    }
                }
                else if (GorpVisible)
                {
                    DeactivationInput();
                    GorpVisible = false;
                }
            }
            else if(GorpVisible)
            {
                DeactivationInput();
                GorpVisible = false;
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    /// <summary>
    /// activate
    /// </summary>
    public override void ActivationInput()
    {
        base.ActivationInput();
        this.MySpriteRenderer.color = Color.blue;
    }

    /// <summary>
    /// deactivate
    /// </summary>
    public override void DeactivationInput()
    {
        base.DeactivationInput();
        this.MySpriteRenderer.color = Color.grey;
    }
}
