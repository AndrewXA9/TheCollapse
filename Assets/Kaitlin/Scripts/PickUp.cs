using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {
	private GameObject homeTeleport;
	
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player"){
			HomeTeleport.currentPickUp++;
			Debug.Log(HomeTeleport.currentPickUp);
			Destroy(this.gameObject);
		}
	}
}
