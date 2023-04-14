/*******************************************************************************
// File Name :         Throwaway.cs
// Author :            Jay Embry
// Creation Date :     4/13/2023
//
// Brief Description : Does ONE thing ONE time. Will delete later.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwaway : MonoBehaviour
{
    public GameObject GarthInstructions;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            GarthInstructions.SetActive(false);

        }

    }

}
