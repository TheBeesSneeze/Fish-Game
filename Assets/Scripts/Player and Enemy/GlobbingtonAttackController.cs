using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

/*******************************************************************************
// File Name :         GlobbingtonAttackController.cs
// Author(s) :         Sky Beal, Jay Embry, Toby Schamberger
// Creation Date :     3/28/2023
//
// Brief Description : Code for Globbington swinging his sword! Also accounts 
//                     for his movement.
*****************************************************************************/

public class GlobbingtonAttackController : PlayerController
{
    [Header("Settings")]
    public float AttackLength;
    public float StrikeFrames = 30;

    [Header("Unity Stuff")]
    public Collider2D Sword;
    public Transform RotatePoint;

    [Header("Controls")]
    public InputAction Strike;
    private Quaternion swordRotation;
   

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
    }

    /// <summary>
    /// starts strike action (sword swing)
    /// </summary>
    /// <param name="obj"></param>
    private void Strike_started(InputAction.CallbackContext obj)
    {
        //if not already attacking
        if( !Sword.enabled ) 
        {
            Sword.enabled = true;

            StartCoroutine(SwingSword());

            if (Rumble)
            {
                //InputDevice a = MyPlayerInput.devices[0];
                MyGamepad.SetMotorSpeeds(0.15f, 0.25f);
            }
        }
        
      
    }

    private IEnumerator SwingSword()
    {
        Vector3 originalPoint = RotatePoint.rotation.eulerAngles;
        Vector3 startAngle = RotatePoint.rotation.eulerAngles;
        startAngle.z += 45;
        Vector3 endAngle = RotatePoint.rotation.eulerAngles;
        endAngle.z += -45;

        for (int i = 0; i < StrikeFrames; i++)
        {
            Vector3 target = Vector3.Lerp(startAngle, endAngle, i / StrikeFrames);
            RotatePoint.transform.eulerAngles = target;

            yield return new WaitForSeconds(AttackLength / StrikeFrames);
        }

        //RotatePoint.transform.eulerAngles = originalPoint;
        RotatePoint.rotation = swordRotation;

        StopAttack();
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
    private void RotateSword()
    {
        float angle = Mathf.Atan2(Move.ReadValue<Vector2>().y, Move.ReadValue<Vector2>().x) * Mathf.Rad2Deg;
        swordRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (!Sword.enabled)
        {
            RotatePoint.rotation = swordRotation;
        }
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

            Animate();

            RotateSword();
            yield return null;
        }
    }

    public override void GetElectrified()
    {
        Stunned = true;
        BeStunned(1.5f);
    }

    private void OnDestroy()
    {
        Strike.started -= Strike_started;
    }
}