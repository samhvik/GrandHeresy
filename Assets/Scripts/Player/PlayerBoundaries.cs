/*
    PlayerBoundaries.cs

    This class controls players not going out of bounds of the camera
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundaries : MonoBehaviour
{ 
    // Controls the widht and height of how far our players can walk
    public int XaxisClampWidth = 32;
    public int ZaxisClampHeight = 15;

    public GameObject cameraTracker;

    void Start()
    {
        cameraTracker = GameObject.Find("CameraTracker");
    }

    // Clamping our player position so they do not go out of bounds
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;

        viewPos.x = Mathf.Clamp(viewPos.x, cameraTracker.transform.position.x - XaxisClampWidth, cameraTracker.transform.position.x + XaxisClampWidth);
        viewPos.z = Mathf.Clamp(viewPos.z, cameraTracker.transform.position.z - ZaxisClampHeight, cameraTracker.transform.position.z + ZaxisClampHeight);

        transform.position = viewPos;
    }
}
