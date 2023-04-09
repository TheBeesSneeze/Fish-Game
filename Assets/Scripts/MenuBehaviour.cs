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

    public void QuitGame()
    {

        Application.Quit();

    }

}
