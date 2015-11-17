using UnityEngine;
using System.Collections;

public class TextControl : MonoBehaviour {
	public bool isPlayButton = false;
	public bool isQuitButton = false;
	public bool isPlayAgainButton = false;
	
	void OnMouseEnter(){
		GetComponent<Renderer>().material.color = Color.black;
	}
	
	void OnMouseExit(){
		GetComponent<Renderer>().material.color = Color.white;	
	}
	
	void OnMouseUp(){
		if(isPlayButton){
			Application.LoadLevel("Hub");
		}
		
		if(isQuitButton){
			Application.Quit();
		}
		if(isPlayAgainButton){
			Application.LoadLevel ("StartScene");
		}
	}
}
