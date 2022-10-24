using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : MonoBehaviour
{
    public GameObject emailWindow;
    public GameObject controlPanelWindow;
    public GameObject menuStart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        Cursor.visible = true;


    }
    private void OnDisable()
    {
        Cursor.visible = false;
    }
    public void ToggleEmailWindow()
    {
        this.emailWindow.SetActive(!this.emailWindow.activeSelf);
    }
    
    public void ToggleControlPanelWindow()
    {
        this.controlPanelWindow.SetActive(!this.controlPanelWindow.activeSelf);
    }
    
    public void ToggleMenuStart()
    {
        this.menuStart.SetActive(!this.menuStart.activeSelf);
    }
}
