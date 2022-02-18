/*
    CameraController.cs

    This class controls the main gameplay camera. 
    Currently follows the only player.
    Goal: Follow the mean position of all players. See: Helldivers
*/
#pragma warning disable 108

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour{
    
    private Camera cameraMain;
    private PlayerMovements playerMovement;

    private Transform transform;
    private Transform target;
    public Transform playerTransform;

    private Vector3 velocity = Vector3.zero;

    [SerializeField] [Range(0.01f, 1f)]
    private float panSpeed = 0.250f;

    public Vector3 offset = new Vector3(0.0f, 10.0f, -9.0f);

    // Tracker that centers the camera if 2+ players are present
    public GameObject cameraTracker;

    // For Tracker Camera Zoom
    [SerializeField]
    [Range(15f, 40f)]
    private float maxZoom = 25f;
    [SerializeField]
    [Range(15f, 25f)]
    private float minZoom = 15f;

    private float largestDistance;
    private float distance;



    void Start(){
        cameraMain = GameObject.Find("Main Camera").GetComponent<Camera>();
        transform = this.GetComponent<Transform>();

        cameraTracker = GameObject.Find("CameraTracker");
    }

    void Update()
    {
        trackPlayers();
    }


    void LateUpdate(){
        switch(playerMovement.currentState){
            case PlayerMovements.PlayerMovementState.Aiming:
                if (GameValues.instance.numPlayers == 1)
                {
                    offset.x = 7f * Mathf.Cos(playerTransform.eulerAngles.y * Mathf.Deg2Rad);
                    offset.z = -10f + -(5f * Mathf.Sin(playerTransform.eulerAngles.y * Mathf.Deg2Rad));
                }
                break;
            default:
                offset.x = 0f;
                break;
        }

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, panSpeed);
    }

    // Shake: Shakes the camera smoothly, based on magnitude and duration of the lerp.
    public IEnumerator Shake(float magnitude, float sharpness){
        float x = Random.Range(-1f, 1f) * magnitude;
        float z = Random.Range(-1f, 1f) * magnitude;

        Vector3 newPos = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, sharpness);
        
        yield return 0;
    }

    void trackPlayers()
    {
        // Finding Prefab Players with tag "Player"
        if (GameValues.instance.numPlayers == 1)
        {
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovements>();
            playerTransform = playerMovement.transform;
            target = playerMovement.transform;
        }
        else
        {
            // Track the Midpoint
            playerTransform = cameraTracker.transform;
            target = cameraTracker.transform;


            // Zoom in and Out Function
            largestDistance = 0;
            distance = 0;

            for (int i = 0; i < GameValues.instance.numPlayers; i++)
            {
                distance = Vector3.Distance(GameValues.instance.playerPosition[i].transform.position, cameraTracker.transform.position);

                if (distance > largestDistance)
                {
                    largestDistance = distance;
                }
            }

            // Caps how far out we can zoom
            if ((minZoom + largestDistance) <= maxZoom)
            {
                offset.x = 0f;
                offset.y = minZoom + Mathf.Abs(largestDistance);
                offset.z = -9f - Mathf.Abs(largestDistance * 0.6f);
            }
        }
    }
}
