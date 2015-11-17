using UnityEngine;
using System.Collections;

public class testmove : MonoBehaviour {
	
	public float dist = 5;
	public float speed = 5;
	
	void Update () {
		this.transform.position = new Vector3(0,0,Mathf.Sin(Time.time*speed)*dist);
	}
}
