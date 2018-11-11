using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Use this script by attaching to any rocket. Enter your rocket speed and button you wish to press to launch the rocket.

public class RocketLaunch: MonoBehaviour {

	public int rocketSpeed;                  //You can change this value in the inspector to change the speed of your rockets.
	public string launchButton;              // Enter the keyboard button you wish to press when you launch your rocket.


	void Update()
	{
		if (Input.GetKeyDown(launchButton))
		{

			GetComponent<ConstantForce>().relativeForce = new Vector3(0, 0,rocketSpeed);  // this is for x,y,z

		}
	}


}
