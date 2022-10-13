using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionButton : MonoBehaviour
{
    public int PositionIndex = 0;
    private Button _buttonObj;
    private TextMeshProUGUI _textObj;
    // Start is called before the first frame update
    void Start()
    {
        this._buttonObj = this.GetComponentInChildren<Button>();
        this._textObj = this.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPositionIndex(int index)
    {
        this.PositionIndex = index;

        float height = this.GetComponent<RectTransform>().anchoredPosition.y;
        Debug.Log("Height: " + height);

        this.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 70 * index);
        this.GetComponent<RectTransform>().offsetMin = new Vector2(0, this.GetComponent<RectTransform>().offsetMin.y);
        this.GetComponent<RectTransform>().offsetMax = new Vector2(-0, this.GetComponent<RectTransform>().offsetMax.y);
    }

    public void SetText(string text)
    {
        this._textObj = this.GetComponentInChildren<TextMeshProUGUI>();
        this._textObj.SetText(text);
    }
}
