/*******************************************************************************
// File Name :         EnemyDetection.cs
// Author(s) :         Toby Schamberger
// Creation Date :     3/23/2023
//
// Brief Description : Causes the enemy to begin pursuing the player if certain
// conditions are met.
// The enemy is constantly checking if either player is visible. If Gorp's light
// is visible. Which player is closer. Etc.
//
// TODO: 
// * make the enemy work with globbington! It's only in gorp mode currently!
// * code that detects if enemy is in light
//
// STRETCH GOAL:
// Unsight distance, so player has to travel further away than regular distance
// to avoid enemy after being seen i am so tired.
// 
// It is currently undecided if the enemies following behavior will be in this
// script! Probably not! HOPEFULLY not! If the game is finalized and you can read this comment uh you shouldn't be doing that. this comment should be gone.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyDetection : MonoBehaviour
{
    [Header("Settings")]
    public float SightDistance = 8f;
    public LayerMask LM;
    public InputAction ToggleLight;

    [Header("Debug")]
    public bool DarkVision = false;
    public float Speed = 1f;

    [Header("Dynamic Variables")]
    public  bool InsideLight;
    public  GameObject CurrentTarget;

    //Rest of variables are private jargain 
    private GameObject gorp;
    private GameObject globbington;
    private Rigidbody2D rb;

    /// <summary>
    /// Finds Gorp and Globbington and starts searching for them
    /// </summary>
    void Start()
    {
        gorp = GameObject.Find("Gorp");
        globbington = GameObject.Find("Globbington");
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(SearchForPlayer());
    }

    /// <summary>
    /// Runs every 1.5 to 4.5 seconds, instantiates BallPrefab.
    /// </summary>
    public IEnumerator SearchForPlayer()
    {
        // This while loop only runs while Gorp and Globbington don't exist at the beginning at the game.
        // Essentially keeping the enemy in purgatory.
        // Gorp will only be null at the very beginning, so I don't want the code to be constantly checking if gorp is null in the for loop.
        // Even if this is weird code.
        // This is the fifth comment I've written about like 4 lines of code
        while (gorp == null && globbington == null)
        {
            gorp        = GameObject.Find("Gorp");
            globbington = GameObject.Find("Globbington");

            yield return new WaitForSeconds(1.00f);
        }

        //The main event!
        for (; ; )
        {
            // Get the origin and direction of the raycast
            Vector3 origin = gameObject.transform.position;
            Vector3 direction = gorp.transform.position - origin;

            // Cast the raycast and get the hit information
            var hit = Physics2D.Raycast(origin, direction, SightDistance, LM);

            if(hit)
            {
                if(hit.collider.gameObject.tag.Equals("Player"))
                {
                    Debug.DrawLine(origin, gorp.transform.position, Color.green, 0.5f);

                    string hitName = hit.collider.gameObject.name;

                    //Only searching for new target when it doesnt already have one
                    if (CurrentTarget == null)
                    {
                        if (hitName.Equals("Gorp") && GorpVisibleCheck())
                        {
                            CurrentTarget = hit.collider.gameObject;
                            StartCoroutine(PursueTarget());
                        }

                        else if (hitName.Equals("Globbington") && globbington.GetComponent<PlayerController>().insideLight)
                        {
                            CurrentTarget = hit.collider.gameObject;
                            StartCoroutine(PursueTarget() );
                        }
                    }
                }
                else
                {
                    CurrentTarget = null;
                }
            }
            else
            {
                //Having this line repeated feels wrong but i cant really see any other way to do it?
                CurrentTarget = null;
            }

            yield return new WaitForSeconds(0.15f);
        }
    }

    /// <summary>
    /// Moves the enemy towards the enemies currently selected target.
    /// ends when the currentTarget is lost.
    /// </summary>
    public IEnumerator PursueTarget()
    {
        
        while(CurrentTarget!=null)
        {
            Vector2 positionDifference = Vector2.MoveTowards(transform.position, gorp.transform.position, Speed);
            Vector2 movementVelocity = positionDifference - (Vector2)transform.position;
            rb.velocity = movementVelocity;

            yield return new WaitForSeconds(0.1f);
        }

        rb.velocity = Vector2.zero;
    }

    /// <summary>
    /// Essentially checking if Gorp has his light on, and whether or not the enemy is within its sightlines.
    /// All that logic is ignored if enemy is currently inside of light
    /// Gorp should already be within sight range so that doesn't have to be checked.
    /// </summary>
    /// <returns>If gorp can be seen</returns>
    public bool GorpVisibleCheck()
    {
        GorpLightController GLC = gorp.GetComponent<GorpLightController>();
        float gorpRadius = GLC.LightRadius;

        if (InsideLight)
            return true;

        //returns false if gorp has light off
        if (!GLC.LightEnabled)
            return false;

        float distanceFromGorp = Vector2.Distance(this.transform.position, gorp.transform.position);
        if (distanceFromGorp > gorpRadius)
            return false;
        else
            return true;

    }
}
