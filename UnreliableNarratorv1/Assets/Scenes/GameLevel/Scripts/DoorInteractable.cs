using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum DoorState
{
    Open,
    Closed,
    Changing
}
public class DoorInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;

    [SerializeField] private int id;
    public int interactID => id;
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
        Debug.Log(string.Format("{0}->{1}", from.ToString(), state.ToString()));
    }
    public bool Interact(Interactor interactor)
    {
        Debug.Log("Using Door!");
        ToggleDoor(); //TODO, make unique
        return true;
    }
    void ToggleDoor()
    {
        if (state != DoorState.Changing)
        {
            from = state;
            state = DoorState.Changing;
            Debug.Log(string.Format("{0}->{1}", from.ToString(), state.ToString()));
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
