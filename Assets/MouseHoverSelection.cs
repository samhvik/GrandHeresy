using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseHoverSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField]
     private Selectable selectable = null;
     public GameObject dummyButton;
     
     public void OnPointerEnter(PointerEventData eventData)
     {
         selectable.Select();
     }

     public void OnPointerExit(PointerEventData eventData)
     {
         EventSystem.current.SetSelectedGameObject(dummyButton);
     }

     public void DebugStuff()
     {
         Debug.Log("CLICKED");
     }
}
