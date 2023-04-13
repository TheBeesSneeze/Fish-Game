/*******************************************************************************
// File Name :         Alpha2.cs
// Author(s) :         Jay Embry
// Creation Date :     4/13/2023
//
// Brief Description : Cannot emphasize enough that this will be deleted
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alpha2 : MonoBehaviour
{


    public GameObject Instructions;
    public GameObject StartText;
    public GameObject Instructions2;

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            Instructions.SetActive(true);
            StartText.SetActive(true);
            Instructions2.SetActive(false);

        }

    }
}
