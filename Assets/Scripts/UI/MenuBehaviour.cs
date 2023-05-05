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

    public void Start()
    {

        Application.targetFrameRate = 60;

    }
    public void StartGame()
    {

        SceneManager.LoadScene(1);

    }

    public void ResetGame()
    {

        SceneManager.LoadScene(0);

    }

    public void QuitGame()
    {

        Application.Quit();

    }

}
