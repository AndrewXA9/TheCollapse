using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public float speed = 10.0f;
	Vector3 lookTarget;
	public GameObject pivot;
	private bool onPlatform = true;
	public GameObject bullet;
	public float fadeTime = 4;
	private float fadeTimer;
	public GameObject fadePlane;
	public GameObject model;
	
	
	void Update (){
	
		if(onPlatform){
			int layerMask = 1 << 8;
			layerMask = ~layerMask;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
	
			if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
			{
				lookTarget = hit.point;
			}
	
			Vector3 lookDelta = (lookTarget+new Vector3(0,this.transform.position.y,0)) - this.transform.position;
			//lookDelta = new Vector3(lookDelta.x,this.transform.position.y,lookDelta.z);
			Quaternion targetRot = Quaternion.LookRotation (lookDelta);
			pivot.transform.rotation = targetRot;
			
			//FIRE
			if(Input.GetMouseButtonDown(0)){
				//if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, Mathf.Infinity,layerMask)){
				//	hit.collider.gameObject.transform.parent.gameObject.SendMessage("breakTile");
				//}
				GameObject fire = Instantiate(bullet,this.transform.position,Quaternion.identity) as GameObject;
				fire.SendMessage("setDestination",hit.point);
				
			}
		}
		else{
			fadePlane.GetComponent<Renderer>().material.color = new Color(0,0,0,fadeTimer/fadeTime);
			fadeTimer += Time.deltaTime;
			if(fadeTimer > fadeTime){
				Application.LoadLevel(Application.loadedLevel);
			}
		}
		
	}
	

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		
		if((moveHorizontal+moveVertical)!=0f){
			model.GetComponent<Animation>().Play("run");
		}
		else{
			model.GetComponent<Animation>().Play("idle");
		}
		
		if(onPlatform){
			Rigidbody body = this.GetComponent<Rigidbody>();
			Vector3 movement = Camera.main.transform.right*moveHorizontal+(Quaternion.Euler(Vector3.up*270)*Camera.main.transform.right)*moveVertical;
			body.velocity = movement * speed;
			
			int layerMask = 1 << 8;
			layerMask = ~layerMask;
			if(!Physics.CheckSphere(this.transform.position-(Vector3.up),0.25f,layerMask)){
				onPlatform = false;
				body.velocity = body.velocity*0.25f;
			}
		}
		
			
	}
}
