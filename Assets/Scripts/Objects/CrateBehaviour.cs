using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/*******************************************************************************
// File Name :         CrateBehaviour.cs
// Author(s) :         Sky Beal, Toby Schamberger, Jay Embry
// Creation Date :     4/5/2023
//
// Brief Description : Code for crate break !! Crates extend CharacterBehavior because they need to respawn
// Prefabs can be placed inside crates!
*****************************************************************************/

public class CrateBehaviour : ObjectType
{
    [Header("Crate:")]

    [Tooltip("PREFAB to instantiate when crate is destroyed")]
    public GameObject SurpriseInside;
    private GameObject surpriseOutside;
    public AudioClip BreakCrate;
    public AudioSource CrateSource;

    /// <summary>
    /// wow do the start stuff from objecttype
    /// </summary>
    public override void Start()
    {
        base.Start();
    }
    /// <summary>
    /// if sword swing hits it BOOM dead
    /// </summary>
    /// <param name="collision"></param>
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("Attack"))
        {
            if (SurpriseInside != null)
            {
                surpriseOutside = Instantiate(SurpriseInside, DefaultPosition, Quaternion.identity);

                Debug.Log(surpriseOutside.name);
            }

            if (CrateSource != null)
            {

                CrateSource.Play();

            }

            try { surpriseOutside.GetComponent<CharacterBehavior>().Health++; } catch { }
            try { surpriseOutside.GetComponent<EnemyBehavior>().DespawnOnStart=false; } catch { }
            StartCoroutine(WaitToSpawnEnemy());

            Despawn();
        }
    }

    /// <summary>
    /// Destroys the item inside the crate
    /// </summary>
    public override void Respawn()
    {
        DespawnItem();
        surpriseOutside = null;

        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// If the item from inside the crate has been spawned,
    /// despawns the item.
    /// </summary>
    private void DespawnItem()
    {
        if (surpriseOutside != null)
            Destroy(surpriseOutside);

        surpriseOutside = null;
    }

    /// <summary>
    /// surprise !!
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitToSpawnEnemy()
    {
        yield return new WaitForSeconds(0.05f);
        surpriseOutside.SetActive(true);
    }
}
