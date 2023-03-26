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

public class PlayerController : MonoBehaviour
{
    public PlayerInput MyPlayerInput;

    public InputAction Move;
    public InputAction Jump;

    public  bool ReadMove;
    public  float Speed;
    private Rigidbody2D myRb;
    

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        MyPlayerInput.actions.Enable();
        Move = MyPlayerInput.actions.FindAction("Move");
        Jump = MyPlayerInput.actions.FindAction("Jump");

        //I believe this is adding the functions to the buttons...
        Move.started += Move_started;
        Move.canceled += Move_canceled;
    }

    private void Move_started(InputAction.CallbackContext obj)
    {
        ReadMove = true;
    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        ReadMove = false;
        myRb.velocity = Vector3.zero; //Replace this line and add the slidey function :D
    }

    /// <summary>
    /// THE FORBIDDEN FUNCTION! HUZZAH
    /// </summary>
    void Update()
    {
        if(ReadMove)
        myRb.velocity = Move.ReadValue<Vector2>() * Speed;
    }

    private void OnDisable()
    {
        Move.started -= Move_started;
        Move.canceled -= Move_canceled;
    }
}
