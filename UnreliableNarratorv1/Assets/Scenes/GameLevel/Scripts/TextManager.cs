using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TextManager : MonoBehaviour
{
	public GameObject textBox;
	public Text Speech;
	
	public TextAsset textFile;
	public string[] TextLines;
	
	public int currentLine;
	public int endAtLine;
	string test = "hi";
	//public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
		//player = FindObjectOfType<PlayerController>();
		
         if(textFile !=null){
			TextLines = (textFile.text.Split('\n'));
		}
		
		if(endAtLine == 0){
			endAtLine = TextLines.Length - 1;
		}
    }
    
	void LateUpdate(){
		
		Speech.text = TextLines[currentLine];
		if(Input.GetKeyDown(KeyCode.Return)){
			currentLine +=1;
		}
		if(currentLine>endAtLine){
			textBox.SetActive(false);
		}
	}

}
