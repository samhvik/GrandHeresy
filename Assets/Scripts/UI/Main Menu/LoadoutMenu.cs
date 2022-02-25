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

    public GameObject currentScreen;
    public GameObject prevScreen;

    void Awake()
    {
        controls = new PlayerControls();

        controls.UI.Start.performed += ctx => PlayGame();
        controls.UI.Cancel.performed += ctx => GoBack();
    }

    // loads the Main scene
    public void PlayGame()
    {
        SceneManager.LoadScene(GameValues.level);
    }

    public void GoBack()
    {
        currentScreen.SetActive(false);

        prevScreen.SetActive(true);
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
