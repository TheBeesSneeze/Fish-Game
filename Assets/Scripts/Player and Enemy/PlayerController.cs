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
    public float InvincibleSeconds = 1.5f;
    private bool invincible;
    public Animator PlayerAnimator;
    public CharacterType CharacterData;
    private SpriteRenderer spriteRenderer;
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
    public AudioClip Scream;
    public AudioSource MyAudioSource;

    /// <summary>
    /// Sets health and binds controls
    /// </summary>
    public override void Start()
    {
        base.Start();
        SetAttributes();

        MyRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
    /// Moves the player when ReadMove is true.
    /// </summary>
    public virtual IEnumerator MovePlayer()
    {
        while (ReadMove)
        {
            Animate();
            if(!IgnoreMove)
                MyRB.velocity = Move.ReadValue<Vector2>() * Speed;

            yield return null;
        }
    }

    /// <summary>
    /// Sends X / Y movement info to the animator.
    /// </summary>
    public void Animate()
    {
        if(PlayerAnimator != null)
        {
            PlayerAnimator.SetFloat("X Movement", MyRB.velocity.x);
            PlayerAnimator.SetFloat("Y Movement", MyRB.velocity.y);
        }
    }

    /// <summary>
    /// Override for take damage, doesnt take damage if invincible
    /// </summary>
    /// <returns>True if player died</returns>
    public override bool TakeDamage(float damage, Vector3 damageSourcePosition)
    {
        if (!invincible)
        {
            bool died = base.TakeDamage(damage, damageSourcePosition);

            if (!died)
                StartInvincibleFrames();

            return died;
        }
        else
            return false;
    }

    /// <summary>
    /// Override for take damage, doesnt take damage if invincible
    /// </summary>
    /// <returns>True if player died</returns>
    public override bool TakeDamage(float damage)
    {
        if (!invincible)
        {
            bool died = base.TakeDamage(damage);

            if (!died)
                StartInvincibleFrames();

            return died;
        }
        else
            return false;
    }

    /// <summary>
    /// Starts coroutines that make player invincible
    /// </summary>
    public void StartInvincibleFrames()
    {
        invincible = true;
        StartCoroutine(InvincibleFrames());
        StartCoroutine(EndInvincibleFrames(InvincibleSeconds));
    }

    /// <summary>
    /// Flickers the player between semi transparent and not
    /// </summary>
    /// <returns></returns>
    private IEnumerator InvincibleFrames()
    {
        Color baseColor = spriteRenderer.color;
        bool clear = true;

        while(invincible)
        {
            if (clear)
                spriteRenderer.color = new Color(baseColor.r, baseColor.g, baseColor.b,0.5f);
            else
                spriteRenderer.color = new Color(baseColor.r, baseColor.g, baseColor.b, 1);
                //spriteRenderer.color = baseColor;

            clear = !clear;
            yield return new WaitForSeconds(0.05f);
        }
    }

    /// <summary>
    /// Makes the player vincible (invincible antonym) after Seconds.
    /// </summary>
    /// <returns></returns>
    private IEnumerator EndInvincibleFrames(float Seconds)
    {
        yield return new WaitForSeconds(Seconds);
        invincible = false;
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

    public override void KnockBack(GameObject target, Vector3 damageSourcePosition)
    {
        base.KnockBack(target, damageSourcePosition);
        IgnoreMove = true;

        StartCoroutine(NoMovementRoutine(0.1f));
    }

    public override void KnockBack(GameObject target, Vector3 damageSourcePosition, float Force)
    {
        base.KnockBack(target, damageSourcePosition, Force);
        IgnoreMove = true;

        StartCoroutine(NoMovementRoutine(0.1f));
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
            //Debug.Log(enemy.name);
            enemy.GetComponent<EnemyBehavior>().Respawn();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("Enemy"))
        {
            TakeDamage(1, collision.transform.position);
            MyAudioSource.Play();
        }
        if(tag.Equals("Player"))
        {
            KnockBack(this.gameObject, collision.transform.position,2);
        }
        if(tag.Equals("Eye"))
        {
            KnockBack(this.gameObject,collision.transform.position); 
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
    

  
    private void OnDestroy()
    {

        Move.started -= Move_started;
        Move.canceled -= Move_canceled;

        Dash.started -= DashInput;

    }
}
