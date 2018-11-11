using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class orbit : MonoBehaviour {

	float timeCounter;
	public float speed;
	public float radius;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate(Vector3.up * Time.deltaTime * speed, Space.World);


	}
}
