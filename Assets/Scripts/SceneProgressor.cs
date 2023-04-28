/*******************************************************************************
// File Name :         SceneProgressor.cs
// Author(s) :         Toby Schamberger
// Creation Date :     4/27/2023
//
// Brief Description : When player enters a trigger, a configurable scene is 
// loaded.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneProgressor : MonoBehaviour
{
    public string SceneToLoadName;
    private Animator animator;

    /// <summary>
    /// Gets animator and opens door
    /// </summary>
    void Start()
    {
        //Open = true;
        animator = GetComponent<Animator>();
        animator.SetBool("Open", true);
    }

    /// <summary>
    /// Starts coroutine that loads scene animation
    /// </summary>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;
        if(tag.Equals("Player"))
        {
            StartCoroutine(CloseDoorAnimation());
        }
    }

    /// <summary>
    /// Silly animation that delays scene load
    /// </summary>
    private IEnumerator CloseDoorAnimation()
    {
        animator.SetBool("Open", false);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneToLoadName); //converting scene => name to avoid typos. no idea if i can just load a scene from the variable lol
    }
}
