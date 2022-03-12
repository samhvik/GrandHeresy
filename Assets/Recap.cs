using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Recap : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    // closes the game application
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
