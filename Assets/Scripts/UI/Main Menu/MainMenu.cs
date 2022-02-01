/// MainMenu.cs
/// Created by: Justin Quan
/// A basic main menu program that allows the buttons "Play" and "Quit"
/// to function as they are supposed to.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject currentScreen;
    public GameObject nextScreen;

    public GameObject prevScreen;
    public GameObject prevButton;

    DefaultInputActions controls;

    void Awake()
    {
        controls = new DefaultInputActions();

        controls.UI.Submit.performed += ctx => onSelect();
        controls.UI.Cancel.performed += ctx => onCancel();
    }

    // loads the Main scene
    public void PlayGame()
    {
        SceneManager.LoadScene("Main");     // CHANGE "Main" INTO A SCENE VARIABLE IN THE FUTURE
    }

    void onSelect()
    {
        currentScreen.SetActive(false);
        nextScreen.SetActive(true);
    }

    void onCancel()
    {
        currentScreen.SetActive(false);
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
