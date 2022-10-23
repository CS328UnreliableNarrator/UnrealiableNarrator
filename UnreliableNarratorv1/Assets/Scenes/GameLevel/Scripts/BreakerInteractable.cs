using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakerInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private string promptFail;
    [SerializeField] private int id;
    public string InteractionPrompt => prompt;
    public string interactFail => promptFail;
    public int interactID => id;

    [SerializeField] private string OnSuccess;
    public string OnInteractSuccessSoundName => OnSuccess;
    [SerializeField] string OnFail;
    public string OnInteractFailSoundName => OnFail;

    public bool Interact(Interactor interactor)
    {
        FindObjectOfType<AudioManager>().Play("WireBoxSuccess"); //Successful interact
        SceneManager.LoadScene(3); //TODO, make unique
        return true;
    }
}
