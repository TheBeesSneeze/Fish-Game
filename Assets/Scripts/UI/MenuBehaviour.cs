/*******************************************************************************
// File Name :         MenuBehaviour.cs
// Author(s) :         Jay Embry
// Creation Date :     4/9/2023
//
// Brief Description :Code that manages the pause and menu function of the game
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuBehaviour : MonoBehaviour
{
    /// <summary>
    /// target frame rate !
    /// </summary>
    public void Start()
    {
        Application.targetFrameRate = 60;
    }

    /// <summary>
    /// load first scene
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// resets game
    /// </summary>
    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// quits game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
