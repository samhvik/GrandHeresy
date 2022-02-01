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

    void Awake()
    {
        controls = new PlayerControls();

        controls.UI.Start.performed += ctx => Pause();
    }

    void OnEnable()
    {
        controls.UI.Enable();
    }

    void OnDisable()
    {
        controls.UI.Disable();
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
