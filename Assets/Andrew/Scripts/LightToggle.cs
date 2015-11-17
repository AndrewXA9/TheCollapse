using UnityEngine;
using System.Collections;

public class LightToggle : MonoBehaviour {
	
	//fade properties
	public float fadeSpeed = 1;
	public float fadeTarget = 8;
	private float currFade = 0;
	public Light light;
	public bool fadeIn = true;
	
	void Update () {
		//fade in or out
		if(fadeIn){
			if(currFade < fadeTarget){
				currFade += fadeSpeed;
				light.intensity = currFade;
			}
		}
		else{
			currFade -= fadeSpeed;
			light.intensity = currFade;
			if(currFade <= 0){
				this.gameObject.active = false;
			}
		}
	}
	
	//switch to fade out mode
	void FadeOut(){
		//Debug.Log("Okay");
		fadeIn = false;
	}
}
