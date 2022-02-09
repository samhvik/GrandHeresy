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
    public GameObject pauseMenuUI;
    public GameObject firstButton;
    public GameObject player;

    PlayerMovement movement;
    PlayerShooting shooting;

    PlayerControls controls;

    // sets up the controls first thing for this scene
    void Awake()
    {
        // sets the variables for moving and shooting
        movement = player.GetComponent<PlayerMovement>();
        shooting = player.GetComponent<PlayerShooting>();

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

        // enables player to start moving and shooting again
        movement.OnEnable();
        shooting.OnEnable();
    }

    // enables pause menu ui, pauses game time, and sets GameIsPaused to true
    void Pause()
    {
        // disables player's ability to move and shoot
        movement.OnDisable();
        shooting.OnDisable();

        // activates pause menu and stops time for the game
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

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
