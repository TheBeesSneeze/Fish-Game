/*******************************************************************************
// File Name :         PlayerController.cs
// Author :            Toby Schamberger
// Creation Date :     whenever we made it in class lol
//
// Brief Description : This was written in class and untouched in the maze
// assignment :D
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

    public  bool        ReadMove;
    private Rigidbody2D myRb;
    public  float       Speed;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        MyPlayerInput.actions.Enable();
        Move = MyPlayerInput.actions.FindAction("Move");
        Jump = MyPlayerInput.actions.FindAction("Jump");

        Move.started += Move_started;
        Move.canceled += Move_canceled;
    }

    // Update is called once per frame
    private void Move_started(InputAction.CallbackContext obj)
    {
        ReadMove = true;
    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        ReadMove = false;
        myRb.velocity = Vector3.zero;
    }

    // THE FORBIDDEN FUNCTION! HUZZAH
    void Update()
    {
        if(ReadMove)
        {
            myRb.velocity = Move.ReadValue<Vector2>() * Speed;
        }
    }

    private void OnDisable()
    {
        Move.started -= Move_started;
        Move.canceled -= Move_canceled;
    }
}
