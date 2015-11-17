using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {
	
	//neighboring tiles
	public GameObject[] neighbors;
	
	//can tile be destroyed?
	public bool destructible = true;
	
	[HideInInspector]
	//minimal state machine value
	public int fallState = 0;
	
	//this vector array is for translating 0 through 3 into vector directions
	private Vector3[] directions = new Vector3[]{Vector3.forward,
												 Vector3.left,
												-Vector3.forward,
												-Vector3.left};
	
	//visual properties for falling tiles
	public Vector3 rotationSpeedBase;
	public float rotationSpeedRandom = 5;
	private Quaternion rotationSpeedFinal;
	public float fallSpeed = 1.0f;
	public float fallCoefficient = 1.0f; //incorrect usage of coefficient but whatever
	
	//light source
	public GameObject myLight;
	
	
	void Start () {
		
		//force debug drawing?
		on = true;
		
		//creating random rotation values
		rotationSpeedFinal.eulerAngles = rotationSpeedBase+new Vector3(Random.Range(-rotationSpeedRandom,rotationSpeedRandom),
		                                                               Random.Range(-rotationSpeedRandom,rotationSpeedRandom),
		                                                               Random.Range(-rotationSpeedRandom,rotationSpeedRandom));
		
		//initialize neighbor array
		neighbors = new GameObject[4];
		
		//find neighbors via raycast
		//NOTE this is not safe if there are colliders above a tile. shoulda used layer mask but I didn't
		RaycastHit hit;
		for(int i=0;i<directions.Length;i++){
			if (Physics.Raycast(this.transform.position+(directions[i]*TileManager.gridSize)+Vector3.up,Vector3.down,out hit)){
				neighbors[i] = hit.collider.gameObject.transform.parent.gameObject;
			}
			else{
				neighbors[i] = null;
			}
		}
		
		
	}
	
	public void Update(){
		
		//0 = not falling, 1 = falling, 2 = fell
		if(fallState == 1){
			
			//rotate and drop during fall
			this.transform.position -= Vector3.up*fallSpeed*Time.deltaTime;
			fallSpeed += fallCoefficient;
			this.transform.rotation = this.transform.rotation*rotationSpeedFinal;
			
			if(this.transform.position.y < TileManager.lavaHeight){
				//stop falling
				fallState = 2;
			}
			
		}
		
	}
	
	//for initiating falling, called from Beam.cs or TileManager.cs
	public void breakTile(){
		//check if destructible
		if(destructible){
			//change state, activate light
			fallState = 1;
			myLight.active = true;
			myLight.transform.parent = null;
		}
		//send lighting check to neighbors
		for(int i=0;i<4;i++){
			if(neighbors[i] != null){
				neighbors[i].SendMessage("LightingCheck");
			}
		}
		//check own lighting (y'know, just in case
		this.gameObject.SendMessage("LightingCheck");
		
	}
	
	//if tile is broken and has no neighbors, turn off light
	public void LightingCheck(){
		
		//variable to see if there are neighbors
		int keep = 0;
		for(int i=0;i<4;i++){
			//iterate thru neighbors and check their fall states
			if(neighbors[i] != null){
				if(neighbors[i].GetComponent<Tile>().fallState == 0){
					keep += 1;
				}
			}
		}
		if(keep == 0){
			//if no neighbors, slowly and sadly disable light (see LightToggle.cs)
			myLight.SendMessage("FadeOut",SendMessageOptions.DontRequireReceiver);
			//Debug.Log("sent");
		}
	}
	
	//toggle for debug gizmos
	private bool on = false;
	
	void OnDrawGizmos() {
		
		//draw connections to neighbor tiles
		Gizmos.color = Color.red;
		if (on){
			for(int i=0;i<4;i++){
				if(neighbors[i] != null){
					Gizmos.DrawLine(this.transform.position+Vector3.up,neighbors[i].transform.position);
				}
			}
		}
	}
	
}

