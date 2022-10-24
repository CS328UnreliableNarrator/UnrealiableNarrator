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
        switch(id)
        {
            // Bosses Computer
            case 0:
                bool locked;
                if (PlayerPrefs.HasKey("PCLock"))
                {
                    locked = System.Convert.ToBoolean(PlayerPrefs.GetInt("PCLock"));
                    Debug.Log("Locked: " + locked);
                    if (!locked)
                    {
                        if(System.Convert.ToBoolean(PlayerPrefs.GetInt("BossUnlock")))
                        {
                            SceneManager.LoadScene(6); //TODO, make unique
                        }
                        Debug.Log("Using Computer!");
                        FindObjectOfType<AudioManager>().Play(OnSuccess);
                        PlayerPrefs.SetInt("BossUnlock", 1);
                        SceneManager.LoadScene(6); //TODO, make unique
                        return true;
                    }
                    else
                    {
                        interactor.Pointer.SetName(promptFail);
                    }
                }
                else
                {
                    FindObjectOfType<AudioManager>().Play(OnFail);
                    Debug.Log("Prompt Fail: " + promptFail);
                    PointerController pointer = interactor.Pointer;
                    pointer.SetName(promptFail);
                }
                break;
            // Camille's Computer
            case 1:
                Debug.Log("Using Computer!");
                FindObjectOfType<AudioManager>().Play(OnSuccess);
                SceneManager.LoadScene(5);
                return true;
            default:
                Debug.Log("This computer does not have a valid ID");
                break;


       }
        
        return false;
    }
}