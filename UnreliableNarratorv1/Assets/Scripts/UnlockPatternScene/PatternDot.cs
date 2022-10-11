using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternDot : MonoBehaviour
{
    public int index;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseOver()
    {
        Debug.Log("Mouse over! " + this.index);
    }
}
