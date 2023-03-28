/*******************************************************************************
// File Name :         PlayerController.cs
// Author :            Toby Schamberger
// Creation Date :     whenever we made it in class lol
//
// Brief Description : This code is to be shared between Gorp and Globbington!
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : CharacterBehavior
{
    [Header("Debug")]
    public bool insideLight;
    public float Speed;

    [Header("Controller stuff:")]

    public PlayerInput MyPlayerInput;

    public InputAction Move;
    public InputAction Jump;

    public  bool ReadMove;
    
    private Rigidbody2D myRb;
    

    // Start is called before the first frame update
    void Start()
    {
        Health = DefaultHealth;
        myRb = GetComponent<Rigidbody2D>();
        MyPlayerInput.actions.Enable();
        Move = MyPlayerInput.actions.FindAction("Move");
        Jump = MyPlayerInput.actions.FindAction("Jump");

        //I believe this is adding the functions to the buttons...
        Move.started += Move_started;
        Move.canceled += Move_canceled;
    }

    private IEnumerator MovePlayer()
    {
        while (ReadMove)
        {
            myRb.velocity = Move.ReadValue<Vector2>() * Speed;
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("Enemy"))
        {
            TakeDamage(1, true, collision.transform.position);
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
        myRb.velocity = Vector3.zero; //Replace this line and add the slidey function :D
    }

    private void OnDisable()
    {
        Move.started -= Move_started;
        Move.canceled -= Move_canceled;
    }
}
