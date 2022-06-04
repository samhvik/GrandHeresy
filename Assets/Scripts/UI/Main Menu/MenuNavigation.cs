using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MenuNavigation : MonoBehaviour
{
    PlayerControls controls;

    public GameObject currentScreen;
    public GameObject prevScreen;
    public GameObject levelButton;
    public Animator animator;

    void Awake()
    {
        controls = new PlayerControls();

        controls.UI.Start.performed += ctx => StartGame();
        controls.UI.Cancel.performed += ctx => GoBack();
    }

    // loads the Main scene
    public void StartGame()
    {
        MainMenuMusic script = GameObject.Find("MainMenuCanvas").GetComponent<MainMenuMusic>();
        script.music_event.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        Destroy(script.gameObject);
        
        SceneManager.LoadScene("Main");
        Fade.ToggleLevelFade(animator);
    }

    public void GoBack()
    {
        EventSystem.current.SetSelectedGameObject(levelButton);

        currentScreen.SetActive(false);

        prevScreen.SetActive(true);
    }
}