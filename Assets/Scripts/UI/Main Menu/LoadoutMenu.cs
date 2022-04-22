using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class LoadoutMenu : MonoBehaviour
{
    PlayerControls controls;

    public GameObject currentScreen;
    public GameObject prevScreen;
    public GameObject levelButton;

    public GameObject P2;
    public GameObject P2Button;
    public GameObject P3;
    public GameObject P3Button;
    public GameObject P4;
    public GameObject P4Button;

    public GameObject playerRoot { get; set; }

    PlayerInput input;

    void Awake()
    {
        controls = new PlayerControls();

        controls.UI.Start.performed += ctx => GoNext();
        controls.UI.Cancel.performed += ctx => GoBack();
        controls.UI.Submit.performed += ctx => HandleJoin(input.playerIndex);
    }

    // loads the Main scene
    public void GoNext()
    {
        SceneManager.LoadScene(GameValues.level);
    }

    public void GoBack()
    {
        EventSystem.current.SetSelectedGameObject(levelButton);

        currentScreen.SetActive(false);

        prevScreen.SetActive(true);
    }

    public void HandleJoin(int index)
    {
        if(index == 1 && !P2.activeSelf)
        {
            P2.SetActive(true);
            EventSystem.current.SetSelectedGameObject(P2Button);
        }
        else if(index == 2 && !P3.activeSelf)
        {
            P3.SetActive(true);
            EventSystem.current.SetSelectedGameObject(P3Button);
        }
        else if(index == 3 && !P4.activeSelf)
        {
            P4.SetActive(true);
            EventSystem.current.SetSelectedGameObject(P4Button);
        }
    }

    public void OnEnable()
    {
        controls.UI.Enable();
    }

    public void OnDisable()
    {
        controls.UI.Disable();
    }
}