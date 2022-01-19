/// MainMenu.cs
/// Created by: Justin Quan
/// A basic main menu program that allows the buttons "Play" and "Quit"
/// to function as they are supposed to.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // loads the Main scene
    public void PlayGame()
    {
        SceneManager.LoadScene("Main");     // CHANGE "Main" INTO A SCENE VARIABLE IN THE FUTURE
    }

    // closes the game application
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
