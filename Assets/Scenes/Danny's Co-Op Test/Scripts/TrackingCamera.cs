/*
    TrackingCamera.cs

    Moves our invisible CameraTracker so that we have a midpoint for Co-Op Camera
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingCamera : MonoBehaviour
{
    public static TrackingCamera instance = null;

    // For Tracker Position
    private float midpointX;
    private float midpointZ;
    private Vector3 midpoint;

    // For Tracker Camera Zoom
    public Vector3 farthestPlayer;
    public float maxDistance;
    private float distance;

    void Start()
    {
        maxDistance = 0;
    }

    void Update()
    {
        trackPosition();
        trackZoom();
    }

    // Track the X and Z value of players and find a midpoint
    public void trackPosition()
    {
        if (GameValues.instance.numPlayers >= 2)
        {
            midpointX = 0;
            midpointZ = 0;

            for (int i = 0; i < GameValues.instance.numPlayers; i++)
            {
                midpointX += GameValues.instance.playerPosition[i].position.x;
                midpointZ += GameValues.instance.playerPosition[i].position.z;
            }

            midpointX /= 3;
            midpointZ /= 3;
            midpoint = new Vector3(midpointX, 0, midpointZ);

            this.transform.position = midpoint;
        }
    }

    // Zoom the camera in and out based on the farthest player 
    public void trackZoom()
    {
        if (GameValues.instance.numPlayers >= 2)
        {

            //maxDistance = 0;
            distance = 0;

            for (int i = 0; i < GameValues.instance.numPlayers; i++)
            {
                distance = Vector3.Distance(GameValues.instance.playerPosition[i].transform.position, this.transform.position);

                if(distance > maxDistance)
                {
                    maxDistance = distance;
                    //farthestPlayer = GameValues.instance.playerPosition[i].transform.position;
                }
            }
        }
    }
}
