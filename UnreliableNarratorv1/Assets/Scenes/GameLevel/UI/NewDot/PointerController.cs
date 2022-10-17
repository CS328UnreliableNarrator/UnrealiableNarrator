using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    public bool isActive = false;
    public IInteractable Interactable;
    public GameObject text;
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
        }
    }

    private void SetName(string name)
    {
        Debug.Log("Set name = " + name);
        this.text.GetComponent<TextMeshProUGUI>().text = name;
    }

    public void UnsetInteractable()
    {
        if (this.isActive)
        {
            this.GetComponent<Animator>().SetTrigger("Deactivate");
            this.text.SetActive(false);
            this.isActive = false;
        }
    }
}
