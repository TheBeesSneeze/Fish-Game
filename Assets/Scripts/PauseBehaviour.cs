/*******************************************************************************
// File Name :         PauseBehaviour.cs
// Author(s) :         Jay Embry, Toby Schamberger
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
    //public PlayerInput PlayerInputMenu;
    //public InputAction Pause;
    public bool Paused;
    public GameObject PauseScreen;

    public PlayerController gorp;
    public PlayerController glob;

    void Start()
    {
        //PlayerInputMenu.actions.Enable();
        //Pause = PlayerInputMenu.actions.FindAction("Pause");

        //Pause.started += PauseFunction;
        Paused = false;

        StartCoroutine(FindPlayers());
    }

    private IEnumerator FindPlayers()
    {
        while (gorp == null)
        {
            try { gorp = GameObject.Find("Gorp").GetComponent<PlayerController>(); } catch { }
            yield return new WaitForSeconds(0.25f);
        }

        gorp.Pause.started += PauseFunction;

        while(glob == null)
        {
            try { glob = GameObject.Find("Globbington").GetComponent<PlayerController>(); } catch { }
            yield return new WaitForSeconds(0.25f);
        }

        glob.Pause.started += PauseFunction;
    }

    /// <summary>
    /// does the pause thing
    /// </summary>
    public void PauseFunction(InputAction.CallbackContext obj)
    {
        Paused = !Paused;

        if ( ! Paused )
        {
            PauseGame();
        }
        if (Paused )
        {
            ResumeGame();
        }

    }

    private void PauseGame()
    {
        PauseScreen.SetActive(true);

        Time.timeScale = 0;

        gorp.DashActive = false;
        if(glob != null)
            glob.DashActive = false;
    }

    private void ResumeGame()
    {
        PauseScreen.SetActive(false);

        Time.timeScale = 1;

        gorp.DashActive = true;
        if (glob != null)
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
