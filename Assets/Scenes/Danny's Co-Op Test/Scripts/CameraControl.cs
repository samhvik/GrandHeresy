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

    public Vector3 offset = new Vector3(0.0f, 10.0f, -6.0f);

    // Tracker that centers the camera if 2+ players are present
    public GameObject cameraTracker;
    private Vector3 zoomPosition;

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
                offset.x = 7f * Mathf.Cos(playerTransform.eulerAngles.y * Mathf.Deg2Rad);
                offset.z = -10f + -(5f * Mathf.Sin(playerTransform.eulerAngles.y * Mathf.Deg2Rad));
                break;
            default:
                offset.x = 0f;
                offset.z = -10f;
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
            playerTransform = cameraTracker.transform;
            target = cameraTracker.transform;
            offset.y = 15 + TrackingCamera.instance.maxDistance;
            Debug.Log(TrackingCamera.instance.maxDistance);
            //zoomPosition = new Vector3(0, 15 + TrackingCamera.instance.farthestPlayer.y, -9);
            //zoomPosition = new Vector3(0, 15 + TrackingCamera.instance.maxDistance, -9);
            //cameraMain.transform.position = zoomPosition;
        }
    }
}
