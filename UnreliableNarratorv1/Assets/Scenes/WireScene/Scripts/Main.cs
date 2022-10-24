using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main Instance;
    public int correctCount;
    private int onCount = 0;

    public void Awake()
    {
        Instance = this;
    }
    public void OnEnable()
    {
        Cursor.visible = true;
    }

    public void OnDisable()
    {
        Cursor.visible = false;
    }
    public void UpdateCount(int points)
    {
        onCount = onCount + points;
        if (onCount == correctCount)
        {
            FindObjectOfType<AudioManager>().Play("WireBoxPuzzleWireWin");
            PlayerPrefs.SetInt("LobbyLock", 0);
            // This is a placeholder, I'm not sure where this puzzle will end up going
            try
            {
                FindObjectOfType<FadeController>().GetComponent<FadeController>().FadeIn(this._loadGameScene);
            }
            catch
            {
                this._loadGameScene();
            }
        }
    }
    
    private void _loadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
