using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPuzzle : MonoBehaviour
{
    [SerializeField] private string answer;
    [SerializeField] private TMPro.TMP_InputField input;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (input.text == answer)
        {
            PlayerPrefs.SetInt("PCLock", 0);
        }
    }
}
