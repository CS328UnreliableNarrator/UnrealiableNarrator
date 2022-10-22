using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narration : MonoBehaviour
{
	public TextAsset textFile;
	public string[] TextLines;
	
    // Start is called before the first frame update
    void Start()
    {
        if(textFile !=null){
			TextLines = (textFile.text.Split('\n'));
		}
    }

}
