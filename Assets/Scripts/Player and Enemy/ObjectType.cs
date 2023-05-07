/*******************************************************************************
// File Name :         CharacterBehavior.cs
// Author(s) :         Toby Schamberger
// Creation Date :     4/25/2023
//
// Brief Description : Basic code that is shared between objects, players and enemies.
// Includes information on how to respawn.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectType : MonoBehaviour
{
    [Header("Debug (don't touch in editor)")]

    public Vector3 DefaultPosition;
    public int LayersOfLight;
    public bool DespawnOnStart = true;
    public RoomBehaviour MyRoom;

    /// <summary>
    /// Sets Health to the Default
    /// </summary>
    public virtual void Start()
    {
        DefaultPosition = this.transform.position;
        if(DespawnOnStart) 
        {
            Despawn();
        }
    }

    /// <summary>
    /// Compares the position of every gameobject with tag to point.
    /// </summary>
    /// <returns>closest distance</returns>
    public float GetDistanceOfClosestTag(Vector2 Point, string Tag)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(Tag);

        float min = -1;

        if (objs.Length > 0) // yk, javascript wouldnt make me do this
        {
            min = Vector2.Distance(Point, objs[0].transform.position);

            for (int i = 0; i < objs.Length; i++)
            {
                float dist = Vector2.Distance(Point, objs[i].transform.position);

                if (min > dist)
                    min = dist;
            }
        }
        return min;
    }

    /// <summary>
    /// respawns the object
    /// </summary>
    public virtual void Respawn()
    {
        this.gameObject.SetActive(true);
        this.transform.position = DefaultPosition;
    }
    /// <summary>
    /// despawns the object
    /// </summary>
    public virtual void Despawn()
    {
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// if it's in light
    /// </summary>
    /// <param name="collision"></param>
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag.Equals("Light"))
        {
            LayersOfLight++;
        }
    }

    /// <summary>
    /// less light
    /// </summary>
    /// <param name="collision"></param>
    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag.Equals("Light"))
        {
            LayersOfLight--;
        }
    }

    
}
