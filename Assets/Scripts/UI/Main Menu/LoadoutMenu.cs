using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class LoadoutMenu : MonoBehaviour
{
    PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();

        controls.UI.Submit.performed += ctx => onJoining();
        controls.UI.Start.performed += ctx => PlayGame();
    }

    void onJoining()
    {

    }

    // loads the Main scene
    public void PlayGame()
    {
        SceneManager.LoadScene("Main");     // CHANGE "Main" INTO A SCENE VARIABLE IN THE FUTURE
    }

    void OnEnable()
    {
        controls.UI.Enable();
    }

    void OnDisable()
    {
        controls.UI.Disable();
    }
}
