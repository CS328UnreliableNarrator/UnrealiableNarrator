using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionData : MonoBehaviour
{
    public string name;
    public Color buttonColor = Color.black;
    public Color buttonFontColor = Color.white;
    public abstract void Action();
    
    public string GetName()
    {
        return this.name;
    }
}


// public abstract class InteractionData : MonoBehaviour
// {
//     public abstract void Action();
//     
//     public string GetName()
//     {
//         return this.name;
//     }
// }
