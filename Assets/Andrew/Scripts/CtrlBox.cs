using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CtrlBox : MonoBehaviour {

	public List<GameObject> turrets;
	private Tile ground;
	
	void Start(){
		int layerMask = 1 << 8;
		layerMask = ~layerMask;
		RaycastHit hit;
		if (Physics.Raycast(this.transform.position+Vector3.up,Vector3.down,out hit,2f,layerMask)){
			ground = hit.collider.transform.parent.GetComponent<Tile>();
		}
	}
	
	void Update () {
		if(ground.fallState != 0){
			foreach(GameObject i in turrets){
				i.gameObject.SendMessage("Deactivate");
				Destroy(this.gameObject);
			}
		}
	}
}
