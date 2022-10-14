using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPanel : MonoBehaviour
{
    public GameObject InteractionButtonPrefab;

    public List<GameObject> InteractionButtonsPresent = new List<GameObject>();

    private int _numberOfButtonsPresent = 0;
    
    void Update()
    {
        if (this._numberOfButtonsPresent != this.InteractionButtonsPresent.Count)
        {
            for (int i = 0; i < this.InteractionButtonsPresent.Count; i++)
            {
                this.InteractionButtonsPresent[i].GetComponent<InteractionButton>().SetPositionIndex(i, true);
            }
        }

        this._numberOfButtonsPresent = this.InteractionButtonsPresent.Count;
    }

    public void AddButton(InteractionData interactionData)
    {
        GameObject newButton = Instantiate(this.InteractionButtonPrefab);
        newButton.transform.SetParent(this.transform);
        newButton.GetComponent<InteractionButton>().InteractionPanel = this;
        newButton.GetComponent<InteractionButton>().SetPositionIndex(this.InteractionButtonsPresent.Count);
        newButton.GetComponent<InteractionButton>().SetText(interactionData.GetName());
        newButton.GetComponent<InteractionButton>().SetInteractionData(interactionData);
        
        newButton.GetComponent<InteractionButton>().SetColor(interactionData.buttonColor);
        newButton.GetComponent<InteractionButton>().SetFontColor(interactionData.buttonFontColor);
        
        
        
        this.InteractionButtonsPresent.Add(newButton);
    }

    public void DeleteButton(InteractionData interactionData)
    {
        for (int i = 0; i < this.InteractionButtonsPresent.Count; i++)
        {
            InteractionData btnInteractionData = this.InteractionButtonsPresent[i].GetComponent<InteractionButton>().InteractionData;
            if (btnInteractionData.Equals(interactionData))
            {
                this.InteractionButtonsPresent[i].GetComponent<InteractionButton>().Out();
                this.InteractionButtonsPresent.RemoveAt(i);
                Debug.Log("Button deleted");
                return;
            }
        }

        Debug.Log("Button to delete was not present!");
    }
}
