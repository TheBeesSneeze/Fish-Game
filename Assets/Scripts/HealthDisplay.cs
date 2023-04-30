/*******************************************************************************
// File Name :         HealthDisplay.cs
// Author(s) :         Jay Embry, Toby Schamberger
// Creation Date :     4/25/2023
//
// Brief Description : Code that updates the players health.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{

    /* Zach Note
  * I did Unity UI for years in Industry. You guys need to make your canvases Scale with screen size, and pin your 
  * assets appropriately. If you need help come talk to me. Unity UI is batshit. 
  */
    private PlayerController gorp;
    private PlayerController glob;

    public GameObject GorpUI;
    public GameObject GlobUI;

    public TMP_Text GorpDisplay;
    public TMP_Text GlobDisplay;

    void Start()
    {
        StartCoroutine(SearchForGorp());
        StartCoroutine(SearchForGlobbington());
    }

    /// <summary>
    /// Looks for gorp until hes found.
    /// Assigns necessary variables.
    /// </summary>
    private IEnumerator SearchForGorp()
    {
        while(gorp == null)
        {
            GameObject g = GameObject.Find("Gorp");

            if(g != null) 
                gorp = g.GetComponent<PlayerController>();

            yield return new WaitForSeconds(1);
        }

        //GorpDisplay = GameObject.Find("GorpLives").GetComponent<TMP_Text>();
        GorpUI.SetActive(true);
    }

    /// <summary>
    /// Looks for globbington until hes found.
    /// Assigns necessary variables.
    /// </summary>
    private IEnumerator SearchForGlobbington()
    {
        while (glob == null)
        {
            GameObject g = GameObject.Find("Globbington");

            if (g != null)
                glob = g.GetComponent<PlayerController>();

            yield return new WaitForSeconds(1);
        }

        //GorpDisplay = GameObject.Find("GorpLives").GetComponent<TMP_Text>();
        GlobUI.SetActive(true);
    }

    /// <summary>
    /// Updates both players 
    /// </summary>
    public void UpdateHealth()
    {
        if(gorp != null)
            GorpDisplay.text = gorp.Health.ToString();

        if (glob != null)
            GlobDisplay.text = glob.Health.ToString();
    }
}
