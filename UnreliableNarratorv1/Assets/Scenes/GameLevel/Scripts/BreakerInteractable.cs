using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakerInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private int id;
    public string InteractionPrompt => prompt;

    public int interactID => id;

    public bool Interact(Interactor interactor)
    {
        SceneManager.LoadScene(3); //TODO, make unique
        return true;
    }
}
