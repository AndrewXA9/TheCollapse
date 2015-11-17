using UnityEngine;
using System.Collections;

public class Instructions : MonoBehaviour {
	private bool display = false;
	public string message;
	public string message2;
	public string message3;
	
	void Start () {

	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player"){
			display = true;
		}
	}

	void OnTriggerExit(Collider other){
		if(other.gameObject.tag == "Player"){
			display = false;
			Destroy (this.gameObject);
		}
	}

	void OnGUI(){
		if (display == true) {
			GUI.Box(new Rect(100, 100, 500, 40), message);
			GUI.Box(new Rect(100, 140, 500, 40), message2);
			GUI.Box(new Rect(100, 180, 500, 40), message3);
		}
	}
}