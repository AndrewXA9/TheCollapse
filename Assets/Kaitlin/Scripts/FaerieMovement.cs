using UnityEngine;
using System.Collections;

public class FaerieMovement : MonoBehaviour{
	private Vector3 hitpoint;
	private bool move = false;
	private float speed = 1.0f;
	private GameObject player;
	public float distance;

	void Start (){
		this.transform.parent = null;
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update(){
		
		distance = (this.gameObject.transform.position - player.transform.position).magnitude;
		if(distance > 20.0f){
			this.gameObject.transform.position = player.transform.position + Vector3.up; 
			hitpoint = this.transform.position;
		}

		if(Input.GetKeyDown(KeyCode.Mouse1)){
		
			int layerMask = 1 << 8;
			layerMask = ~layerMask;
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit,Mathf.Infinity,layerMask)){
				hitpoint = hit.point + (Vector3.up * 2);
				move = true;
			}
		}

		if(move){
		
			this.transform.position = Vector3.Lerp(this.transform.position, hitpoint, Time.deltaTime * speed);
			if(this.transform.position == hitpoint){
				move = false;
			}
		}

	}
}
