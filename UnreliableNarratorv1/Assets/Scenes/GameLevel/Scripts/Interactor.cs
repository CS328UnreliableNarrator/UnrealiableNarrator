using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask interactableMask;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound;

    [SerializeField] private GameObject player;

    private void Start()
    {
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
    private void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, interactableMask);

        if(numFound > 0)
        {
            var interactable = colliders[0].GetComponent<IInteractable>();
            if(interactable != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                Debug.Log("e was pressed");
                float x = player.transform.position.x;
                float y = player.transform.position.y;
                float z = player.transform.position.y;
                PlayerPrefs.SetFloat("x", x);
                PlayerPrefs.SetFloat("y", y);
                PlayerPrefs.SetFloat("z", z);
                interactable.Interact(this);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }
}
