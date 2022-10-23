using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{

    public GameObject pauseScreen;
    public GameObject pointer;
    private static bool isPaused = false;
    private static bool isDelayActive = false;
    private float timer = 0.1f;

    // Update is called once per frame
    void Start()
    {
        pauseScreen.SetActive(false);
        Cursor.visible = isPaused;
    }
    void Pause()
    {
        pauseScreen.SetActive(true);
        pointer.SetActive(false);
    }
    void Resume()
    {
        pauseScreen.SetActive(false);
        pointer.SetActive(true);
    }
    public void TogglePause()
    {
        //gotta use bools here, float comparison isn't consistent
        if (!isDelayActive)
        {
            isDelayActive = true;
            isPaused = !isPaused;
            Cursor.visible = isPaused;
            Time.timeScale = ((isPaused) ? 0.0f : 1.0f);
            if (!isPaused) Resume();
            else if (isPaused) Pause();
            else Debug.LogError("Somehow time scale is neither 0 or 1");
        }
    }
    void Update()
    {
        if (isDelayActive)
        {
            timer -= Time.unscaledDeltaTime;
            if (timer <= 0.0f)
            {
                isDelayActive = false;
                timer = 0.1f;
            }
        }
    }
}
