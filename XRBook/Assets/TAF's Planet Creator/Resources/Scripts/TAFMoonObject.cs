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
 	Contains all essential information of a single moon object.
*/

[System.Serializable]
public class TAFMoonObject : System.Object {

	[HideInInspector]
	public string listName = "Moon";

	[Header("Moon Settings")]
	public string moonName = "New Moon";

	[SerializeField]
	[HideInInspector]
	private GameObject planetCenter;
	[SerializeField]
	[HideInInspector]
	private GameObject moon;


	public enum moonStyle{Simple,Advanced,Custom};
	[HideInInspector]
	public moonStyle moonLayout;

	[HideInInspector]
	public int moonModusIndex = 0;

	public Material moonMaterial;

	public Vector3 moonRotation = new Vector3(0,0,0);
	public Vector3 moonRotationSpeed = new Vector3(0,1,0);
	public float moonDistanceFromPlanet = 10.0f;
	public Vector3 moonPlanetRotation;
	public float moonPlanetRotationSpeed = 10.0f;

	public enum moonRes{High, Low};
	public moonRes moonPolycount = moonRes.Low;
	public float moonSize = 0.2f;

	//--- Simple Material Variables Start ---//
	public Color32 moonSimpleRimColor = new Color32(0,128,255,255);
	public Color32 moonSimpleSpecColor = new Color32(107,204,218,255);
	[Range(0,1)]
	public float moonSimpleSpecPower = 0.75f;
	[Range(0,1)]
	public float moonSimpleGlossPower = 0.3f;
	[Range(0,100)]
	public float moonSimpleRimPower = 1.7f;
	[Range(0,14)]
	public float moonSimpleRimThickness = 11;
	public int moonSimpleTextureIndex = 0;
	public Texture moonSimpleSurfaceTexture;
	//--- Simple Material Variables End ---//

	//--- Advanced Material Variables Start ---//
	public Color32 moonAdvancedPrimaryColor = new Color32(0,54,120,255);
	public Color32 moonAdvancedSecondaryColor = new Color32(194,71,173,255);

	public Color32 moonAdvancedRimColor = new Color32(195,60,94,255);
	public Color32 moonAdvancedSpecColor = new Color32(194,129,203,255);
	[Range(0,1)]
	public float moonAdvancedSpecPower = 0.26f;
	[Range(0,1)]
	public float moonAdvancedGlossPower  = 0.66f;
	[Range(0,100)]
	public float moonAdvancedRimPower = 3.45f;
	[Range(0,14)]
	public float moonAdvancedRimThickness = 7.5f;
	public int moonAdvancedTextureIndex = 0;
	public int moonAdvancedIlluminTextureIndex = 0;
	public Texture moonAdvancedSurfaceTexture;
	public Texture moonAdvancedIlluminTexture;
	public Color32 moonAdvancedIlluminColor = new Color32(33,255,255,255);
	[Range(0,0.2f)]
	public float moonAdvancedDarkSideIllumin = 0.1f;
	//--- Advanced Material Variables End ---//

	//--- Custom Variables Start ---//
	public GameObject moonPrefab;
	public Material customMoonMaterial;
	//--- Custom Variables End ---//

	//Returns the current object for the planet center.
	public GameObject GetPlanetCenter() {
		return planetCenter;
	}

	//Sets the planet center with the given object.
	public void SetPlanetCenter(GameObject center) {
		planetCenter = center;
		planetCenter.name = moonName;
	}

	//Returns the current moon object.
	public GameObject GetMoon() {
		return moon;
	}

	//Sets the moon with the given object.
	public void SetMoon(GameObject moonObject) {
		moon = moonObject;
		moon.name = moonName + " Object";
	}

	//Destroys the moon object and the planet center.
	public void DestroyMoon() {
		Object.DestroyImmediate(moon);
		Object.DestroyImmediate(planetCenter);
	}

	//Resets the current moon variables with the default settings.
	public void resetMoonSetting() {
		moonName = "New Moon";
		moonRotation = new Vector3(0,0,0);
		moonRotationSpeed = new Vector3(0,1,0);
		moonDistanceFromPlanet = 10.0f;
		moonPlanetRotation = new Vector3(0,0,0);
		moonPlanetRotationSpeed = 10.0f;
		moon = null;
		planetCenter = null;
		moonMaterial = null;
		moonSize = 0.2f;
	}
}
