using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
    public float pickupRadius;
    public int maxInteractables;

    private PlayerControls controls;
    private static Collider[] interactables;
    private PlayerInventory playerInventory;
    private AnimatorManager animManager;

    private void Start() {
        interactables = new Collider[maxInteractables];
        playerInventory = GetComponent<PlayerInventory>();
        animManager = this.GetComponent<AnimatorManager>();
    }

    private void Awake() {
        controls = new PlayerControls();
        
        controls.Gameplay.Interact.performed += ctx => OnPlayerInteract();
    }
    
    // Enable and disable control input when script is enabled/disabled.
    void OnEnable(){
        controls.Gameplay.Enable();
    }

    void OnDisable(){
        controls.Gameplay.Disable();
    }

    private void OnPlayerInteract() {
        
        Physics.OverlapSphereNonAlloc(transform.position, pickupRadius, interactables);
        var minDist = float.MaxValue;
        Collider toInteract = null;
        foreach (var interactable in interactables) {
            if (interactable is null) break;
            if (interactable.CompareTag("ObjectiveSpawn") ||
                     interactable.CompareTag("ObjectiveSpawnNE") ||
                     interactable.CompareTag("ObjectiveSpawnNW") ||
                     interactable.CompareTag("ObjectiveSpawnSE") ||
                     interactable.CompareTag("ObjectiveSpawnSW") ||
                     interactable.CompareTag("Beacon")||
                     interactable.CompareTag("Drop")) {
                var interactableDist = Vector3.Distance(interactable.transform.position, transform.position);
                if (minDist > interactableDist) {
                    minDist = interactableDist;
                    toInteract = interactable;
                }
                Debug.Log("help");
            }
        }

        if (toInteract is null) return;
        Debug.Log(toInteract.tag);
        switch (toInteract.tag) {
            case "Drop":
                
                // run gear script
                playerInventory.OnPickupWeapon(toInteract);
                //Destroy(toInteract.gameObject);
                break;
            case "Beacon":
                if(GameValues.instance.extractionOpen){
                    GameValues.instance.extractionStarted = true;
                }
                break;
            case "ObjectiveSpawn":
            case "ObjectiveSpawnNE":
            case "ObjectiveSpawnNW":
            case "ObjectiveSpawnSE":
            case "ObjectiveSpawnSW":
                // run objective script
                break;
            default:
                // shouldn't reach here cause everything should have a tag
                Debug.Log("interacted with collider with no tag");
                break;
        }
    }
    
    
}
