using UnityEngine;
using System.Collections;


public class Turret : MonoBehaviour {
		
	public int state = 0;
	public int firestate = 0;
	public float cooldown = 5;
	private float countdown = 0;
	public GameObject light;
	public GameObject reticle;
	private GameObject player;
	public GameObject bullet;
	void Start(){
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void OnTriggerEnter(Collider other){
		if(((other.gameObject.tag == "Player")||(other.gameObject.tag == "Bullet")||(other.gameObject.tag == "Fairy"))&&(state == 0)){
			state = 1;
			light.active = true;
		}
	}
	
	void Update () {
		
		if (state == 1){
			countdown += Time.deltaTime;
			if (countdown >= cooldown){
				countdown = 0;
				if (firestate == 1){
					firestate = 0;
					reticle.transform.position = player.transform.position-(Vector3.up*0.8f);
					Instantiate(bullet,this.transform.position+Vector3.up,Quaternion.Euler(270,0,0));
					Instantiate(bullet,player.transform.position+(Vector3.up*30f),Quaternion.Euler(90,0,0));
				}
				else{
					firestate = 1;
				}
			}
		}
		
	}
	
	void Deactivate(){
		reticle.transform.position = this.transform.position-Vector3.up;
		state = 2;
	}
	
}
