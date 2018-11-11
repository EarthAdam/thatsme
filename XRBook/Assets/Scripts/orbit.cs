using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class orbit : MonoBehaviour {

	float timeCounter;
	public float speed;
	public float radius;
<<<<<<< HEAD

	// Use this for initialization
	void Start () {
		timeCounter = 1;
		//speed = 50;
=======
  private float scale=0.0002f;
  private float speedScale=0.3f;

  // Use this for initialization
  void Start () {
		timeCounter = 0;
		//speed = 0;
>>>>>>> 5ac9f751472eac2188c123011ec48ba2d1d329ed
		//radius = 0;
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
		timeCounter += Time.deltaTime * speed;
		//timeCounter = 100;

		//float x = Mathf.Sin (timeCounter) * radius;
		//float y = 0;
		//float z = Mathf.Cos (timeCounter) * radius;

		//transform.position = new Vector3 (x, y, z);

		///accellerates
		transform.Rotate(Vector3.up * Time.deltaTime * speed, Space.World);
=======
		timeCounter += Time.deltaTime * speed* speedScale;

		float x = scale*Mathf.Sin (timeCounter) * radius;
		float y = 4;
		float z = scale * Mathf.Cos (timeCounter) * radius;

		transform.position = new Vector3 (x, y, z);
>>>>>>> 5ac9f751472eac2188c123011ec48ba2d1d329ed

	}
}
