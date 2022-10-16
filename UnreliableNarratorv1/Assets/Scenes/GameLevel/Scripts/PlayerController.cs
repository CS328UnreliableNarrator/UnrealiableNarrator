using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [Header("Behaviors")]
    public PlayerMovementBehavior playerMovementBehavior;
    public PlayerLookBehavior playerLookBehavior;

    [Header("Input Settings")]
    public PlayerInput playerInput;
    private bool movementHeld = false; //fix input movement input needs to be restarted after camera toggle

    [Range(0.0f, 30.0f)] public float movementSmoothingSpeed = 15.0f;

    private Vector3 rawInputMovement;
    private Vector3 consumerInputMovement;
    private Vector3 smoothInputMovement;

    [Range(0.0f, 30.0f)] public float lookSmoothingSpeed = 15.0f;

    private Vector3 rawInputLook;
    private Vector3 consumerInputLook;
    private Vector3 smoothInputLook;

    public InteractionPanel interactionPanel;

    [Header("Camera Settings")]
    [SerializeField] private Camera cam3D;
    [SerializeField] private Camera cam2D;
    [SerializeField] private bool cam3DInitialized;

    //Input Action Maps
    private string actionMapPlayerControls = "Player Controls";
    private string actionMapMenuControls = "Menu Controls";

    //Current Input Control Scheme (Gamepad or Mouse+Keyboard)
    private string currentControlScheme;

    public bool playerInteracted = false;
    // Start is called before the first frame update
    void Start()
    {
        cam3D.enabled = cam3DInitialized;
        cam2D.enabled = !cam3DInitialized;
        
        cam3D.GetComponent<AudioListener>().enabled = cam3DInitialized;
        cam2D.GetComponent<AudioListener>().enabled = !cam3DInitialized;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayer();
        this.UpdateInteractionFinder();
    }

    void UpdatePlayer()
    {
        if (!cam3D.enabled) consumerInputMovement = Vector3.zero;
        CalculateMovementInputSmoothing();
        UpdatePlayerMovement();
        CalculateLookInputSmoothing();
        UpdatePlayerLook();
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        if (cam3D.enabled)
        {
            Vector2 inputMovement = value.ReadValue<Vector2>();
            rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
            consumerInputMovement = rawInputMovement;
            rawInputMovement = Vector3.zero;
        }
        Debug.Log(string.Format("Raw Movement Input: {0}", rawInputMovement));
    }

    public void OnLook(InputAction.CallbackContext value)
    {
        if (cam3D.enabled)
        {
            Vector2 inputLook = value.ReadValue<Vector2>();
            rawInputLook = new Vector3(inputLook.x, inputLook.y, 0);
            consumerInputLook = rawInputLook;
            rawInputLook = Vector3.zero;
        }
        Debug.Log(string.Format("Raw Look Input: {0}", rawInputLook));
    }

    public void OnInteract(InputAction.CallbackContext value)
    {
        if (cam3D.enabled)
        {
            playerInteracted = true;
        }
    }

    public void ToggleCamera(InputAction.CallbackContext value)
    {
        cam3D.enabled = !cam3D.enabled;
        cam2D.enabled = !cam2D.enabled;
                
        cam3D.GetComponent<AudioListener>().enabled = !cam3D.GetComponent<AudioListener>().enabled;
        cam2D.GetComponent<AudioListener>().enabled = !cam2D.GetComponent<AudioListener>().enabled; 
    }

    void CalculateMovementInputSmoothing()
    {
        smoothInputMovement = Vector3.Lerp(smoothInputMovement, consumerInputMovement, Time.deltaTime * movementSmoothingSpeed);
    }

    void CalculateLookInputSmoothing()
    {
        smoothInputLook = Vector3.Lerp(smoothInputLook, consumerInputLook, Time.deltaTime * lookSmoothingSpeed);
    }

    void UpdatePlayerMovement()
    {

        playerMovementBehavior.UpdateMovementData(smoothInputMovement);
    }

    void UpdatePlayerLook()
    {
        playerLookBehavior.UpdateLookData(smoothInputLook);
    }

    public void EnableGameplayControls()
    {
        playerInput.SwitchCurrentActionMap(actionMapPlayerControls);
    }

    public void EnableMenuControls()
    {
        playerInput.SwitchCurrentActionMap(actionMapMenuControls);
    }

    private void UpdateInteractionFinder()
    {
        // this function is supposed to check if there is an interactable item withing close distance
        // on the center of player's screen to allow him to interact with that
        
        // TO DO not finished
        
        RaycastHit hit;
        Physics.Raycast(this.transform.position, this.cam3D.transform.forward, out hit);

        if (hit.collider && hit.distance < 10)
        {
            // Destroy(hit.collider.gameObject);
            // Debug.Log(hit.distance);
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "InteractionTrigger")
        {
            Debug.Log("Interaction trigger entered!");
            List<InteractionData> actions = collider.GetComponent<InteractionTrigger>().Actions;
            for (int i = 0; i < actions.Count; i++)
            {
                this.interactionPanel.AddButton(actions[i]);
            }
        }
    }
    
    public void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "InteractionTrigger")
        {
            Debug.Log("Interaction trigger exited!");
            List<InteractionData> actions = collider.GetComponent<InteractionTrigger>().Actions;
            for (int i = 0; i < actions.Count; i++)
            {
                // this.interactionPanel.(actions[i]);
                // TO DO
                this.interactionPanel.DeleteButton(actions[i]);
            }
        }
    }  
}
