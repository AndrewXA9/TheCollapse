using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 
{
	private GameObject homeTeleport;
	private int pickup = 1;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			collider.gameObject.SendMessage("PickUpParts", pickup, SendMessageOptions.DontRequireReceiver);
			Destroy(this.gameObject);
		}
	}
}
