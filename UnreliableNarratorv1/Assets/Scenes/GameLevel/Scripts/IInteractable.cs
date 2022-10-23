using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public interface IInteractable
{
    public string interactFail { get; }
    public string InteractionPrompt { get; }
    public int interactID { get; }
    public bool Interact(Interactor interactor);
    public string OnInteractSuccessSoundName { get; }
    public string OnInteractFailSoundName { get; }
}
