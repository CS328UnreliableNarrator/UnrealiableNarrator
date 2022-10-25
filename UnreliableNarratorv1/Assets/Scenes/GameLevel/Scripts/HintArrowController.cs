using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintArrowController : MonoBehaviour
{
    public bool isVisible = false;

    public GameObject HintArrowGameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isVisible) return;
        ;
        if (PlayerPrefs.GetInt("HintArrow") == 1)
        {
            this.isVisible = true;
            this.HintArrowGameObject.SetActive(true);
        }
    }
}
