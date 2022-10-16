using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ComputerInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;

    [SerializeField] private int id;
    public int interactID => id;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Using Computer!");
        SceneManager.LoadScene(2); //TODO, make unique
        return true;
    }
}