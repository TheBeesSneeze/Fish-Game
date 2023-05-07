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
    public GameObject CurrentTarget;
    private GameObject[] players;
    private GameObject[] visibleTargets = new GameObject[2];
    public bool Blinded;
    private Coroutine blindCoroutine;
    private bool active;
    
    /// <summary>
    /// starts coroutines
    /// </summary>
    public override void Start()
    {
        base.Start();
        DisableLight();

        players = new GameObject[0];

        if (this.gameObject.activeSelf)
            StartCoroutine(SearchForPlayers());

        this.gameObject.TryGetComponent<ActivatorType>(out EyeActivator);
    }

    /// <summary>
    /// First looks checks if CurrentTarget can still be seen. if not,
    /// it looks for a new target.
    /// </summary>
    private IEnumerator SearchForPlayers()
    {
        LightAnchor.SetActive(true);

        //dont start *Really* looking until both players are present
        while (players.Length < 2) 
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            yield return new WaitForSeconds(1);
        }

        while(active) 
        {
            if(!Blinded)
            {
                //check that CurrentTarget is still there
                if(CurrentTarget != null)
                {
                    GameObject temp = LookForPlayer(CurrentTarget); //this can be null 
                    if(temp == null)
                        CurrentTarget.GetComponent<PlayerController>().LayersOfLight--;

                    CurrentTarget = temp;
                }
                
                if (CurrentTarget == null)
                {
                    FindNewTarget();
                }
            }

            yield return new WaitForSeconds(PlayerSearchDelay);
        }
    }

    /// <summary>
    /// looks for both players and chooses whichever one it sees first.
    /// only call this function if CurrentTarget is null
    /// </summary>
    private void FindNewTarget()
    {
        GameObject player1 = LookForPlayer(players[0]);
        GameObject player2 = LookForPlayer(players[1]);

        //whoops i gues no one was found
        if (player1 == null && player2 == null)
        {
            // I LOVE NULL CHECKS!!!!!!!

            if(CurrentTarget != null)
            {
                CurrentTarget.GetComponent<PlayerController>().LayersOfLight--;
                CurrentTarget = null;
            }
                
            if (EyeActivator != null)
                EyeActivator.DeactivationInput();
        }

        // if at least one player is visible
        // if its already looking, dont worry about it
        if (player1 != null || player2 != null)
        {
            //okay so it is possible for both players to be in view at the same time. in that case,
            // the eye prioritizes (i think) gorp.
            if (player1 != null)
            {
                CurrentTarget = player1;
            }

            else if (player2 != null)
            {
                CurrentTarget = player2;
            }

            gazing = StartCoroutine(CalculateGaze());
            CurrentTarget.GetComponent<PlayerController>().LayersOfLight++;

            if (EyeActivator != null)
                EyeActivator.ActivationInput();
        }
    }

    /// <summary>
    /// Shoots a raycast to player. returns the player gameobject
    /// if it can be seen
    /// </summary>
    /// <param name="player"></param>
    private GameObject LookForPlayer(GameObject player)
    {
        if(player == null)
            return null;

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
                return hit.collider.gameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// Turns off light really.
    /// </summary>
    private void DisableLight()
    {
        if (gazing != null)
        {
            StopCoroutine(gazing);
            gazing = null;
        }
        if (EyeActivator != null)
            EyeActivator.DeactivationInput();

        LightAnchor.SetActive(false);
    }

    /// <summary>
    /// Constantly rotates the mir- eye to face away from the player. Like a real light bouncing off.
    /// Does a lot of cool math that I copied and pasted from online.
    /// </summary>
    private IEnumerator CalculateGaze()
    {
        LightAnchor.SetActive(true);
        Vector3 sourcePosition = Vector3.zero;

        while (CurrentTarget != null)
        {
            //only recalculates gaze if it needs too!
            //this is efficient and it 
            if (sourcePosition != CurrentTarget.transform.position)
            {
                sourcePosition = CurrentTarget.transform.position;

                //vector between two points
                Vector2 Direction = transform.position - sourcePosition;
                float angle = Vector2.SignedAngle(Vector2.right, Direction) + 90;

                Vector3 TargetRotation = new Vector3(0, 0, angle);
                LightAnchor.transform.rotation = Quaternion.RotateTowards(LightAnchor.transform.rotation, Quaternion.Euler(TargetRotation), RotateSpeed);
            }

            yield return new WaitForSeconds(AngleRecalculationDelay);
        }

        //Cant see anymore, pack it up boys
        DisableLight();
    }

    /// <summary>
    /// ooooo im blinded by the light.
    /// deactivates activator if applicable.
    /// </summary>
    public void BecomeBlind()
    {
        Blinded = true;
        DisableLight();

        CurrentTarget = null;

        if (EyeActivator != null)
            EyeActivator.DeactivationInput();
    }

    /// <summary>
    /// Runs after being blind. Waits for a delay. 
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator BecomeUnblind(float Seconds)
    {
        yield return new WaitForSeconds(Seconds);

        Blinded = false;

        //StartCoroutine(SearchForPlayers());

        blindCoroutine = null;
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
        active = false;
    }

    /// <summary>
    /// gets blinded by flashes
    /// </summary>
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
            blindCoroutine = StartCoroutine(BecomeUnblind(BlindLength));
        }

        if (tag.Equals("Attack"))
        {
            if (blindCoroutine != null)
            {
                StopCoroutine(blindCoroutine);
                blindCoroutine = null;
            }

            BecomeBlind();
            blindCoroutine = StartCoroutine(BecomeUnblind(0.25f));
        }
    }

}
