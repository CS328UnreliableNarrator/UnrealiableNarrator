using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum DoorState
{
    Open,
    Closed,
    Changing,
    Locked
}
public class DoorInteractable : MonoBehaviour, IInteractable
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

    private DoorState from;
    private DoorState state;
    private Quaternion closed;
    private Quaternion open;
    public float speed = 20.0f;
    private BoxCollider pivot;
    private float EPSILON = 0.1f;
    [SerializeField] [Range(0.1f, 180.0f)] public float angle = 120.0f;
    void Start()
    {
        pivot = this.GetComponent<BoxCollider>();
        state = DoorState.Closed;
        from = DoorState.Open;
        closed = pivot.transform.rotation;
        open = pivot.transform.rotation;
        open.y += angle;
    }
    void FixedUpdate()
    {
        if (state == DoorState.Changing) RotateDoor();
        //Debug.Log(string.Format("{0}->{1}", from.ToString(), state.ToString()));
    }
    public bool Interact(Interactor interactor)
    {

        switch (id)
        {
            case 1:
                if (PlayerPrefs.HasKey("OfficeDoorLock"))
                {
                    bool locked = System.Convert.ToBoolean(PlayerPrefs.GetInt("OfficeDoorLock"));
                    if(!locked)
                    {
                        Debug.Log("Using Door!");
                        ToggleDoor();
                        return true;
                    }
                    else
                    {
                        interactor.Pointer.SetName(promptFail);
                    }
                }
                else
                {
                    interactor.Pointer.SetName(promptFail);
                }
                break;
            case 2:
                if (PlayerPrefs.HasKey("LobbyLock"))
                {
                    bool locked = System.Convert.ToBoolean(PlayerPrefs.GetInt("LobbyLock"));
                    if (!locked)
                    {
                        ToggleDoor();
                        return true;
                    }
                    else
                    {
                        interactor.Pointer.SetName(promptFail);
                    }
                }
                break;
            default:
                Debug.Log("Using Door!");
                ToggleDoor(); //TODO, make unique
                return true;
        }
        return false;
    }
    void ToggleDoor()
    {
        if(state == DoorState.Locked)
        {
            FindObjectOfType<AudioManager>().Play(OnFail); //as it stands when/if this is set up, there's no delay on input, thus audio couild be spammed
        }
        else if (state != DoorState.Changing)
        {
            from = state;
            state = DoorState.Changing;
            FindObjectOfType<AudioManager>().Play(OnSuccess);
            //Debug.Log(string.Format("{0}->{1}", from.ToString(), state.ToString()));
        }
    }
    void RotateDoor()
    {
        if (from == DoorState.Closed)
        {
            pivot.transform.rotation = Quaternion.Lerp(pivot.transform.rotation, Quaternion.Euler(open.x, open.y, open.z), Time.deltaTime * speed);
            if (pivot.transform.rotation == Quaternion.Euler(open.x, angle - EPSILON, open.z))
            {
                //pivot.transform.rotation = open;
                state = DoorState.Open;
            }
        }
        else if (from == DoorState.Open)
        {
            pivot.transform.rotation = Quaternion.Lerp(pivot.transform.rotation, Quaternion.Euler(closed.x, closed.y, closed.z), Time.deltaTime * speed);
            if (pivot.transform.rotation == Quaternion.Euler(closed.x, 0.0f + EPSILON, closed.z))
            {
                //pivot.transform.rotation = closed;
                state = DoorState.Closed;
            }
        }

    }
}
