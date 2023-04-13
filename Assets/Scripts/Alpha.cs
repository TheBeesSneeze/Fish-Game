/*******************************************************************************
// File Name :         Alpha.cs
// Author(s) :         Jay Embry
// Creation Date :     4/13/2023
//
// Brief Description : This is literally just for the alpha lol
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Alpha : MonoBehaviour
{

    public GameObject Instructions;
    public GameObject StartText;
    public GameObject Instructions2;

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            Instructions.SetActive(false);
            StartText.SetActive(false);
            Instructions2.SetActive(true);

        }

    }


}
