using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public bool fadeOutOnStart = true;
    public delegate void FunctionDelegate();
    // Start is called before the first frame update
    void Start()
    {
        if (this.fadeOutOnStart)
        {
            this.FadeOut();
        }
        else
        {
            this.GetComponent<Image>().enabled = false;
        }
    }

    public void FadeOut()
    {
        this.GetComponent<Animator>().SetTrigger("FadeOut");
        StartCoroutine(this._hideImageComponent());
    }

    public void FadeIn()
    {
        this.GetComponent<Image>().enabled = true;
        this.GetComponent<Animator>().SetTrigger("FadeIn");
    }
    
    public void FadeIn(FunctionDelegate function)
    {
        this.GetComponent<Image>().enabled = true;
        this.GetComponent<Animator>().SetTrigger("FadeIn");
        StartCoroutine(this._callFunction(function));
    }

    private IEnumerator _callFunction(FunctionDelegate function)
    {
        yield return new WaitForSeconds(1f);
        function();
    }

    private IEnumerator _hideImageComponent()
    {
        yield return new WaitForSeconds(1f);
        this.GetComponent<Image>().enabled = false;
    }
}
