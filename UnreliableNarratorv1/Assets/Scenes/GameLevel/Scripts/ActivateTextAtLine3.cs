using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextAtLine3 : MonoBehaviour
{
	public TextAsset theText;
	public int startLine;
	public int endLine;
	public GameObject trigger;
	public textmanager2 theTextManager;
	
	public bool DestroyWhenActivated;
	public bool test;
	
    void Start()
    {
		
        theTextManager = FindObjectOfType<textmanager2>();
    }
	
	void update(){
		
		test = trigger.activeInHierarchy;
		if (trigger.activeInHierarchy ==true){
			Debug.Log("ACTIVE");
			SetScript();
		}
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
