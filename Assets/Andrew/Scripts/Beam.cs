using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour {
	
	public float speed = 5f;
	
	private Vector3 destination;
	
	void Update () {
		this.transform.position += this.transform.forward*speed*Time.deltaTime;
	}
	
	void OnTriggerStay(Collider collider){
		if(collider.gameObject.transform.parent != null){
			if(collider.gameObject.transform.parent.gameObject.tag == "Tile"){
				collider.gameObject.transform.parent.gameObject.SendMessage("breakTile");
				Destroy(this.gameObject);
			}
		}
	}
	
	void setDestination(Vector3 _dest){
		destination = _dest;
		this.transform.rotation = Quaternion.LookRotation(destination-this.transform.position);
	}
	
}
