/*
    AimPointController.cs

    Aiming with the mouse is handled in this script
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Runtime.InteropServices;

public class AimPointController : MonoBehaviour
{
    public Terrain[] allTerrains;
    public TerrainCollider terrainCollider;
    private Camera mainCam;
    Vector3 worldPosition;
    Ray ray;
    RaycastHit hitData;
    Vector3 mousePos;

    [Range(-100.0F, 100.0F)]
    public float cursorDistance = 40f;
    
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);
    int xPos = 30, yPos = 1000;   
    

    void Awake()
    {
        allTerrains = Terrain.activeTerrains;

        for(int i = 0; i < allTerrains.Length; i++)
        {
            if(allTerrains[i].name == "Terrain")
            {
                terrainCollider = allTerrains[i].GetComponent<TerrainCollider>();
            }
        }

        mainCam = GameObject.FindWithTag("SceneCamera").GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Confined;

        //terrainCollider = mainTerrain.GetComponent<TerrainCollider>();
        SetCursorPos(Screen.width /2, Screen.height /2);//Call this when you want to set the mouse position

    }

    void Update()
    {
        //mousePos = new Vector3(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2, 0);
        //mousePos = GetScreenPosition();

        //mousePos = new Vector3(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2, 0);
        //ray = Camera.main.ScreenPointToRay(mousePos * cursorDistance);

        mousePos = GetScreenPosition();
        ray = mainCam.ScreenPointToRay(mousePos);

        //ray = Camera.main.ViewportPointToRay(Mouse.current.position.ReadValue() * cursorDistance);

        //ray = Camera.main.ViewportPointToRay(GetBoundedScreenPosition());

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);

        if (terrainCollider.Raycast(ray, out hitData, 1000))
        {
            worldPosition = hitData.point;
        }

        this.transform.position = worldPosition;
    }
    
    public Vector2 GetScreenPosition()
    {
        return Mouse.current.position.ReadValue();
    }

    // public Vector3 GetBoundedScreenPosition()
    // {
    //     Vector3 raw = GetScreenPosition();
    //     return new Vector3( Mathf.Clamp(raw.x, 0, Screen.width), 
    //                             Mathf.Clamp(raw.y, 0, Screen.height), 0);
    // }

    // public Vector2 GetViewportPosition()
    // {
    //     Vector2 screenPos = GetScreenPosition();
    //     return screenPos / new Vector2(Screen.width, Screen.height);
    // }
}
