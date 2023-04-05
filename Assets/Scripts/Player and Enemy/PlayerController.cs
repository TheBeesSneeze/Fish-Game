/*******************************************************************************
// File Name :         PlayerController.cs
// Author :            Toby Schamberger
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
    public int PlayerNumber;

    private Rigidbody2D myRb;
    public GameManager gameManager;

    [Header("Controller stuff:")]

    public bool Rumble;
    public Gamepad MyGamepad;

    public PlayerInput MyPlayerInput;
    public InputAction Move;

    public  bool ReadMove;

    //public int PlayerNumber;

    //public Gamepad playerGamepad;


    /// <summary>
    /// Sets health and binds controls
    /// </summary>
    public virtual void Start()
    {
        Health = DefaultHealth;

        myRb = GetComponent<Rigidbody2D>();
        myRb.angularDrag = Weight; // felt silly. might delete later

        MyPlayerInput.actions.Enable();
        Move = MyPlayerInput.actions.FindAction("Move");

        //I believe this is adding the functions to the buttons...
        Move.started += Move_started;
        Move.canceled += Move_canceled;

        MyGamepad = MyPlayerInput.GetDevice<Gamepad>();
        if (MyGamepad == null) Rumble = false;

        
    }
    
    /// <summary>
    /// Moves the player when ReadMove is true
    /// </summary>
    /// <returns></returns>
    public virtual IEnumerator MovePlayer()
    {
        while (ReadMove)
        {
            myRb.velocity = Move.ReadValue<Vector2>() * Speed;
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
    }

    private void Move_started(InputAction.CallbackContext obj)
    {
        ReadMove = true;
        StartCoroutine(MovePlayer());
    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        ReadMove = false;
        //myRb.velocity = Vector3.zero; 
        //Slidey-ness can be configured in linear drag of the rigidbody2d
    }
}
