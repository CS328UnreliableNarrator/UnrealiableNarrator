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
		if(player.didAcceptPrompt){
			currentLine +=1;
			player.didAcceptPrompt = false;
		}
		if(currentLine>endAtLine){
			DisableTextBox();
		}
	}
	
	
	
	public void EnableTextBox(){
		textBox.SetActive(true);
		
		if(StopPlayerMovement){
				player.canMove = false;
				player.didAcceptDelay = 3.0f; //3 seconds
		}
		isActive = true;
	}
	public void EnableTextBox2(){
		textBox.SetActive(true);
		isActive = true;
	}
	
	public void DisableTextBox(){
		textBox.SetActive(false);
		player.canMove = true;
		isActive = false;
	}
	
	public void ReloadScript(TextAsset thetext){
		if(thetext != null ){
			TextLines = new string[1];
			TextLines = (thetext.text.Split('\n'));
		}
	}
}
