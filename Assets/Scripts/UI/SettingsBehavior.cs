/*******************************************************************************
// File Name :         SettingsBehavior.cs
// Author(s) :         Toby Schamberger
// Creation Date :     5/5/2023
//
// Brief Description : Gives the players options to toggle rumble, sound, and music
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsBehavior : MonoBehaviour
{
    [Header("Debug")]
    public bool Rumble = true;
    public bool Music = true;
    public bool SFX = true;

    [Header("Buttons")]
    public Button RumbleButton;
    public Button MusicButton;
    public Button SFXButton;

    public TextMeshProUGUI rumbleText;
    public TextMeshProUGUI musicText;
    public TextMeshProUGUI sfxText;

    public PauseBehaviour PauseScreen;
    private GameManager gameManager;

    /// <summary>
    /// Gets buttons texts
    /// </summary>
    public void Start()
    {
        
        rumbleText = RumbleButton.GetComponentInChildren<TextMeshProUGUI>();
        musicText = MusicButton.GetComponentInChildren<TextMeshProUGUI>();
        sfxText = SFXButton.GetComponentInChildren<TextMeshProUGUI>();
        
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }



    /// <summary>
    /// Toggles rumble. Updates Playercontrollers and GameManager.
    /// </summary>
    public void ToggleRumble()
    {
        //prioritizes what GameManager thinks
        Rumble = ! gameManager.Rumble;
        gameManager.Rumble = !gameManager.Rumble;

        if (PauseScreen.Gorp != null)
        {
            if(PauseScreen.Gorp.MyGamepad == null)
                PauseScreen.Gorp.Rumble = false;
            else
                PauseScreen.Gorp.Rumble = Rumble;
        }

        if (PauseScreen.Glob != null)
        {
            if (PauseScreen.Glob.MyGamepad == null)
                PauseScreen.Glob.Rumble = false;
            else
                PauseScreen.Glob.Rumble = Rumble;
        }

        if (Rumble)
            rumbleText.text = "Rumble ON";
        else
            rumbleText.text = "Rumble OFF";

    }

    /// <summary>
    /// Toggles music, tells gameManager about it.
    /// </summary>
    public void ToggleMusic()
    {
        Music = !gameManager.Music;
        gameManager.Music = !gameManager.Music;

        if (Music)
            musicText.text = "Music ON";
        else
            musicText.text = "Music OFF";
    }

    public void ToggleSFX()
    {
        SFX = !gameManager.SFX;
        gameManager.SFX = !gameManager.SFX;

        if (SFX)
        {
            sfxText.text = "SFX ON";
        }
        else
        {
            sfxText.text = "SFX OFF";
        }
    }

    public void BackButton()
    {
        PauseScreen.PauseScreen.SetActive(true);
        PauseScreen.MenuNavigator.SetSelectedGameObject(PauseScreen.TopButton);
<<<<<<< HEAD
        PauseScreen.SettingsGameObject.SetActive(false);
=======
        gameObject.SetActive(false);
>>>>>>> main
    }
}
