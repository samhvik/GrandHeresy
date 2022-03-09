/*
    TrackingCamera.cs

    Moves our invisible CameraTracker so that we have a midpoint for Co-Op Camera
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingCamera : MonoBehaviour
{
    // For Tracker Position
    private float midpointX;
    private float midpointZ;
    private Vector3 midpoint;

    void Update()
    {
        trackPosition();
    }

    // Track the X and Z value of players and find a midpoint
    public void trackPosition()
    {
        if (GameValues.instance.numAlive >= 2)
        {
            midpointX = 0;
            midpointZ = 0;

            for (int i = 0; i < GameValues.instance.numPlayers; i++)
            {
                if (GameValues.instance.playerAlive[i] == true)
                {
                    midpointX += GameValues.instance.playerPosition[i].position.x;
                    midpointZ += GameValues.instance.playerPosition[i].position.z;
                }
            }

            midpointX /= GameValues.instance.numPlayers;
            midpointZ /= GameValues.instance.numPlayers;
            midpoint = new Vector3(midpointX, 0, midpointZ);

            this.transform.position = midpoint;
        }
        else if (GameValues.instance.numAlive == 1)
        {
            this.transform.position = GameValues.instance.Players[GameValues.instance.findWhosAlive()].transform.position;
        }
    }
}
