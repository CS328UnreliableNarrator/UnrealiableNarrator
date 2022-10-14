using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionButton : MonoBehaviour
{
    public int PositionIndex = 0;
    private GameObject _buttonObj;
    private TextMeshProUGUI _textObj;
    public InteractionPanel InteractionPanel;
    public InteractionData InteractionData;
    private float expectedPositionY;
    
    // Start is called before the first frame update
    void Start()
    {
        this._buttonObj = this.GetComponentInChildren<Button>().gameObject;
        this._textObj = this.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
        if (this.expectedPositionY == anchoredPosition.y)
        {
            // okay, do nothing
        } else if (Math.Abs(this.expectedPositionY - anchoredPosition.y) <= 0.1f)
        {
            this.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, this.expectedPositionY);
        } else if (anchoredPosition.y < this.expectedPositionY)
        {
            this.GetComponent<RectTransform>().transform.Translate(0, 160f * Time.deltaTime, 0);
        } else if (anchoredPosition.y > this.expectedPositionY)
        {
            this.GetComponent<RectTransform>().transform.Translate(0, -160f * Time.deltaTime, 0);
        }
    }

    public void SetInteractionData(InteractionData interactionDataData)
    {
        this.InteractionData = interactionDataData;
    }

    public void SetPositionIndex(int index, bool useAnimation = false)
    {
        this.PositionIndex = index;

        // float height = this.GetComponent<RectTransform>().anchoredPosition.y;
        // Debug.Log("Height: " + height);
        
        

        if (!useAnimation) this.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 70 * index);
        this.expectedPositionY = 70 * index;
        this.GetComponent<RectTransform>().offsetMin = new Vector2(0, this.GetComponent<RectTransform>().offsetMin.y);
        this.GetComponent<RectTransform>().offsetMax = new Vector2(-0, this.GetComponent<RectTransform>().offsetMax.y);
    }

    public void SetText(string text)
    {
        if (!this._textObj) this._textObj = this.GetComponentInChildren<TextMeshProUGUI>();
        this._textObj.SetText(text);
    }
    
    public void SetColor(Color color)
    {
        if (!this._buttonObj) this._buttonObj = this.GetComponentInChildren<Button>().gameObject;
        this._buttonObj.GetComponent<Image>().color = color;
    }
    
    public void SetFontColor(Color color)
    {
        this._textObj.color = color;
    }

    public void OnClick()
    {
        this.InteractionData.Action();
        this.Out();
    }

    public void Out()
    {
        this.GetComponent<Animator>().SetTrigger("Out");

        StartCoroutine(this.Delete());
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(1f);
        this.InteractionPanel.InteractionButtonsPresent.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
}
