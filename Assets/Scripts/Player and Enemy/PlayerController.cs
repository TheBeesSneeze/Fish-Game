/*******************************************************************************
// File Name :         PlayerController.cs
// Author :            Toby Schamberger, Jay Embry, Sky Beal
// Creation Date :     whenever we made it in class lol
//
// Brief Description : This code is to be shared between Gorp and Globbington!
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using TMPro;

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
    private HealthDisplay healthDisplay;

    [Header("Controller stuff:")]

    public bool Rumble;
    public bool IgnoreMove;
    public Gamepad MyGamepad;

    public PlayerInput MyPlayerInput;
    public InputAction Move;
    public InputAction Dash;
    public InputAction Select;
    public InputAction Swap;
    public InputAction Debug;
    public InputAction Pause;

    public  bool ReadMove;
    public float DashForce;
    public AudioClip Scream;
    public AudioSource MyAudioSource;

    [Tooltip("Debug, if player can dash")]
    public bool DashActive = true;

    /// <summary>
    /// Sets health and binds controls
    /// </summary>
    public override void Start()
    {
        base.Start();
        SetAttributes();

        MyRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager   = GameObject.FindObjectOfType<GameManager>();
        healthDisplay = GameObject.FindObjectOfType<HealthDisplay>();

        MyPlayerInput.actions.Enable();
        Move = MyPlayerInput.actions.FindAction("Move");
        Dash = MyPlayerInput.actions.FindAction("Dash");
        Select = MyPlayerInput.actions.FindAction("Select");
        Swap = MyPlayerInput.actions.FindAction("Swap");
        Debug = MyPlayerInput.actions.FindAction("Dev Mode");
        Pause = MyPlayerInput.actions.FindAction("Pause");

        //I believe this is adding the functions to the buttons...
        Move.started += Move_started;
        Move.canceled += Move_canceled;

        Dash.started += DashInput;

        Swap.started += SwapInput;

        Debug.started += DevMode;

        MyGamepad = MyPlayerInput.GetDevice<Gamepad>();
        if (MyGamepad == null) Rumble = false;

        if(gameManager.CurrentRoom != null)
            gameManager.CurrentRoom.EnterRoom();
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
            healthDisplay.UpdateHealth();

            if (!died)
                StartInvincibleFrames();

            return died;
        }
        else
        {
            KnockBack(this.gameObject, damageSourcePosition);
            return false;
        }
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
            healthDisplay.UpdateHealth();

            if (!died)
                StartInvincibleFrames();

            return died;
        }
        else
        {
            return false;
        }
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
        spriteRenderer.color = new Color(baseColor.r, baseColor.g, baseColor.b, 1);
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
    public override void Despawn()
    {
        ResetScene();
    }

    public override void Respawn()
    {
        invincible = false;
        healthDisplay.UpdateHealth();
        SetAttributes();
        base.Respawn();
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

        //test
        if (Rumble)
        {
            MyGamepad.SetMotorSpeeds(0f, 0f);
        }

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
        catch { /*Debug.Log("no globbington!");*/ }

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

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag.Equals("Light"))
        {
            LayersOfLight++;
        }
        if (tag.Equals("Electricity") && !ImmuneToElectricity)
        {
            GetElectrified();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("Enemy"))
        {
            TakeDamage(1, collision.transform.position);
            if(MyAudioSource!=null)
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

        //test
        if (DashActive)
        {

            IgnoreMove = true;
            MyRB.AddForce(Move.ReadValue<Vector2>() * DashForce, ForceMode2D.Impulse);

            //test
            if (Rumble)
            {
                MyGamepad.SetMotorSpeeds(0.3f, 0.3f);
            }

            StartCoroutine(NoMovementRoutine(0.2f));

        }
       
    }

    public void SwapInput(InputAction.CallbackContext obj)
    {

        gameManager.SwapPlayers();
    }

    /// <summary>
    /// Disables the collider
    /// </summary>
    public void DevMode(InputAction.CallbackContext obj)
    {
        Collider2D c = gameObject.GetComponent<Collider2D>();
        c.enabled = !c.enabled;

        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.GetComponent<CameraController>().MoveCamera = false;

        if (c.enabled)
        {
            MyRB.bodyType = RigidbodyType2D.Dynamic;
            camera.transform.parent = null;
        }
        else
        {
            MyRB.bodyType = RigidbodyType2D.Kinematic;
            camera.transform.parent = this.gameObject.transform;
            camera.transform.localPosition = new Vector3(0, 0, camera.transform.position.z);
        }
    }

    public virtual void OnDestroy()
    {

        Move.started -= Move_started;
        Move.canceled -= Move_canceled;

        Dash.started -= DashInput;

        Swap.started -= SwapInput;

        Debug.started -= DevMode;

        MyPlayerInput.actions.Disable();
    }
}
