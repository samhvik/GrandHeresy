using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   InputManager inputManager;

   private void Awake() {
       inputManager = GetComponent<InputManager>();
   }

   private void Update() {
       inputManager.HandleAllInputs();
   }
}
