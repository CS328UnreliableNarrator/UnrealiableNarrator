using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interaction_Computer1 : InteractionData
{
    public override void Action()
    {
        SceneManager.LoadScene("UnlockPattern");
    }
}
