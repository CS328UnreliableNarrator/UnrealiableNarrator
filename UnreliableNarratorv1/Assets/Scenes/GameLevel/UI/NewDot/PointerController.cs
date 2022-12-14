using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    public bool isActive = false;
    public IInteractable Interactable;
    public GameObject text;

    public GameObject E_button_info;
    // Start is called before the first frame update
    void Start()
    {
        this.text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInteractable(IInteractable interactable)
    {
        if (!this.isActive)
        {
            this.Interactable = interactable;
            this.GetComponent<Animator>().SetTrigger("Activate");
            this.text.SetActive(true);
            this.isActive = true;
            this.SetName(interactable.InteractionPrompt);
            this.E_button_info.SetActive(true);
        }
    }
    public void SetName(string name)
    {
        Debug.Log("Set name = " + name);
        this.text.GetComponent<TextMeshProUGUI>().text = name;
        this.text.GetComponent<TextMeshProUGUI>().ForceMeshUpdate();
    }

    public void UnsetInteractable()
    {
        if (this.isActive)
        {
            this.GetComponent<Animator>().SetTrigger("Deactivate");
            this.text.SetActive(false);
            this.isActive = false;
            this.E_button_info.SetActive(false);
        }
    }
}
