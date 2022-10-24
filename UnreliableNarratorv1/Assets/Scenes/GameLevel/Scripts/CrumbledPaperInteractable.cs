using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrumbledPaperInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private string promptFail;
    public string InteractionPrompt => prompt;

    public string interactFail => promptFail;
    [SerializeField] private int id;
    public int interactID => id;

    [SerializeField] private string OnSuccess;
    public string OnInteractSuccessSoundName => OnSuccess;
    [SerializeField] string OnFail;
    public string OnInteractFailSoundName => OnFail;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Paper interaction");
        FindObjectOfType<CrumbledPaperController>().GetComponent<CrumbledPaperController>().ToggleVisibility();

        return false;
    }
}
