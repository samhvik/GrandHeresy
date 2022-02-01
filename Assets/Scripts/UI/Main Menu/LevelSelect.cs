using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class LevelSelect : MonoBehaviour
{
    public GameObject currentLevel;

    public GameObject nextLevel;
    public GameObject nextImage;

    public GameObject prevLevel;
    public GameObject prevImage;

    DefaultInputActions controls;

    void Awake()
    {
        controls = new DefaultInputActions();

        controls.UI.Navigate.performed += ctx => selectLevel(ctx.ReadValue<Vector2>());
    }

    void selectLevel(Vector2 nav)
    {
        if(nav.x > 0)
        {
            currentLevel.SetActive(false);
            nextLevel.SetActive(true);

            EventSystem.current.SetSelectedGameObject(nextImage);
        }

        else if(nav.x < 0)
        {
            currentLevel.SetActive(false);
            prevLevel.SetActive(true);

            EventSystem.current.SetSelectedGameObject(prevImage);
        }
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
