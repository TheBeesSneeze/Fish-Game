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
    public bool DarkVision = false;

    [Header("Dynamic Variables")]
    public  GameObject CurrentTarget;
    private PlayerController targetController;

    [Header("You don't need to touch this:")]
    public GameObject Gorp;
    public GameObject Globbington;
    private Rigidbody2D rb;
    private EnemyBehavior enemyBehavior;

    /// <summary>
    /// Finds Gorp and Globbington and starts searching for them
    /// </summary>
    void Start()
    {
        enemyBehavior=GetComponent<EnemyBehavior>();
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
        while (Gorp == null)
        {
            Gorp        = GameObject.Find("Gorp");
            Globbington = GameObject.Find("Globbington"); //this probably wont find globbington actually.

            yield return new WaitForSeconds(1.00f);
        }
        Debug.Log("found nemo.");

        //The main event!
        for (; ; )
        {
            // Get the origin and direction of the raycast
            bool gorpVisible = CheckForVisibility(Gorp);
            bool globVisible = CheckForVisibility(Globbington);

            if (CurrentTarget == null)
            {
                if (gorpVisible)
                {
                    CurrentTarget = Gorp;
                    targetController = Gorp.GetComponent<PlayerController>();
                    StartCoroutine(PursueTarget());
                }
                else if(globVisible)
                {
                    CurrentTarget = Globbington;
                    targetController = Globbington.GetComponent<PlayerController>();
                    StartCoroutine(PursueTarget());
                }
                else
                {
                    CurrentTarget = null;
                    targetController = null;
                }
            }

            yield return new WaitForSeconds(0.15f);
        }
    }

    /// <summary>
    /// Returns true/false if the gameobject is obstructed between enemy or not.
    /// automatically checks if target is null
    /// </summary>
    /// <param name="target">Gameobject to pursue</param>
    public bool CheckForVisibility(GameObject target)
    {
        if(target == null)
            return false;

        Vector3 origin = gameObject.transform.position;
        Vector3 direction = target.transform.position - origin;

        // Cast the raycast and get the hit information
        var hit = Physics2D.Raycast(origin, direction, SightDistance, LM);

        //Searching for a victim!
        if (hit)
        {
            string hitName = hit.collider.gameObject.name;

            if (hitName == target.name)
            {
                Debug.DrawLine(origin, target.transform.position, Color.green, 0.5f);

                PlayerController tempTargetController = hit.collider.GetComponent<PlayerController>();

                if (tempTargetController.LayersOfLight > 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Moves the enemy towards the enemies currently selected target.
    /// ends when the currentTarget is lost.
    /// </summary>
    public IEnumerator PursueTarget()
    {
        while(CurrentTarget!=null)
        {
            if(targetController.LayersOfLight > 0)
            {
                Vector2 positionDifference = Vector2.MoveTowards(transform.position, CurrentTarget.transform.position, enemyBehavior.Speed);
                Vector2 movementVelocity = positionDifference - (Vector2)transform.position;
                rb.velocity = movementVelocity;
            }
            else
            {
                targetController = null;
                CurrentTarget = null;
            }
            yield return new WaitForSeconds(0.1f);
        }

        rb.velocity = Vector2.zero;
    }
}
