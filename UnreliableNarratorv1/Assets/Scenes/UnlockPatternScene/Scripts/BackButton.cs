using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public void ChangeScene()
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
