using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlashMaterialInRange : MonoBehaviour
{
    public bool canFlash = true;
    public Color flashColor;
    public float flashSpeed;
    //public float range;
    private Renderer renderer;
    private Color originalColor;
    private Color currentColor;
    private Transform m_transform;
    

    //public Color =>

    // Start is called before the first frame update
    void OnEnable()
    {
        renderer = GetComponent<MeshRenderer>();
        originalColor = renderer.material.GetColor("_EmissionColor");
        m_transform = this.gameObject.transform;
    }

    void OnDisable()
    {
        renderer.material.SetColor("_EmissionColor", originalColor);
    }

    // Update is called once per frame
    void Update()
    {
        if(canFlash)
        {
            currentColor = Lerp(originalColor, flashColor, flashSpeed);
            renderer.material.SetColor("_EmissionColor", currentColor);
        }

        // try
        // {
        //     if (Vector3.Distance(m_transform.position, GameObject.FindWithTag("Player").transform.position) < range)
        //     {
        //         //Do something because the distance is less than 100
        //         currentColor = Lerp(originalColor, flashColor, flashSpeed);
        //         renderer.material.SetColor("_EmissionColor", currentColor);
        //     }
        // }
        // catch
        // {
        //     Debug.Log("No Player In Range");
        // }
        // renderer.material.color = Lerp(originalColor, flashColor, flashSpeed);
        // renderer.material.color = Lerp(originalColor, flashColor, flashSpeed);
    }

    public Color Lerp(Color _originalColor, Color _flashColor, float _flashSpeed) => Color.Lerp(_originalColor, _flashColor, Mathf.Sin(Time.time * _flashSpeed));
}
