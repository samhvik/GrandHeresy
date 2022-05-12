/*
    AimPointController.cs

    Aiming with the mouse is handled in this script
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimPointController : MonoBehaviour
{
    public TerrainCollider terrainCollider;
    Vector3 worldPosition;
    Ray ray;

    public float cursorDistance = 40f;

    void Start()
    {
        terrainCollider = Terrain.activeTerrain.GetComponent<TerrainCollider>();
    }

    void Update()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2, 0);
        ray = Camera.main.ScreenPointToRay(mousePos * cursorDistance);

        RaycastHit hitData;
        if (terrainCollider.Raycast(ray, out hitData, 1000))
        {
            worldPosition = hitData.point;
        }

        this.transform.position = worldPosition;
    }
}
