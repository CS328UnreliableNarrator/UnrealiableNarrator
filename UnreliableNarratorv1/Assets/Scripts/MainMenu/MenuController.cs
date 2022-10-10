using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public int gameSceneIndex;

    public void StartGame()
    {
        SceneManager.LoadScene(this.gameSceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
