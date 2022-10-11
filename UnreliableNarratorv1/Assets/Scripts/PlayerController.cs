using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [Header("Behaviors")]
    public PlayerMovementBehavior playerMovementBehavior;

    [Header("Input Settings")]
    public PlayerInput playerInput;
    public float movementSmoothingSpeed = 1f;
    private Vector3 rawInputMovement;
    private Vector3 smoothInputMovement;

    [SerializeField] private Camera cam3D;
    [SerializeField] private Camera cam2D;
    
    //Input Action Maps
    private string actionMapPlayerControls = "Player Controls";
    private string actionMapMenuControls = "Menu Controls";

    //Current Input Control Scheme (Gamepad or Mouse+Keyboard)
    private string currentControlScheme;

    // Start is called before the first frame update
    void Start()
    {
        cam3D.enabled = true;
        cam2D.enabled = false;
        
        cam3D.GetComponent<AudioListener>().enabled = true;
        cam2D.GetComponent<AudioListener>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovementInputSmoothing();
        UpdatePlayerMovement();
        
        cam3D.GetComponent<AudioListener>().enabled = !cam3D.GetComponent<AudioListener>().enabled;
        cam2D.GetComponent<AudioListener>().enabled = !cam2D.GetComponent<AudioListener>().enabled; 
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
        Debug.Log(string.Format("{0}", rawInputMovement));
    }

    public void changeCamera(InputAction.CallbackContext value)
    {
        cam3D.enabled = !cam3D.enabled;
        cam2D.enabled = !cam2D.enabled;
    }

    void CalculateMovementInputSmoothing()
    {
        smoothInputMovement = Vector3.Lerp(smoothInputMovement, rawInputMovement, Time.deltaTime * movementSmoothingSpeed);
    }

    void UpdatePlayerMovement()
    {
        playerMovementBehavior.UpdateMovementData(smoothInputMovement);
    }

    public void EnableGameplayControls()
    {
        playerInput.SwitchCurrentActionMap(actionMapPlayerControls);
    }

    public void EnableMenuControls()
    {
        playerInput.SwitchCurrentActionMap(actionMapMenuControls);
    }
    
    
}
