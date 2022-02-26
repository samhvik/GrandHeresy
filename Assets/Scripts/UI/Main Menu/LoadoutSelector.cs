using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class LoadoutSelector : MonoBehaviour
{
    public GameObject GreyBox;
    public GameObject Menu;
    public GameObject Button;

    LoadoutMenu loadoutMenu;
    DefaultInputActions controls;

    void Awake()
    {
        controls = new DefaultInputActions();

        controls.UI.Submit.performed += ctx => GoBack();
        controls.UI.Cancel.performed += ctx => GoBack();
    }

    public void disableLoadoutMenu()
    {
        loadoutMenu.OnDisable();
    }

    void GoBack()
    {
        GreyBox.SetActive(false);
        Menu.SetActive(false);

        loadoutMenu.OnEnable();
        EventSystem.current.SetSelectedGameObject(Button);
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
