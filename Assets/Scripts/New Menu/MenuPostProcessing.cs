using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MenuPostProcessing : MonoBehaviour
{

    [SerializeField]
    private bool blurBackground;
    [SerializeField]
    private GameObject m_postProcessing;

    void OnDisable()
    {
        if(blurBackground) m_postProcessing.SetActive(false);
        else m_postProcessing.SetActive(true);
        //Debug.Log("PrintOnDisable: script was disabled");
    }

    void OnEnable()
    {
        if(blurBackground) m_postProcessing.SetActive(true);
        else m_postProcessing.SetActive(false);
        //Debug.Log("PrintOnEnable: script was enabled");
    }
}
