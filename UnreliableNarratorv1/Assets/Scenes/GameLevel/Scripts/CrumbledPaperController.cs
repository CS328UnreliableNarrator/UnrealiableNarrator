using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumbledPaperController : MonoBehaviour
{
    public GameObject UIObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleVisibility()
    {
        this.UIObject.SetActive(!this.UIObject.activeSelf);
    }
}
