using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PatternDot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int index;
    public bool isPointerOver;
    public PatternController PatternController;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Dot nr. " + this.index);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse over! " + this.index);
        this.isPointerOver = true;
        this.PatternController.OnMouseEnterDot(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exit! " + this.index);
        this.isPointerOver = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Mouse down! " + this.index);
        this.PatternController.OnDotClicked(this.index);
    }

    public void PlayZoomInOutAnimation()
    {
        this.GetComponent<Animator>().SetTrigger("ZoomInOut");
    }
}
