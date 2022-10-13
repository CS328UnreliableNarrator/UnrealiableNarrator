using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPanel : MonoBehaviour
{
    public GameObject InteractionButtonPrefab;

    public List<GameObject> InteractionButtonsPresent = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        this.AddButton();
        this.AddButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddButton()
    {
        GameObject newButton = Instantiate(this.InteractionButtonPrefab);
        newButton.transform.SetParent(this.transform);
        newButton.GetComponent<InteractionButton>().SetPositionIndex(this.InteractionButtonsPresent.Count);
        newButton.GetComponent<InteractionButton>().SetText("Test btn");
        
        this.InteractionButtonsPresent.Add(newButton);
    }
}
