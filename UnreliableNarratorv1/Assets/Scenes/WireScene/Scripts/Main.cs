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
    public void UpdateCount(int points)
    {
        onCount = onCount + points;
        if (onCount == correctCount)
        {
            // This is a placeholder, I'm not sure where this puzzle will end up going
            SceneManager.LoadScene(1);
        }
    }
}
