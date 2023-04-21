/*******************************************************************************
// File Name :         EyeBehaviour.cs
// Author(s) :         Toby Schamberger
// Creation Date :     4/16/2023
//
// This code was made on a whim. Searches for gorp, when gorp is visible, it
// searches for things affected by light (mirrors and enemies)
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.UI.Image;

public class EyeBehaviour : MonoBehaviour
{
    [Header("Settings")]
    public bool SeeEnemies;
    public LayerMask LM;
    public float RayCastDistance;
    public float AngleRecalculationDelay = 0.2f;
    [Tooltip("How frequently eye checks if player is still visible to eye")]
    public float PlayerSearchDelay = 1;
    public float RotateSpeed = 5;

    [Header("Unity Stuff")]
    public Light2D Light;
    public GameObject LightAnchor;

    [Header("Debug")]
    private Coroutine gazing;
    private GameObject[] players;
    public GameObject[] visibleTargets = new GameObject[2];

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SearchForPlayers());
    }

    /// <summary>
    /// Sends raycast to all players present.
    /// If any are found, starts gazing coroutine.
    /// This function is only checking if players are visible, so it
    /// does not run very frequently
    /// </summary>
    /// <returns></returns>
    private IEnumerator SearchForPlayers()
    {
        LightAnchor.SetActive(true);
        while(true) 
        {
            players = GameObject.FindGameObjectsWithTag("Player");

            //Raycast visibility
            for (int i = players.Length-1; i>=0; i--)
            { // starts at end of list to prioritize globbington

                Vector3 sourcePosition = players[i].transform.position;
                Vector3 origin = LightAnchor.transform.position;
                Vector3 direction = sourcePosition - origin;

                var hit = Physics2D.Raycast(origin, direction, RayCastDistance, LM);
                Debug.DrawLine(origin, hit.point, Color.green, 0.5f);

                if(hit)
                {
                    string tag = hit.collider.tag;

                    if (tag.Equals("Player"))
                    {
                        //this cant be an && statement because i am not feeling computer science-y today
                        if(gazing == null)
                        {
                            visibleTargets[i] = players[i];
                            hit.collider.GetComponent<PlayerController>().LayersOfLight++;

                            gazing = StartCoroutine(CalculateGaze(players[i]));
                        }
                        
                    }
                    //if it missed and the coroutine needs to stop now
                    else if (visibleTargets[i] != null)
                    {
                        StopCoroutine(gazing);
                        gazing = null;
                        LightAnchor.SetActive(false);

                        visibleTargets[i].GetComponent<PlayerController>().LayersOfLight--;
                        visibleTargets[i] = null;

                        //other player index
                        int j;
                        if (i == 0) j = 1;
                        else        j = 0;

                        if (visibleTargets[j] != null)
                            StartCoroutine(CalculateGaze(visibleTargets[j]));
            
                    }
                }
            }

            yield return new WaitForSeconds(PlayerSearchDelay);
        }
    }

    /// <summary>
    /// Constantly rotates the mirror to face away from the player. Like a real light bouncing off.
    /// Does a lot of cool math that I copied and pasted from online.
    /// </summary>
    private IEnumerator CalculateGaze(GameObject target)
    {
        LightAnchor.SetActive(true);
        while (true)
        {
            Vector3 sourcePosition = target.transform.position;

            //vector between two points
            Vector2 Direction = transform.position - sourcePosition;
            float angle = Vector2.SignedAngle(Vector2.right, Direction) + 90;

            Vector3 TargetRotation = new Vector3(0, 0, angle);
            LightAnchor.transform.rotation = Quaternion.RotateTowards(LightAnchor.transform.rotation, Quaternion.Euler(TargetRotation), RotateSpeed);

            yield return new WaitForSeconds(AngleRecalculationDelay);
        }
    }

   
}
