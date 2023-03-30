using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlobbingtonAttackController : MonoBehaviour
{
    [Header("Fucking Around And Finding Out")]
    public GameObject Sword;
    public PlayerInput MyPlayerInput;

    public InputAction Strike;

    private Rigidbody2D myRB;
    public float AttackLength;

    //all the controller shit
    void Start()
    {
        this.gameObject.name = "Globbington";
        myRB = GetComponent<Rigidbody2D>();
        MyPlayerInput.actions.Enable();
        Strike = MyPlayerInput.actions.FindAction("Strike");

        Strike.started += Strike_started;
    }

    //more controller shit
    private void Strike_started(InputAction.CallbackContext obj)
    {
        GlobAttack();
        Invoke("StopAttack", AttackLength);
    }

    //attack method !!
    private void GlobAttack()
    {
        Sword.SetActive(true);
    }

    //knock it off
    private void StopAttack()
    {
        Sword.SetActive(false);
    }

    //no more !!
    private void OnDisable()
    {
        Strike.started -= Strike_started;
    }
}
