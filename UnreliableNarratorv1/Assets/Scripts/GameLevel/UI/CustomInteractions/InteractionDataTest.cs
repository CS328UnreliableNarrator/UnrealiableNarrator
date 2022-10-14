using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDataTest : InteractionData
{
    public string name = "test btn";
    public Color buttonColor = Color.black;

    public override string GetName()
    {
        return this.name;
    }

    public override void Action()
    {
        Debug.Log("A");
    }
}
