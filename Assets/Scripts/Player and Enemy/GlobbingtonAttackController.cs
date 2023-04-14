using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
/*******************************************************************************
// File Name :         GlobbingtonAttackController.cs
// Author(s) :         Sky Beal, Jay Embry
// Creation Date :     3/28/2023
//
// Brief Description : Code for Globbington swinging his sword! Also accounts 
//                     for his movement.
*****************************************************************************/

public class GlobbingtonAttackController : PlayerController
{
    [Header("Crate")]
    public float AttackLength;

    [Header("Unity Stuff")]
    public Collider2D Sword;
    public Transform RotatePoint;

    [Header("Controls")]
    public InputAction Strike;

    /// <summary>
    /// steals start from playercontoller and adapts it for globbington
    /// </summary>
    public override void Start()
    {
        base.Start();

        this.gameObject.name = "Globbington";
        //MyRB = GetComponent<Rigidbody2D>();
        MyPlayerInput.actions.Enable();
        Strike = MyPlayerInput.actions.FindAction("Strike");

        Strike.started += Strike_started;
        Dash.started += Strike_started;
    }

    /// <summary>
    /// starts strike action (sword swing)
    /// </summary>
    /// <param name="obj"></param>
    private void Strike_started(InputAction.CallbackContext obj)
    {
        Sword.enabled = true;


        if (Rumble)
        {
            //InputDevice a = MyPlayerInput.devices[0];
            MyGamepad.SetMotorSpeeds(0.15f, 0.25f);
        }
        

        Invoke("StopAttack", AttackLength);
    }

    /// <summary>
    /// stops attack - turns sword inactive
    /// </summary>
    private void StopAttack()
    {
        
        if (Rumble)
            MyGamepad.SetMotorSpeeds(0, 0);
        

        Sword.enabled = false;
    }

    /// <summary>
    /// a bunch of math to rotate the sword around globbington
    /// </summary>
    private void RotatePlayer()
    {
        float angle = Mathf.Atan2(Move.ReadValue<Vector2>().y, Move.ReadValue<Vector2>().x) * Mathf.Rad2Deg;
        RotatePoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    /// <summary>
    /// moves player and calls rotate for sword
    /// </summary>
    /// <returns></returns>
    public override IEnumerator MovePlayer()
    {
        while (ReadMove)
        {
            if (!IgnoreMove)
                MyRB.velocity = Move.ReadValue<Vector2>() * Speed;

            RotatePlayer();
            yield return null;
        }
    }

    private void OnDestroy()
    {

        Strike.started -= Strike_started;

    }
}