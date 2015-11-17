using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour 
{
	private bool move = true;
	private float speed = 1.5f;
	private GameObject player;
	public Vector3 distance;


	// Use this for initialization
	void Start () 
	{
		this.gameObject.transform.parent = null;
		player = GameObject.FindGameObjectWithTag("Player");
		distance = this.gameObject.transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(move == true)
		{
			this.transform.position = Vector3.Lerp(this.transform.position, player.transform.position + distance, Time.deltaTime * speed);
		}
	}
}
