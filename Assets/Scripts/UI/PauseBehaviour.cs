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
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseBehaviour : MonoBehaviour
{
    //public PlayerInput PlayerInputMenu;
    //public InputAction Pause;
    public bool Paused;
    public GameObject PauseScreen;
    public GameObject SettingsGameObject;
    public SettingsBehavior SettingsScreen;
    [Tooltip("The first button that is selected")]
    public GameObject TopButton;

    public EventSystem MenuNavigator;
    private GameManager gameManager;

    public PlayerController Gorp;
    public PlayerController Glob;

    /// <summary>
    /// starts paused, finds players
    /// </summary>
    void Start()
    {
        Paused = false;

        StartCoroutine(FindPlayers());

        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    /// <summary>
    /// coroutine that finds gorp, then globbington
    /// </summary>
    private IEnumerator FindPlayers()
    {
        while (Gorp == null)
        {
            try { Gorp = GameObject.Find("Gorp").GetComponent<PlayerController>(); } catch { }
            yield return new WaitForSeconds(0.25f);
        }

        Gorp.Pause.started += PauseFunction;

        while(Glob == null)
        {
            try { Glob = GameObject.Find("Globbington").GetComponent<PlayerController>(); } catch { }
            yield return new WaitForSeconds(0.25f);
        }

        Glob.Pause.started += PauseFunction;
    }

    /// <summary>
    /// toggles pausing
    /// </summary>
    public void PauseFunction(InputAction.CallbackContext obj)
    {
        Paused = !Paused;

        if ( Paused )
        {
            PauseGame();
        }
        if ( ! Paused )
        {
            ResumeGame();
        }
    }

    /// <summary>
    /// opens pause menu
    /// </summary>
    private void PauseGame()
    {
        PauseScreen.SetActive(true);
        MenuNavigator.SetSelectedGameObject(TopButton);

        Time.timeScale = 0;

        Gorp.DashActive = false;
        if(Glob != null)
            Glob.DashActive = false;
    }

    /// <summary>
    /// closes pause menu
    /// </summary>
    private void ResumeGame()
    {
        PauseScreen.SetActive(false);

        Time.timeScale = 1;

        if(Gorp!= null)
            Gorp.DashActive = true;
        if (Glob != null)
            Glob.DashActive = true;

    }

    /// <summary>
    /// opens settings menu
    /// </summary>
    public void SettingsButton()
    {
        PauseScreen.SetActive(false);
        SettingsGameObject.SetActive(true);

        MenuNavigator.SetSelectedGameObject(SettingsScreen.RumbleButton.gameObject);
    }

    /// <summary>
    /// Doesnt work
    /// </summary>
    public void ReturnToMenu()
    {

        SceneManager.LoadScene(0);

    }

    /// <summary>
    /// Closes game
    /// </summary>
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
