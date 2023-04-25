/*******************************************************************************
// File Name :         PauseBehaviour.cs
// Author(s) :         Jay Embry
// Creation Date :     4/9/2023
//
// Brief Description :Code that manages the menu function of the game
//This maybe could've been combined w/ MenuBehaviour?
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseBehaviour : MonoBehaviour
{
    public PlayerInput PlayerInputMenu;
    public InputAction Pause;
    public bool alreadyPaused;
    public GameObject PauseScreen;

    public PlayerController gorp;
    public PlayerController glob;

    void Start()
    {
        

        PlayerInputMenu.actions.Enable();
        Pause = PlayerInputMenu.actions.FindAction("Pause");

        Pause.started += PauseFunction;
        alreadyPaused = false;

        gorp = GameObject.Find("Gorp").GetComponent<PlayerController>();
        glob = GameObject.Find("Globbington").GetComponent<PlayerController>();

    }

    private void PauseFunction(InputAction.CallbackContext obj)
    {

        if (alreadyPaused == false)
        {

            PauseScreen.SetActive(true);
            alreadyPaused = true;

            Time.timeScale = 0;

            gorp.DashActive = false;
            glob.DashActive = false;

        }
        else if (alreadyPaused == true)
        {

            ResumeGame();

        }

    }

    public void ResumeGame()
    {

        PauseScreen.SetActive(false);
        alreadyPaused = false;

        Time.timeScale = 1;

        gorp.DashActive = true;
        glob.DashActive = true;

    }

    public void ReturnToMenu()
    {

        SceneManager.LoadScene(0);

    }

    public void QuitGame()
    {

        Application.Quit();

    }

    /// <summary>
    /// Simply refrences GameManager and calls its swap function.
    /// Resumes the game after.
    /// </summary>
    public void SwapPlayers()
    {
        GameManager gm = GameManager.FindObjectOfType<GameManager>();
        gm.SwapPlayers();
        ResumeGame();
    }

}
