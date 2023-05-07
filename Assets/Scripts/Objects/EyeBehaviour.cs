/*******************************************************************************
// File Name :         EyeBehaviour.cs
// Author(s) :         Toby Schamberger
// Creation Date :     4/16/2023
//
// Extends ObjectType. Looks at first player it can.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.UI.Image;

public class EyeBehaviour : ObjectType
{
    [Header("Settings")]

    [Tooltip("i dont remeber if this works")]
    public bool SeeEnemies;
    public LayerMask LM;
    public float RayCastDistance;
    public float AngleRecalculationDelay = 0.2f;
    [Tooltip("How frequently eye checks if player is still visible to eye")]
    public float PlayerSearchDelay = 1;
    public float RotateSpeed = 5;
    public float BlindLength = 10;

    [Header("Unity Stuff")]
    public Light2D Light;
    public GameObject LightAnchor;
    public ActivatorType EyeActivator; //unity gets this guy automatically :D ;D

    [Header("Debug")]
    private Coroutine gazing;
    private GameObject[] players;
    public GameObject[] visibleTargets = new GameObject[2];
    public bool Blinded;
    private Coroutine blindCoroutine;
    private bool active;
    
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        DisableLight();

        if (this.gameObject.activeSelf)
            StartCoroutine(SearchForPlayers());

        this.gameObject.TryGetComponent<ActivatorType>(out EyeActivator);
    }

    /// <summary>
    /// Iterates through both players and looks for them. What a stalker!
    /// </summary>
    /// <returns></returns>
    private IEnumerator SearchForPlayers()
    {
        LightAnchor.SetActive(true);

        //dont start *Really* looking until both players are present
        while (players.Length <2) 
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            yield return new WaitForSeconds(1);
        }

        while(!Blinded && active) 
        {
            //Raycast visibility
            for (int i = players.Length-1; i>=0; i--)
            { // starts at end of list to prioritize globbington

                LookForPlayer(players[i], i);
            }

            yield return new WaitForSeconds(PlayerSearchDelay);
        }
    }

    /// <summary>
    /// Sends raycast to all players present.
    /// If any are found, starts gazing coroutine.
    /// This function is only checking if players are visible, so it
    /// does not run very frequently
    /// </summary>
    /// <param name="player"></param>
    /// <param name="i"></param>
    private void LookForPlayer(GameObject player, int i)
    {
        Vector3 sourcePosition = player.transform.position;
        Vector3 origin = LightAnchor.transform.position;
        Vector3 direction = sourcePosition - origin;

        var hit = Physics2D.Raycast(origin, direction, RayCastDistance, LM);
        Debug.DrawLine(origin, hit.point, Color.green, 0.5f);

        if (hit)
        {
            string tag = hit.collider.tag;

            if (tag.Equals("Player"))
            {
                //this cant be an && statement because i am not feeling computer science-y today
                if (gazing == null)
                {
                    visibleTargets[i] = player;
                    hit.collider.GetComponent<PlayerController>().LayersOfLight++;

                    gazing = StartCoroutine(CalculateGaze(player));

                    if (EyeActivator != null)
                        EyeActivator.ActivationInput();
                }

            }

            //if it missed and the coroutine needs to stop now
            else if (visibleTargets[i] != null)
            {
                DisableLight();

                visibleTargets[i].GetComponent<PlayerController>().LayersOfLight--;
                visibleTargets[i] = null;

                //other player index
                int j;
                if (i == 0) j = 1;
                else j = 0;

                //Redirecting gaze...
                if (visibleTargets[j] != null)
                    StartCoroutine(CalculateGaze(visibleTargets[j]));

                else
                {
                    if (EyeActivator != null)
                        EyeActivator.DeactivationInput();
                }
            }
            else
                DisableLight();

        }
    }

    /// <summary>
    /// Turns off light really.
    /// </summary>
    private void DisableLight()
    {
        if (gazing!= null)
        {
            StopCoroutine(gazing);
            gazing = null;

            if (EyeActivator != null)
                EyeActivator.ActivationInput();
        }
        
        LightAnchor.SetActive(false);

        
    }

    /// <summary>
    /// Constantly rotates the mir- eye to face away from the player. Like a real light bouncing off.
    /// Does a lot of cool math that I copied and pasted from online.
    /// </summary>
    private IEnumerator CalculateGaze(GameObject target)
    {
        LightAnchor.SetActive(true);
        Vector3 sourcePosition = Vector3.zero;

        while (true)
        {
            //only recalculates gaze if it needs too!
            //this is efficient and it 
            if(sourcePosition != target.transform.position)
            {
                sourcePosition = target.transform.position;

                //vector between two points
                Vector2 Direction = transform.position - sourcePosition;
                float angle = Vector2.SignedAngle(Vector2.right, Direction) + 90;

                Vector3 TargetRotation = new Vector3(0, 0, angle);
                LightAnchor.transform.rotation = Quaternion.RotateTowards(LightAnchor.transform.rotation, Quaternion.Euler(TargetRotation), RotateSpeed);
            }
            

            yield return new WaitForSeconds(AngleRecalculationDelay);
        }
    }

    /// <summary>
    /// stops the light.
    /// </summary>
    public void BecomeBlind()
    {
        Blinded = true;
        DisableLight();
    }

    /// <summary>
    /// Runs after being blind. Waits for a delay
    /// </summary>
    /// <returns></returns>
    private IEnumerator BecomeUnblind()
    {
        yield return new WaitForSeconds(BlindLength);
        blindCoroutine = null;

        Blinded = false;

        StartCoroutine(SearchForPlayers());
    }

    /// <summary>
    /// Activates light
    /// </summary>
    public override void Respawn()
    {
        base.Respawn();
        active = true;
        StartCoroutine(SearchForPlayers());
        
    }

    /// <summary>
    /// deactivates the light
    /// </summary>
    public override void Despawn()
    {
        base.Despawn();
        StopAllCoroutines();
        active = false;
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;

        if(tag.Equals("Flash"))
        {
            if(blindCoroutine != null)
            {
                StopCoroutine(blindCoroutine);
                blindCoroutine = null;
            }

            BecomeBlind();
            blindCoroutine = StartCoroutine(BecomeUnblind());
        }
        
    }

}
