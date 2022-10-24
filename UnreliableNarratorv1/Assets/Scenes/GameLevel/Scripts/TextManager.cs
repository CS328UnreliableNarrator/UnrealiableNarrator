using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TextManager : MonoBehaviour
{
	//game objects
	public GameObject textBox;
	public Text Speech;
	//file shit
	public TextAsset textFile;
	public string[] TextLines;
	//player
	public PlayerController player;
	//settings variables
	public int currentLine;
	public int endAtLine;
	
	public bool isActive;
	public bool StopPlayerMovement;
    
    void Start()
    {
		player = FindObjectOfType<PlayerController>();
		
         if(textFile !=null){
			TextLines = (textFile.text.Split('\n'));
		}
		
		if(endAtLine == 0){
			endAtLine = TextLines.Length - 1;
		}
		if(isActive){
			EnableTextBox();
		}
		else{
				DisableTextBox();
		}
    }
    
	void LateUpdate(){
		
		if(!isActive){
			return;
		}
		
		Speech.text = TextLines[currentLine];
		if(Input.GetKeyDown(KeyCode.Return)){
			currentLine +=1;
		}
		if(currentLine>endAtLine){
			DisableTextBox();
		}
	}
	
	
	
	public void EnableTextBox(){
		textBox.SetActive(true);
		
		if(StopPlayerMovement){
				player.canMove = false;
		}
		isActive = true;
	}
	
	public void DisableTextBox(){
		textBox.SetActive(false);
		player.canMove = true;
		isActive = false;
	}
}
