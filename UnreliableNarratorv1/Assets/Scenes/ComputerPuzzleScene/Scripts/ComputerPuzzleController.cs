using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPuzzleController : MonoBehaviour
{
    public GameObject emailWindow;
    public GameObject controlPanelWindow;
    public GameObject menuStart;

    [SerializeField] private string answer;
    [SerializeField] private TMPro.TMP_InputField input;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(input.text == answer)
        {
            PlayerPrefs.SetInt("PCLock", 0);
        }
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
