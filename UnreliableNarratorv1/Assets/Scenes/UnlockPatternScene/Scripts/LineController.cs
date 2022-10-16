using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LineController : MonoBehaviour
{
    public Color color;
    public bool isColorDefault = true;
    // Start is called before the first frame update
    void Start()
    {
        this.color = new Color(1, 0.8f, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isColorDefault)
        {
            Color colorTest = new Color(this.color.r, this.color.g, this.color.b, this.color.a);
            this.GetComponent<LineRenderer>().startColor = colorTest;
            this.GetComponent<LineRenderer>().endColor = colorTest;
        }
    }

    public void PlayCorrectPatternAnimation()
    {
        this.isColorDefault = false;
        this.GetComponent<Animator>().SetTrigger("CorrectPattern");
        Invoke("SwitchScene", 1);

    }

    public void PlayWrongPatternAnimation()
    {
        this.isColorDefault = false;
        this.GetComponent<Animator>().SetTrigger("WrongPattern");
    }
    
    public void PlayExitAnimation()
    {
        this.GetComponent<Animator>().SetTrigger("Exit");
    }

    private void SwitchScene()
    {
        SceneManager.LoadScene(1);
    }
}
