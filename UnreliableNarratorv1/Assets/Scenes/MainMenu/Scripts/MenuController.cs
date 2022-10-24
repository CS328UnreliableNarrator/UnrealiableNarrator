using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public int gameSceneIndex;

    public void StartGame()
    {
        // if we use player prefs for settings then this needs to be changed
        // to just delete the relevant keys on game start
        PlayerPrefs.DeleteAll();
        
        FindObjectOfType<FadeController>().GetComponent<FadeController>().FadeIn(this._loadGameScene);
    }

    private void _loadGameScene()
    {
        SceneManager.LoadScene(this.gameSceneIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene(4);
    }
}
