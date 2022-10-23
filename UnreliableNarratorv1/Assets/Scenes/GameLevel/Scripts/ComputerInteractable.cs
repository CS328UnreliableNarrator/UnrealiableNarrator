using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ComputerInteractable : MonoBehaviour, IInteractable
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
        bool locked;
        if (PlayerPrefs.HasKey("PCLock"))
        {
            locked = System.Convert.ToBoolean(PlayerPrefs.GetInt("PCLock"));
            Debug.Log("Locked: " + locked);
            if (!locked)
            {
                Debug.Log("Using Computer!");
                FindObjectOfType<AudioManager>().Play(OnSuccess);
                SceneManager.LoadScene(2); //TODO, make unique
                return true;
            }
            else
            {
                
                interactor.Pointer.SetName(promptFail);
                return false;
            }
        }
        else
        {
            FindObjectOfType<AudioManager>().Play(OnFail);
            Debug.Log("Prompt Fail: " + promptFail);
            PointerController pointer = interactor.Pointer;
            pointer.SetName(promptFail);
            return false;
        }
    }
}