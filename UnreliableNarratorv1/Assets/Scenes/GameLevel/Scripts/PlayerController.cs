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
    public float movementSmoothingSpeed = 1f;
    private Vector3 rawInputMovement;
    private Vector3 smoothInputMovement;
    public float lookSmoothingSpeed = 1f;
    private Vector3 rawInputLook;
    private Vector3 smoothInputLook;

    public InteractionPanel interactionPanel;

    [SerializeField] private Camera cam3D;
    [SerializeField] private Camera cam2D;

    [SerializeField] private bool cam3DInitialized;

    //Input Action Maps
    private string actionMapPlayerControls = "Player Controls";
    private string actionMapMenuControls = "Menu Controls";

    //Current Input Control Scheme (Gamepad or Mouse+Keyboard)
    private string currentControlScheme;

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

    }

    void UpdatePlayer()
    {
        

        if (cam3D.enabled)
        {
            CalculateMovementInputSmoothing();
            UpdatePlayerMovement();
            CalculateLookInputSmoothing();
            UpdatePlayerLook();
        }
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
        //Debug.Log(string.Format("Raw Movement Input: {0}", rawInputMovement));
    }

    public void OnLook(InputAction.CallbackContext value)
    {
        Vector2 inputLook = value.ReadValue<Vector2>();
        rawInputLook = new Vector3(inputLook.x, inputLook.y, 0);
        //Debug.Log(string.Format("Raw Look Input: {0}", rawInputLook));
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
        smoothInputMovement = Vector3.Lerp(smoothInputMovement, rawInputMovement, Time.deltaTime * movementSmoothingSpeed);
    }

    void CalculateLookInputSmoothing()
    {
        smoothInputLook = Vector3.Lerp(smoothInputLook, rawInputLook, Time.deltaTime * lookSmoothingSpeed);
        //smoothInputLook = rawInputLook;
    }

    void UpdatePlayerMovement()
    {

        playerMovementBehavior.UpdateMovementData(smoothInputMovement);
        //rawInputMovement = new Vector3(0.0f, 0.0f, 0.0f);
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
