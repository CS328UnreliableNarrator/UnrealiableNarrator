using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI
;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 0.1f;
    [SerializeField] private LayerMask interactableMask;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject fpsCam;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject playerDefaultReticle;
    [SerializeField] private GameObject playerInteractReticle;
    [SerializeField] private PointerController pointer;
    private void Start()
    {
        playerInteractReticle.SetActive(false);
    }

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("x") && PlayerPrefs.HasKey("y") && PlayerPrefs.HasKey("z"))
        {
            Debug.Log("Position should change now");
            float x = PlayerPrefs.GetFloat("x");
            float y = PlayerPrefs.GetFloat("y");
            float z = PlayerPrefs.GetFloat("z");
            Vector3 position = new Vector3(x, y, z);
            Debug.Log("Position: " + position);
            player.transform.position = position;
            PlayerPrefs.DeleteKey("x");
            PlayerPrefs.DeleteKey("y");
            PlayerPrefs.DeleteKey("z");
        }

        if (PlayerPrefs.HasKey("xRot") && PlayerPrefs.HasKey("yRot") && PlayerPrefs.HasKey("zRot"))
        {
            float xRot = PlayerPrefs.GetFloat("xRot");
            float yRot = PlayerPrefs.GetFloat("yRot");
            float zRot = PlayerPrefs.GetFloat("zRot");
            Vector3 rotation = new Vector3(xRot, yRot, zRot);
            Debug.Log("Rotation: " + rotation);
            fpsCam.transform.Rotate(rotation);
            PlayerPrefs.DeleteKey("xRot");
            PlayerPrefs.DeleteKey("yRot");
            PlayerPrefs.DeleteKey("zRot");
        }

    }
    private void LateUpdate()
    {
        
        if (this.playerController.pointerController.isActive)
        {
            if(!playerInteractReticle.activeSelf) ToggleReticle();
            var interactable = this.playerController.pointerController.Interactable;
            if(interactable != null && playerController.playerInteracted)
            {
                Debug.Log("e was pressed");
                float x = player.transform.position.x;
                float y = player.transform.position.y;
                float z = player.transform.position.z;
                Debug.Log("x: " + x + ", y: " + y + " z: " + z);
                PlayerPrefs.SetFloat("x", x);
                PlayerPrefs.SetFloat("y", y);
                PlayerPrefs.SetFloat("z", z);

                float xRot = fpsCam.transform.rotation.eulerAngles.x;
                float yRot = fpsCam.transform.rotation.eulerAngles.y;
                float zRot = fpsCam.transform.rotation.eulerAngles.z;
                PlayerPrefs.SetFloat("xRot", xRot);
                PlayerPrefs.SetFloat("yRot", yRot);
                PlayerPrefs.SetFloat("zRot", zRot);
                playerController.playerInteracted = false;
                interactable.Interact(this);
            }
            else
            {
                // clear the flag, as it should only be consumed when sphere is colliding and interactable
                playerController.playerInteracted = false;
                if (!playerDefaultReticle.activeSelf) ToggleReticle();
            }
            
            // numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, interactableMask);
            //
            // if(numFound > 0)
            // {
            //     if(!playerInteractReticle.activeSelf) ToggleReticle();
            //     var interactable = colliders[0].GetComponent<IInteractable>();
            //     if(interactable != null && playerController.playerInteracted)
            //     {
            //         Debug.Log("e was pressed");
            //         float x = player.transform.position.x;
            //         float y = player.transform.position.y;
            //         float z = player.transform.position.y;
            //         PlayerPrefs.SetFloat("x", x);
            //         PlayerPrefs.SetFloat("y", y);
            //         PlayerPrefs.SetFloat("z", z);
            //         playerController.playerInteracted = false;
            //         interactable.Interact(this);
            //     }
            // }
            // else
            // {
            //     //clear the flag, as it should only be consumed when sphere is colliding and interactable
            //     playerController.playerInteracted = false;
            //     if (!playerDefaultReticle.activeSelf) ToggleReticle();
            // }
        }
        else
        {
            playerController.playerInteracted = false;
        }
    }
    private void ToggleReticle()
    {
        playerDefaultReticle.SetActive(!playerDefaultReticle.activeSelf);
        playerInteractReticle.SetActive(!playerInteractReticle.activeSelf);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }

    public PointerController Pointer
    {
        get { return pointer; }
    }
}
