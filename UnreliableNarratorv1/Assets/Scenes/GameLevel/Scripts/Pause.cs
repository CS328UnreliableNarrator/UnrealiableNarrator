using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pause : MonoBehaviour
{
	
	public GameObject Background;
	public GameObject PauseTXT;
	public GameObject ResumeBTN;
	public GameObject QuitBTN;
	
	void Awake(){
		
		PauseTXT.SetActive(false);
		Background.SetActive(false);
		ResumeBTN.SetActive(false);
		QuitBTN.SetActive(false);
		
	}
	
	public void pause(){
		if(Time.timeScale == 1){
			Time.timeScale = 0;
		}	
			PauseTXT.SetActive(true);
			Background.SetActive(true);
			ResumeBTN.SetActive(true);
			QuitBTN.SetActive(true);
		
		
	}
	
	public void Resume(){
		if (Time.timeScale == 0){
			Debug.Log ("high");
			Time.timeScale = 1;
		}
			PauseTXT.SetActive(false);
			Background.SetActive(false);
			ResumeBTN.SetActive(false);
			QuitBTN.SetActive(false);
		
		
	}
	
	public void quit(){
        Application.Quit();
	}

	
}
