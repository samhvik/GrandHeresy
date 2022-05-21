using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Rendering;
//using UnityEngine.Rendering.HighDefinition;

//public enum BlurBackground { Blur, NoBlur}

public class MouseHoverSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    //public BlurBackground blurSelection;

    [SerializeField]
     private Selectable selectable = null;
     public GameObject dummyButton;
     //public Volume m_UIVolume;
     
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
