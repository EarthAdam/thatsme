using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class orbit : MonoBehaviour {

	float timeCounter;
	public float speed;
	public float radius;
  private float scale=0.0002f;
  private float speedScale=0.3f;

  // Use this for initialization
  void Start () {
		timeCounter = 0;
		//speed = 0;
		//radius = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timeCounter += Time.deltaTime * speed* speedScale;

		float x = scale*Mathf.Sin (timeCounter) * radius;
		float y = 4;
		float z = scale * Mathf.Cos (timeCounter) * radius;

		transform.position = new Vector3 (x, y, z);

	}
}
