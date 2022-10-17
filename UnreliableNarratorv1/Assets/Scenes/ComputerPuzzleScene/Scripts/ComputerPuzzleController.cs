using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPuzzleController : MonoBehaviour
{
    public GameObject emailWindow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleEmailWindow()
    {
        this.emailWindow.SetActive(!this.emailWindow.activeSelf);
    }
}
