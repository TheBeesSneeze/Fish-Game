/*******************************************************************************
// File Name :         PlayerController.cs
// Author :            Toby Schamberger, Jay Embry, Sky Beal
// Creation Date :     whenever we made it in class lol
//
// Brief Description : This code is to be shared between Gorp and Globbington!
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerController : CharacterBehavior
{
    [Header("Settings:")]
    public CharacterType CharacterData;
    public int PlayerNumber;

    private GameManager gameManager;

    [Header("Controller stuff:")]

    public bool Rumble;
    public bool IgnoreMove;
    public Gamepad MyGamepad;

    public PlayerInput MyPlayerInput;
    public InputAction Move;
    public InputAction Dash;
    public InputAction Select;
    public InputAction Swap;

    public  bool ReadMove;
    public float DashForce;

    //public float dashForce = 40;

    //public int PlayerNumber;

    //public Gamepad playerGamepad;


    /// <summary>
    /// Sets health and binds controls
    /// </summary>
    public override void Start()
    {
        base.Start();
        SetAttributes();

        MyRB = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindObjectOfType<GameManager>();

        MyPlayerInput.actions.Enable();
        Move = MyPlayerInput.actions.FindAction("Move");
        Dash = MyPlayerInput.actions.FindAction("Dash");
        Select = MyPlayerInput.actions.FindAction("Select");
        Swap = MyPlayerInput.actions.FindAction("Swap");

        //I believe this is adding the functions to the buttons...
        Move.started += Move_started;
        Move.canceled += Move_canceled;

        Dash.started += DashInput;

        Swap.started += SwapInput;

        MyGamepad = MyPlayerInput.GetDevice<Gamepad>();
        if (MyGamepad == null) Rumble = false;
    }

    /*
    private void Dash_canceled(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }
    */

    /// <summary>
    /// Sets variables to those in CharacterData
    /// </summary>
    public override void SetAttributes()
    {
        Health = CharacterData.Health;
        Speed = CharacterData.Speed;
        TakeKnockback = CharacterData.TakeKnockback;
        ImmuneToElectricity = CharacterData.ImmuneToElectricity;
        StunLength = CharacterData.StunDuration;
        KnockbackForce = CharacterData.KnockBackForce;
        DashForce = CharacterData.DashForce;
    }

    /// <summary>
    /// Moves the player when ReadMove is true
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator MovePlayer()
    {
        while (ReadMove)
        {
            if(!IgnoreMove)
                MyRB.velocity = Move.ReadValue<Vector2>() * Speed;

            yield return null;
        }
    }

    /// <summary>
    /// When the player dies, the scene is reset.
    /// </summary>
    public override void Die()
    {
        ResetScene();
    }

    public override void Respawn()
    {
        base.Respawn();
        SetAttributes();
    }

    /// <summary>
    /// When player dies, set both players to their starting positions.
    /// Also sends enemies to their start.
    /// </summary>
    public void ResetScene()
    {
        //Resets players:
        PlayerController gorp = GameObject.Find("Gorp")   .GetComponent<PlayerController>();

        PlayerController globbington = null;
        try { globbington = GameObject.Find("Globbington").GetComponent<PlayerController>(); }
        catch { Debug.Log("no globbington!"); }

        gorp.Respawn();

        if(globbington!=null)
            globbington.Respawn();

        //Reset enemies:
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies) 
        { 
            enemy.GetComponent<EnemyBehavior>().Respawn();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("Enemy"))
        {
            TakeDamage(1, collision.transform.position);
        }
        if(tag.Equals("Player"))
        {
            KnockBack(this.gameObject, collision.transform.position);
        }
    }

    private void Move_started(InputAction.CallbackContext obj)
    {
        ReadMove = true;
        StartCoroutine(MovePlayer());
    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        ReadMove = false;
        //MyRB.velocity = Vector3.zero; 
        //Slidey-ness can be configured in linear drag of the rigidbody2d
    }

    /// <summary>
    /// Dashes in the direction that the player is moving in. Yippee!!
    /// </summary>
    /// <param name="obj"></param>
    public void DashInput(InputAction.CallbackContext obj)
    {

        IgnoreMove = true;
        MyRB.AddForce(Move.ReadValue<Vector2>() * DashForce, ForceMode2D.Impulse);

        StartCoroutine(NoMovementRoutine(0.2f));
    }

    public void SwapInput(InputAction.CallbackContext obj)
    {

        gameManager.SwapPlayers();
    }
    public override void BeStunned()
    {
        base.BeStunned();
        MyPlayerInput.actions.Disable();
    }
    public override void BeUnStunned()
    {
        base.BeUnStunned();
        MyPlayerInput.actions.Enable();
    }
    public IEnumerator NoMovementRoutine(float Seconds)
    {

        yield return new WaitForSeconds(Seconds);
        IgnoreMove = false;

    }

    public override void KnockBack(GameObject target, Vector3 damageSourcePosition)
    {
        base.KnockBack(target, damageSourcePosition);
        IgnoreMove = true;
        Debug.Log("knocking back");

        StartCoroutine(NoMovementRoutine(0.1f));
    }

    private void OnDestroy()
    {

        Move.started -= Move_started;
        Move.canceled -= Move_canceled;

        Dash.started -= DashInput;

    }
}
