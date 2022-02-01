/// MainMenu.cs
/// Created by: Justin Quan
/// A basic pause menu program that controls pausing the game.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    
    public GameObject pauseMenuUI;
    public GameObject firstButton;

    PlayerControls controls;

    // sets up the controls first thing for this scene
    void Awake()
    {
        controls = new PlayerControls();

        // Whenever the game detects the start button being pressed
        controls.UI.Start.performed += ctx => Pause();
    }

    // Enables the controls for UI
    void OnEnable()
    {
        controls.UI.Enable();
    }

    // Disables the controls for UI when not needed
    void OnDisable()
    {
        controls.UI.Disable();
    }

    // removes pause menu ui, resumes game time, and sets GameIsPaused to false
    public void Resume()
    {
        // deactivates the pause menu and resumes time scale for game
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    // enables pause menu ui, pauses game time, and sets GameIsPaused to true
    void Pause()
    {
        // activates pause menu and stops time for the game
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        // sets resume as the first selected game object
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

    // loads the MainMenu scene
    public void LoadMenu()
    {
        SceneManager.LoadScene("Justin's Main Menu"); // CHANGE "Menu" INTO A SCENE VARIABLE IN THE FUTURE
    }

    // closes the game application
    public void QuitGame()
    {
        Application.Quit();
    }
}
