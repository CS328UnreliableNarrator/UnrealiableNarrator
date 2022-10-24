using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComputerController : MonoBehaviour
{
    public GameObject emailWindow;
    public GameObject controlPanelWindow;
    public GameObject menuStart;
    
    [SerializeField] private string answer;
    [SerializeField] private TMPro.TMP_InputField input;
    public GameObject WrongCodeTextGameObject;

    public GameObject BeforeCorrectCode;
    public GameObject AfterCorrectCode;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("PCLock", 1);
        this.WrongCodeTextGameObject.SetActive(false);

        if (PlayerPrefs.GetInt("PCLock") == 0)
        {
            // if the player already entered the correct code before
            this.SetCorrectCodeState();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        Cursor.visible = true;


    }
    private void OnDisable()
    {
        Cursor.visible = false;
    }
    public void ToggleEmailWindow()
    {
        this.emailWindow.SetActive(!this.emailWindow.activeSelf);
    }
    
    public void ToggleControlPanelWindow()
    {
        this.controlPanelWindow.SetActive(!this.controlPanelWindow.activeSelf);
    }
    
    public void ToggleMenuStart()
    {
        this.menuStart.SetActive(!this.menuStart.activeSelf);
    }

    private void SetCorrectCodeState()
    {
        this.BeforeCorrectCode.SetActive(false);
        this.AfterCorrectCode.SetActive(true);
    }

    public void OnSendCodeButtonClick()
    {
        if (this.input.text.ToUpper() == answer.ToUpper())
        {
            PlayerPrefs.SetInt("PCLock", 0);
            PlayerPrefs.Save();
            Debug.Log("Correct");
            this.SetCorrectCodeState();
        }
        else
        {
            Debug.Log("Incorrect");
            this.WrongCodeTextGameObject.SetActive(true);
            this.input.SetTextWithoutNotify("");
        }
    }
    
    public void LeaveScene()
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
