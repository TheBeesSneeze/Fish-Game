using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/*******************************************************************************
// File Name :         CrateBehaviour.cs
// Author(s) :         Sky Beal, Toby Schamberger
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

    public override void Start()
    {
        base.Start();
    }
    /// <summary>
    /// if sword swing hits it BOOM dead
    /// </summary>
    /// <param name="collision"></param>
    /// 
    /* Zach Note
    * okay so I tend to try and avoid the try / catch scenario. General code guidelines say you should only use it 
    * when you expect the catch to hit sometimes and need to do something in that case. It's a bit more resource intensive and 
    * just kinda industry standard to use if/else checks instead here. Not a bad thing at all, but i'd avoid in the future. Excellent job 
    * using something like this though as your building your toolset skills. 
    */
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

    private IEnumerator WaitToSpawnEnemy()
    {
        yield return new WaitForSeconds(0.05f);
        surpriseOutside.SetActive(true);
    }
}
