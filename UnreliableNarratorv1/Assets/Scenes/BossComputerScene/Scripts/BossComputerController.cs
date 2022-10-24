using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossComputerController : MonoBehaviour
{
    public GameObject emailWindow;
    public GameObject powerMenu;

    public void ToggleEmailWindow()
    {
        this.emailWindow.SetActive(!this.emailWindow.activeSelf);
    }
    
    public void TogglePowerMenu()
    {
        this.powerMenu.SetActive(!this.powerMenu.activeSelf);
    }

    public void LeaveScene()
    {
        try
        {
            FindObjectOfType<FadeController>().GetComponent<FadeController>().FadeIn(this._loadGameScene);
        }
        catch
        {
            this._loadGameScene();
        }
    }
    
    private void _loadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
