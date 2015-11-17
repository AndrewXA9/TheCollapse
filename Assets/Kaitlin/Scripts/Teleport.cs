using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			gameObject.SendMessage("Deactivate");
		}
	}

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "Player"){
			if(this.gameObject.tag == "TeleportA"){
				Application.LoadLevel ("Level_A");
			}
			if(this.gameObject.tag == "TeleportB"){
				Application.LoadLevel ("Level_B");
			}
			if(this.gameObject.tag == "TeleportC"){
				Application.LoadLevel ("Level_C");
			}
			/*if(this.gameObject.tag == "Teleport4"){
				//collider.transform.position = target.position;
				//Application.LoadLevel ("");
			}*/
		}
	}

	void Deactivate(){
		Destroy (this.gameObject.GetComponent<BoxCollider>());
	}
}