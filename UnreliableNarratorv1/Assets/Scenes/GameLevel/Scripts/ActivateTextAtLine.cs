using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextAtLine : MonoBehaviour
{
	public TextAsset theText;
	public int startLine;
	public int endLine;
	
	public TextManager theTextManager;
	
	public bool DestroyWhenActivated;
	
	
    void Start()
    {
		
        theTextManager = FindObjectOfType<TextManager>();
    }

	
	private void OnTriggerEnter(Collider other){
		if(other.gameObject.name == "Player"){
			SetScript();
		}
	}
	public void SetScript(){
		
			Debug.Log("Triggered by Enemy");
			theTextManager.ReloadScript(theText);
			theTextManager.currentLine = startLine;
			theTextManager.endAtLine = endLine;
			Debug.Log("fuck this shit");
			theTextManager.EnableTextBox2();
			
			if(DestroyWhenActivated){
				Destroy(gameObject);
			}
		
	}
}
