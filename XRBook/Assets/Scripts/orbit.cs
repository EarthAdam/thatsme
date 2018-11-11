using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class orbit : MonoBehaviour {

	float timeCounter;
	public float speed;
	public float radius;

	// Use this for initialization
	void Start () {
		timeCounter = 1;
		//speed = 50;
		//radius = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timeCounter += Time.deltaTime * speed;
		//timeCounter = 100;

		//float x = Mathf.Sin (timeCounter) * radius;
		//float y = 0;
		//float z = Mathf.Cos (timeCounter) * radius;

		//transform.position = new Vector3 (x, y, z);

		///accellerates
		transform.Rotate(Vector3.up * Time.deltaTime * speed, Space.World);

	}
}
