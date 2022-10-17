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
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject playerDefaultReticle;
    [SerializeField] private GameObject playerInteractReticle;

    private void Start()
    {
        playerInteractReticle.SetActive(false);
        if(PlayerPrefs.HasKey("x") && PlayerPrefs.HasKey("y") && PlayerPrefs.HasKey("z"))
        {
            float x = PlayerPrefs.GetFloat("x");
            float y = PlayerPrefs.GetFloat("y");
            float z = PlayerPrefs.GetFloat("z");
            Vector3 position = new Vector3(x, y, z);
            player.transform.position = position;
            PlayerPrefs.DeleteKey("x");
            PlayerPrefs.DeleteKey("y");
            PlayerPrefs.DeleteKey("z");
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
                float z = player.transform.position.y;
                PlayerPrefs.SetFloat("x", x);
                PlayerPrefs.SetFloat("y", y);
                PlayerPrefs.SetFloat("z", z);
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
}
