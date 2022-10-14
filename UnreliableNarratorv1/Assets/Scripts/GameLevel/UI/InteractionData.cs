using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionData : MonoBehaviour
{
    public abstract string GetName();
    public abstract void Action();
}
