using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextAtLine2 : MonoBehaviour
{
	public TextAsset theText;
	public int startLine;
	public int endLine;
	public textmanager2 theTextManager;
	
	public bool DestroyWhenActivated;
	
    void Start()
    {
		
        theTextManager = FindObjectOfType<textmanager2>();
    }
	

	
	public void SetScript(){
		
			Debug.Log("Triggered by Enemy2");
			theTextManager.ReloadScript(theText);
			theTextManager.currentLine = startLine;
			theTextManager.endAtLine = endLine;
			Debug.Log("fuck this shit2");
			theTextManager.EnableTextBox2();
			
			if(DestroyWhenActivated){
				Destroy(gameObject);
			}
		
	}
}
