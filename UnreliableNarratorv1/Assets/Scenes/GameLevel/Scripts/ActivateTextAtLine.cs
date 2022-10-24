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

	
	void OnTriggerEnter3D(BoxCollider other){
		if(other.name == "Player"){
			theTextManager.ReloadScript(theText);
			theTextManager.currentLine = startLine;
			theTextManager.endAtLine = endLine;
			
			
			if(DestroyWhenActivated){
				Destroy(gameObject);
			}
		}
	}
}
