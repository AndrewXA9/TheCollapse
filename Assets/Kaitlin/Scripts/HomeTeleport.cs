using UnityEngine;
using System.Collections;

public class HomeTeleport : MonoBehaviour {
	[HideInInspector]
	public static int currentPickUp;
	private static int maxPickUp;

	void Start () {
		maxPickUp = 0;
		currentPickUp = 0;
		GameObject[] pickups = GameObject.FindGameObjectsWithTag("PickUp");
		maxPickUp = pickups.Length;
		Debug.Log("Maxpickups: "+maxPickUp);
	}

	void OnTriggerEnter(Collider collider){
		if((collider.gameObject.tag == "Player") && (currentPickUp >= maxPickUp)){
			Application.LoadLevel("Hub");
		}
	}
	
}
