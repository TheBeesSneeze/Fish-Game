using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/*******************************************************************************
// File Name :         GlobbingtonAttackController.cs
// Author(s) :         Sky Beal
// Creation Date :     3/28/2023
//
// Brief Description : Code for Globbington swinging his sword! Also accounts 
//                     for his movement.
*****************************************************************************/

public class GlobbingtonAttackController : PlayerController
{
    [Header("Settings")]
    public float AttackLength;

    [Header("Unity Stuff")]
    public GameObject Sword;
    public InputAction Strike;
    public Transform RotatePoint;
    private Rigidbody2D myRB;

    /// <summary>
    /// steals start from playercontoller and adapts it for globbington
    /// </summary>
    public override void Start()
    {
        base.Start();

        this.gameObject.name = "Globbington";
        myRB = GetComponent<Rigidbody2D>();
        MyPlayerInput.actions.Enable();
        Strike = MyPlayerInput.actions.FindAction("Strike");

        Strike.started += Strike_started;
    }

    /// <summary>
    /// starts strike action (sword swing)
    /// </summary>
    /// <param name="obj"></param>
    private void Strike_started(InputAction.CallbackContext obj)
    {
        Sword.SetActive(true);
        Invoke("StopAttack", AttackLength);
    }

    /// <summary>
    /// stops attack - turns sword inactive
    /// </summary>
    private void StopAttack()
    {
        Sword.SetActive(false);
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
            myRB.velocity = Move.ReadValue<Vector2>() * Speed;
            RotatePlayer();
            yield return null;
        }
    }
}