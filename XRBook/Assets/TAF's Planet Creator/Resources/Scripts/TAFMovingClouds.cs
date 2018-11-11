/*
-----------------------------------------------------
            TAF's Planet Creator
           Copyright © 2018 - TAF
           
  https://assetstore.unity.com/publishers/6837
    https://www.facebook.com/TobisAssetForge
-----------------------------------------------------
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Description:
 	Moves the cloud texture if the material contains a "_CloudsMap" texture property.
*/

public class TAFMovingClouds : MonoBehaviour {

	[SerializeField]
	private float cloudMovingSpeed = 0;
	private Renderer rend;

	//Get the component renderer at start.
	void Start() {
		rend = this.GetComponent<Renderer>();
	}

	//Moves the cloud texture along the x-axis with the current moving speed.
	void FixedUpdate() {
		float offset = Time.time * cloudMovingSpeed / 1000;
		if (rend.material.HasProperty("_CloudsMap") && rend.material.GetTexture("_CloudsMap") != null) {
			rend.material.SetTextureOffset("_CloudsMap", new Vector2(offset, 0));
		}
	}

	//Sets the cloud moving speed.
	public void setCloudSpeed(float speed) {
		cloudMovingSpeed = speed;
	}

	//Returns the current cloud moving speed.
	public float getCloudSpeed() {
		return cloudMovingSpeed;
	}
}
