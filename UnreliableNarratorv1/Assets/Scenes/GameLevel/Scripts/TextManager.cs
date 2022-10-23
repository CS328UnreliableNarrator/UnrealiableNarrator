using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TextManager : MonoBehaviour
{
	public GameObject textBox;
	public TextMeshProUGUI Speech;
	
	public TextAsset textFile;
	public string[] TextLines;
	
	public int currentLine;
	public int endAtLine;
	
	//public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
		//player = FindObjectOfType<PlayerController>();
		
         if(textFile !=null){
			TextLines = (textFile.text.Split('\n'));
		}
    }
    
	void update(){
		Speech.text = TextLines[currentLine].ToString();

		if(Input.GetKeyDown(KeyCode.Return)){
			currentLine += 1;
		}
	}

}
