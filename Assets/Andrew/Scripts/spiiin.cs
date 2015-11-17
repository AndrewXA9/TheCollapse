using UnityEngine;
using System.Collections;

public class spiiin : MonoBehaviour {
	public float speed = 10;
	
	void Update () {
		this.transform.eulerAngles += (Vector3.forward*speed);
	}
}
