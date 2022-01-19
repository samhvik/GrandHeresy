/// MainMenu.cs
/// Created by: Justin Quan
/// A basic pause menu program that controls pausing the game.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        // if the key / button is pressed
        if (Input.GetKeyDown(KeyCode.Escape))       // CHANGE Escape TO A GENERALIZED TERM TO INCLUDE CONTROLLER SUPPORT
        {
            // if the game is already paused, resume game
            if (GameIsPaused)
            {
                Resume();
            }

            // else, pause the game
            else
            {
                Pause();
            }
        }
    }

    // removes pause menu ui, resumes game time, and sets GameIsPaused to false
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    // enables pause menu ui, pauses game time, and sets GameIsPaused to true
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    // loads the MainMenu scene
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu"); // CHANGE "Menu" INTO A SCENE VARIABLE IN THE FUTURE
    }

    // closes the game application
    public void QuitGame()
    {
        Application.Quit();
    }
}
