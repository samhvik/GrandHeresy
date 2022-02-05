/// LevelSelectMenu.cs
/// Created by: Justin Quan
/// A basic main menu program that allows the buttons "Play" and "Quit"
/// to function as they are supposed to.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class LevelSelectMenu : MonoBehaviour
{
    public GameObject currentScreen;

    public GameObject nextScreen;

    public GameObject prevScreen;
    public GameObject prevButton;

    PlayerControls controls;

    // sets up the controls first thing for this scene
    void Awake()
    {
        controls = new PlayerControls();

        // whenever the controller presses the Submit or Cancel buttons
        controls.UI.Submit.performed += ctx => onSelect();
        controls.UI.Cancel.performed += ctx => onCancel();
    }

    // whenever the players press submit
    void onSelect()
    {
        // sets the current screen to false
        currentScreen.SetActive(false);
        // activates the next screen
        nextScreen.SetActive(true);
    }

    // whenever the players press cancel
    void onCancel()
    {
        // sets the current screen to false
        currentScreen.SetActive(false);
        // activates the next screen
        prevScreen.SetActive(true);

        EventSystem.current.SetSelectedGameObject(prevButton);
    }

    void OnEnable()
    {
        controls.UI.Enable();
    }

    void OnDisable()
    {
        controls.UI.Disable();
    }

    // closes the game application
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
