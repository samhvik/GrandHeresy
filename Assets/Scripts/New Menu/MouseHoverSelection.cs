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
    //public virtual void DoStateTransition(Selectable.SelectionState state, bool instant){}

    [SerializeField]
     public Selectable selectable = null;
     public GameObject dummyButton;
     //public Volume m_UIVolume;
     
     public void OnPointerEnter(PointerEventData eventData)
     {
         selectable.Select();
     }

     public void OnPointerExit(PointerEventData eventData)
     {
         //selectable.SelectionState = 0;
         EventSystem.current.SetSelectedGameObject(dummyButton);
         //eventData.selectedObject
         //selectable.DoStateTransition(0, true);
     }

     public void DebugStuff()
     {
         Debug.Log("CLICKED");
     }
}
