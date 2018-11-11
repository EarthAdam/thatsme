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
#if UNITY_EDITOR
using UnityEditor;
#endif

/*
 Description:
 	Contains the logic for the planet creator.
*/

[ExecuteInEditMode]
public class TAFPlanetCreator : MonoBehaviour {
	#if UNITY_EDITOR
	//--- Planet Settings Start ---//
	public string planetName = "New Planet";

	[Range(0,50)]
	public float cloudMovingSpeed = 2.0f;

	public GameObject planet;
	public Material usedMaterialPlanet;

	public Vector3 planetRotation = new Vector3(0,0,0);
	public Vector3 planetRotationSpeed = new Vector3(0,1,0);

	public enum planetStyle{Planet,Earthlike,Moon,Simple,Custom};
	public planetStyle planetLayout;
	public int modusIndex;
	public enum planetRes{High, Low};
	public planetRes planetPolycount;
	public float planetSize = 1.0f;
	private GameObject newPlanet;
	public GameObject planetPrefab;
	public Material customPlanetMaterial;

	string[] tempTextures;

	public bool validateAgain = false;

	//--- Foreign Planet Material Variables Start ---//
	public Color32 foreignPrimaryColor = new Color32(31,87,199,255);
	public Color32 foreignSecondaryColor = new Color32(231,137,19,255);
	public Color32 foreignAtmosphereColor = new Color32(234,232,195,255);

	public Color32 foreignRimColor = new Color32(101,101,52,255);
	public Color32 foreignSpecColor = new Color32(72,143,171,255);
	[Range(0,1)]
	public float foreignSpecPower = 0.75f;
	[Range(0,1)]
	public float foreignGlossPower  = 0.3f;
	[Range(0,100)]
	public float foreignRimPower = 3.45f;
	[Range(0,14)]
	public float foreignRimThickness = 9;
	public int foreignTextureIndex = 0;
	public int foreignCloudTextureIndex = 1;
	public int foreignIlluminTextureIndex = 1;
	public Texture foreignSurfaceTexture;
	public Texture foreignCloudTexture;
	public Color32 foreignCloudColor = new Color32(221,221,221,255);
	public Texture foreignIlluminTexture;
	public Color32 foreignIlluminColor = new Color32(255,203,124,255);
	[Range(0,0.2f)]
	public float foreignDarkSideIllumin = 0.1f;
	//--- Foreign Planet Material Variables End ---//

	//--- Earthlike Planet Material Variables Start ---//
	public Color32 earthlikeLandColor = new Color32(158,178,31,255);
	public Color32 earthlikeWaterColor = new Color32(0,128,255,255);
	public Color32 earthlikeIceColor = new Color32(173,238,255,255);
	public Color32 earthlikeAtmosphereColor = new Color32(234,232,195,255);

	public Color32 earthlikeRimColor = new Color32(0,202,255,255);
	public Color32 earthlikeSpecColor = new Color32(72,143,171,255);
	[Range(0,1)]
	public float earthlikeSpecPower = 0.75f;
	[Range(0,1)]
	public float earthlikeGlossPower  = 0.3f;
	[Range(0,100)]
	public float earthlikeRimPower = 1.7f;
	[Range(0,14)]
	public float earthlikeRimThickness = 9;
	public int earthlikeTextureIndex = 0;
	public int earthlikeCloudTextureIndex = 1;
	public int earthlikeIlluminTextureIndex = 1;
	public Texture earthlikeSurfaceTexture;
	public Texture earthlikeCloudTexture;
	public Color32 earthlikeCloudColor = new Color32(255,255,255,255);
	public Texture earthlikeIlluminTexture;
	public Color32 earthlikeIlluminColor = new Color32(255,203,124,255);
	[Range(0,0.2f)]
	public float earthlikeDarkSideIllumin = 0.05f;
	//--- Foreign Planet Material Variables End ---//

	//--- mainMoon Planet Material Variables Start ---//
	public Color32 mainMoonPrimaryColor = new Color32(153,162,178,255);
	public Color32 mainMoonSecondaryColor = new Color32(118,69,8,255);

	public Color32 mainMoonRimColor = new Color32(114,95,70,255);
	public Color32 mainMoonSpecColor = new Color32(153,162,178,255);
	[Range(0,1)]
	public float mainMoonSpecPower = 0.26f;
	[Range(0,1)]
	public float mainMoonGlossPower  = 0.66f;
	[Range(0,100)]
	public float mainMoonRimPower = 3.45f;
	[Range(0,14)]
	public float mainMoonRimThickness = 7.5f;
	public int mainMoonTextureIndex = 0;
	public int mainMoonIlluminTextureIndex = 0;
	public Texture mainMoonSurfaceTexture;
	public Texture mainMoonIlluminTexture;
	public Color32 mainMoonIlluminColor = new Color32(255,203,124,255);
	[Range(0,0.2f)]
	public float mainMoonDarkSideIllumin = 0.05f;
	//--- mainMoon Planet Material Variables End ---//

	//--- Simple Material Variables Start ---//
	public Color32 simpleRimColor = new Color32(0,128,255,255);
	public Color32 simpleSpecColor = new Color32(107,204,218,255);
	[Range(0,1)]
	public float simpleSpecPower = 0.75f;
	[Range(0,1)]
	public float simpleGlossPower = 0.3f;
	[Range(0,100)]
	public float simpleRimPower = 1.7f;
	[Range(0,14)]
	public float simpleRimThickness = 11;
	public int simpleTextureIndex = 0;
	public Texture simpleSurfaceTexture;
	//--- Simple Material Variables End ---//
	//--- Planet Settings End ---//

	//--- Ring Settings Start ---//
	[Header("Ring Settings")]
	public Vector3 ringRotation = new Vector3(0,0,0);

	public GameObject ring;

	public int ringModusIndex;

	public float ringSize = 1.0f;
	public Vector3 ringRotationSpeed = new Vector3(0,5,0);

	public enum ringStyle{Simple,Advanced,Custom};
	public ringStyle ringLayout;

	public enum ringRes{High, Low};
	public ringRes ringPolycount;
	private GameObject newRing;
	private Material usedMaterialRing;

	//--- Ring Material Variables Start ---//
	public Texture ringSimpleSurfaceTexture;
	public int ringSimpleTextureIndex = 0;

	public Texture ringAdvancedSurfaceTexture;
	public int ringAdvancedTextureIndex = 0;
	public Color32 ringAdvancedColor1 = new Color32(189,88,251,255);
	public Color32 ringAdvancedColor2 = new Color32(28,212,129,255);
	[Range(0,1)]
	public float ringAdvancedEmissionPower = 0.5f;
	[Range(0,1)]
	public float ringAdvancedAlpha = 1.0f;

	public GameObject ringPrefab;
	public Material customRingMaterial;
	//--- Ring Material Variables End ---//
	//--- Ring Settings End ---//

	//--- Moon Settings Start ---//
	[Header("Moons")]
	TAFMoonObject emptyMoon;
	public TAFMoonObject[] moons;
	public TAFMoonObject[] tempMoons;

	private GameObject newMoon;
	private Material usedMaterialMoon;
	//--- Moon Settings End ---//

	//--- Shader Varibales Start ---//
	private Shader earthlikeShader;
	private Shader planetShader;
	private Shader ringSimpleShader;
	private Shader ringAdvancedShader;
	private Shader simpleShader;
	private Shader moonShader;
	//--- Shader Varibales End ---//

	// Start Setup
	[ExecuteInEditMode]
	void Start () {
		//--- Define Shaders Start ---//
		earthlikeShader = Shader.Find("TAF's Planet Shader/Planet - Earthlike");
		planetShader = Shader.Find("TAF's Planet Shader/Planet - Foreign");
		ringSimpleShader = Shader.Find("TAF's Planet Shader/Ring - Simple");
		ringAdvancedShader = Shader.Find("TAF's Planet Shader/Ring - Advanced");
		simpleShader = Shader.Find("TAF's Planet Shader/Planet & Moon - Simple");
		moonShader = Shader.Find("TAF's Planet Shader/Moon - Advanced");
		//--- Define Shaders Start ---//

		//--- Moon Setup Start ---//
		if (moons == null) {
			moons = new TAFMoonObject[0];
		}
		//--- Moon Setup End ---//
	}

	// Execute on every value change in the inspector
	public void OnValidate()
	{
		//--- Main Object Start ---//
		//this.transform.rotation = Quaternion.identity;
		//--- Main Object End ---//

		//--- Planet Value Changes Start ---//
		if (planet != null) {

			//--- Planet Action Commands Start ---//
			SetMovingClouds();
			SetPlanetRotation();
			SetPlanetObject();
			SetPlanetLayout ();
			//--- Planet Action Commands End ---//

			//--- Planet Realtime Overwrite Start ---//
			planet.transform.localScale = new Vector3(planetSize, planetSize, planetSize);
			planet.transform.localEulerAngles = planetRotation;
			this.name = planetName;
			//--- Planet Realtime Overwrite End ---//
		}
		//--- Planet Value Changes End ---//

		//--- Ring Value Changes Start ---//
		if (ring != null) {
			
			//--- Ring Action Commands Start ---//
			SetRingRotation();
			SetRingLayout();
			//--- Ring Action Commands End ---//

			//--- Ring Realtime Overwrite Start ---//
			ring.transform.localEulerAngles = ringRotation;
			ring.transform.localScale = new Vector3(ringSize, ringSize, ringSize);
			//--- Ring Realtime Overwrite End ---//

		}
		//--- Ring Value Changes End ---//

		//--- Moon Value Changes Start ---//
		createMoons();
		if (moons != null) {
			foreach (TAFMoonObject moon in moons) {
				moon.listName = moon.moonName;

				if (moon.GetMoon() != null) {
					//--- Moon Action Commands Start ---//
					SetMoonRotation(moon);
					//--- Moon Action Commands End ---//

					//--- Moon Realtime Overwrite Start ---//
					moon.GetMoon().transform.localScale = new Vector3(moon.moonSize, moon.moonSize, moon.moonSize);
					moon.GetMoon().transform.localPosition = new Vector3(moon.moonDistanceFromPlanet, 0, 0);
					moon.GetMoon().transform.localEulerAngles = moon.moonRotation;
					moon.GetPlanetCenter().transform.localEulerAngles = moon.moonPlanetRotation;
					moon.GetMoon().name = "Moon Object";
					moon.GetPlanetCenter().name = moon.moonName;

					SetMoonLayout(moon);
					//--- Moon Realtime Overwrite End ---//
				}
			}
		}
		//--- Moon Value Changes End ---//
	}

	// Creates a planet
	public void createPlanet() {
		//--- Find Planet Object Start ---//
		if (transform.Find("Planet Object") == null) {
			if (planetPolycount == planetRes.High) {
				planet = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Planet Models/Planet or Moon - HighRes.prefab", typeof(GameObject));
			}
			else {
				planet = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Planet Models/Planet or Moon - LowRes.prefab", typeof(GameObject));
			}
			planet = Instantiate(planet, transform.position, transform.rotation);
			planet.transform.localScale = this.transform.lossyScale;
			planet.transform.parent = this.transform;
			planet.transform.localEulerAngles = planetRotation;
			planet.name = "Planet Object";

		} else {
			planet = transform.Find("Planet Object").gameObject;
		}
		//--- Find Planet Object End ---//

		//--- Get Planet Material Start ---//
		if (planet.GetComponent<Renderer>().sharedMaterial.name == "Default-Material") {
			usedMaterialPlanet = planet.GetComponent<Renderer>().sharedMaterial;
			SetPlanetLayout ();
		}
		//--- Get Planet Material End ---//
	}
		
	// Sets the moving of the cloud texture
	void SetMovingClouds () {
		//--- Moving Clouds Start ---//
		TAFMovingClouds cloudsScript;
		if (planet != null && planet.GetComponent<Renderer>().sharedMaterial != null) {
			usedMaterialPlanet = planet.GetComponent<Renderer>().sharedMaterial;
			if (usedMaterialPlanet.HasProperty("_CloudsMap")) {
				if (planet.gameObject.GetComponent<TAFMovingClouds>() == null) {
					cloudsScript = planet.gameObject.AddComponent<TAFMovingClouds>() as TAFMovingClouds;
				} else {
					cloudsScript = planet.gameObject.GetComponent<TAFMovingClouds>();
				}
				if (cloudsScript != null) {
					cloudsScript.setCloudSpeed(cloudMovingSpeed);
				}
			}
		}
		//--- Moving Clouds End ---//
	}

	// Sets the rotation of the planet
	void SetPlanetRotation () {
		//--- Planet Rotation Start ---//
		TAFRotateObject planetRotateScript;
		if (planet != null) {
			if (planet.gameObject.GetComponent<TAFRotateObject>() == null) {
				planetRotateScript = planet.gameObject.AddComponent<TAFRotateObject>() as TAFRotateObject;
			} else {
				planetRotateScript = planet.gameObject.GetComponent<TAFRotateObject>();
			}
			if (planetRotateScript != null) {
				planetRotateScript.setRotationDirection(planetRotationSpeed);
			}
		}
		//--- Planet Rotation End ---//
	}

	// Adjusts the layout of the planet
	public void SetPlanetLayout () {
		//--- Planet Layout Changes Start ---//
		if (planet != null) {
				switch(planetLayout)
				{
				// Sets Foreign Planet Values
				case planetStyle.Planet: 
					planetShader = Shader.Find("TAF's Planet Shader/Planet - Foreign");
					usedMaterialPlanet = new Material(planetShader);
					usedMaterialPlanet.SetColor ("_PrimaryColor", foreignPrimaryColor);
					usedMaterialPlanet.SetColor ("_SecondaryColor", foreignSecondaryColor);
					usedMaterialPlanet.SetColor ("_AtmosphereColor", foreignAtmosphereColor);
					usedMaterialPlanet.SetColor ("_CloudsColor", foreignCloudColor);
					usedMaterialPlanet.SetColor ("_IlluminColor", foreignIlluminColor);
					usedMaterialPlanet.SetColor ("_RimColor", foreignRimColor);
					usedMaterialPlanet.SetColor ("_SpecColor", foreignSpecColor);
					usedMaterialPlanet.SetFloat ("_SpecPower", foreignSpecPower);
					usedMaterialPlanet.SetFloat ("_GlossPower", foreignGlossPower);
					usedMaterialPlanet.SetFloat ("_RimPower", foreignRimPower);
					usedMaterialPlanet.SetFloat ("_RimThickness", foreignRimThickness);
					tempTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Planet/Foreign"});
					foreignSurfaceTexture = (Texture)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(tempTextures[foreignTextureIndex]), typeof(Texture));
					usedMaterialPlanet.SetTexture ("_Surface", foreignSurfaceTexture);
					if (foreignCloudTextureIndex > 0) {
					tempTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Planet/Clouds"});
						foreignCloudTexture = (Texture)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(tempTextures[foreignCloudTextureIndex - 1]), typeof(Texture));
					}
					else {
						foreignCloudTexture = null;
					}
					usedMaterialPlanet.SetTexture ("_CloudsMap", foreignCloudTexture);
					if (foreignIlluminTextureIndex > 0) {
					tempTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Planet/Illumination"});
						foreignIlluminTexture = (Texture)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(tempTextures[foreignIlluminTextureIndex - 1]), typeof(Texture));
					}
					else {
						foreignIlluminTexture = null;
					}
					usedMaterialPlanet.SetTexture ("_IlluminMap", foreignIlluminTexture);
					usedMaterialPlanet.SetFloat("_ShadowSideIntensity", foreignDarkSideIllumin);
					break;
				// Sets Earthlike Planet Values
				case planetStyle.Earthlike:
					earthlikeShader = Shader.Find("TAF's Planet Shader/Planet - Earthlike");
					usedMaterialPlanet = new Material(earthlikeShader);
					usedMaterialPlanet.SetColor ("_LandColor", earthlikeLandColor);
					usedMaterialPlanet.SetColor ("_WaterColor", earthlikeWaterColor);
					usedMaterialPlanet.SetColor ("_IceColor", earthlikeIceColor);
					usedMaterialPlanet.SetColor ("_AtmosphereColor", earthlikeAtmosphereColor);
					usedMaterialPlanet.SetColor ("_CloudsColor", earthlikeCloudColor);
					usedMaterialPlanet.SetColor ("_IlluminColor", earthlikeIlluminColor);
					usedMaterialPlanet.SetColor ("_RimColor", earthlikeRimColor);
					usedMaterialPlanet.SetColor ("_SpecColor", earthlikeSpecColor);
					usedMaterialPlanet.SetFloat ("_SpecPower", earthlikeSpecPower);
					usedMaterialPlanet.SetFloat ("_GlossPower", earthlikeGlossPower);
					usedMaterialPlanet.SetFloat ("_RimPower", earthlikeRimPower);
					usedMaterialPlanet.SetFloat ("_RimThickness", earthlikeRimThickness);
					tempTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Planet/Earthlike"});
					earthlikeSurfaceTexture = (Texture)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(tempTextures[earthlikeTextureIndex]), typeof(Texture));
					usedMaterialPlanet.SetTexture ("_SurfaceMap", earthlikeSurfaceTexture);
					if (earthlikeCloudTextureIndex > 0) {
					tempTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Planet/Clouds"});
						earthlikeCloudTexture = (Texture)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(tempTextures[earthlikeCloudTextureIndex - 1]), typeof(Texture));
					}
					else {
						earthlikeCloudTexture = null;
					}
					usedMaterialPlanet.SetTexture ("_CloudsMap", earthlikeCloudTexture);
					if (earthlikeIlluminTextureIndex > 0) {
					tempTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Planet/Illumination"});
						earthlikeIlluminTexture = (Texture)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(tempTextures[earthlikeIlluminTextureIndex - 1]), typeof(Texture));
					}
					else {
						earthlikeIlluminTexture = null;
					}
					usedMaterialPlanet.SetTexture ("_IlluminMap", earthlikeIlluminTexture);
					usedMaterialPlanet.SetFloat("_ShadowSideIntensity", earthlikeDarkSideIllumin);
					break;
				// Sets Simple Planet Values
				case planetStyle.Simple:
					simpleShader = Shader.Find("TAF's Planet Shader/Planet and Moon - Simple");
					usedMaterialPlanet = new Material (simpleShader);
					usedMaterialPlanet.SetColor ("_RimColor", simpleRimColor);
					usedMaterialPlanet.SetColor ("_SpecColor", simpleSpecColor);
					usedMaterialPlanet.SetFloat ("_SpecPower", simpleSpecPower);
					usedMaterialPlanet.SetFloat ("_GlossPower", simpleGlossPower);
					usedMaterialPlanet.SetFloat ("_RimPower", simpleRimPower);
					usedMaterialPlanet.SetFloat ("_RimThickness", simpleRimThickness);
					tempTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Planet/Simple"});
					simpleSurfaceTexture = (Texture)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(tempTextures[simpleTextureIndex]), typeof(Texture));
					usedMaterialPlanet.SetTexture ("_Surface", simpleSurfaceTexture);
					break;
				// Sets Moon Planet Values
				case planetStyle.Moon:
					moonShader = Shader.Find("TAF's Planet Shader/Moon - Advanced");
					usedMaterialPlanet = new Material(moonShader);
					usedMaterialPlanet.SetColor ("_PrimaryColor", mainMoonPrimaryColor);
					usedMaterialPlanet.SetColor ("_SecondaryColor", mainMoonSecondaryColor);
					usedMaterialPlanet.SetColor ("_IlluminColor", mainMoonIlluminColor);
					usedMaterialPlanet.SetColor ("_RimColor", mainMoonRimColor);
					usedMaterialPlanet.SetColor ("_SpecColor", mainMoonSpecColor);
					usedMaterialPlanet.SetFloat ("_SpecPower", mainMoonSpecPower);
					usedMaterialPlanet.SetFloat ("_GlossPower", mainMoonGlossPower);
					usedMaterialPlanet.SetFloat ("_RimPower", mainMoonRimPower);
					usedMaterialPlanet.SetFloat ("_RimThickness", mainMoonRimThickness);
					tempTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Moon/Advanced"});
					mainMoonSurfaceTexture = (Texture)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(tempTextures[mainMoonTextureIndex]), typeof(Texture));
					usedMaterialPlanet.SetTexture ("_Surface", mainMoonSurfaceTexture);
					if (mainMoonIlluminTextureIndex > 0) {
						tempTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Planet/Illumination"});
						mainMoonIlluminTexture = (Texture)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(tempTextures[mainMoonIlluminTextureIndex - 1]), typeof(Texture));
					}
					else {
						mainMoonIlluminTexture = null;
					}
					usedMaterialPlanet.SetTexture ("_IlluminMap", mainMoonIlluminTexture);
					usedMaterialPlanet.SetFloat("_ShadowSideIntensity", mainMoonDarkSideIllumin);
					break;
				// Sets Custom Planet Values
				case planetStyle.Custom:
				planetShader = Shader.Find("TAF's Planet Shader/Planet - Foreign");
					if (planetPrefab != null && planetPrefab.GetComponent<MeshFilter>() != null) {
						planet.GetComponent<MeshFilter>().sharedMesh = planetPrefab.GetComponent<MeshFilter>().sharedMesh;
						if (planetPrefab.GetComponent<Renderer>().sharedMaterial != null) {
							usedMaterialPlanet = planetPrefab.GetComponent<Renderer>().sharedMaterial;
						}
						else {
							usedMaterialPlanet = new Material(planetShader);
						}
					}else {
						usedMaterialPlanet = new Material(planetShader);
						if (planetPolycount == planetRes.High) {
							newPlanet = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Planet Models/Planet or Moon - HighRes.prefab", typeof(GameObject));
							if (newPlanet != null) {
								planet.GetComponent<MeshFilter>().sharedMesh = newPlanet.GetComponent<MeshFilter>().sharedMesh;
							}
						}
						else {
							newPlanet = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Planet Models/Planet or Moon - LowRes.prefab", typeof(GameObject));
							if (newPlanet != null) {
								planet.GetComponent<MeshFilter>().sharedMesh = newPlanet.GetComponent<MeshFilter>().sharedMesh;
							}
						}
					}
					if (customPlanetMaterial != null) {
						usedMaterialPlanet = customPlanetMaterial;
					}
					break;
				// Sets Default Planet Values
				default:
					usedMaterialPlanet = new Material(planetShader);
					break;
				}
			}
			
			planet.GetComponent<Renderer>().sharedMaterial = usedMaterialPlanet;
			if (planetLayout != planetStyle.Custom) {
				planet.GetComponent<Renderer>().sharedMaterial.name = planetName + " Material";
			}
			planet.transform.localEulerAngles = planetRotation;


			//--- Planet Layout Changes End ---//
	}

	// Switches between lowres und highres planet object
	void SetPlanetObject() {
		if (planet != null) {
			if (planetPolycount == planetRes.High) {
				newPlanet = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Planet Models/Planet or Moon - HighRes.prefab", typeof(GameObject));
				if (newPlanet != null) {
					planet.GetComponent<MeshFilter>().sharedMesh = newPlanet.GetComponent<MeshFilter>().sharedMesh;
				}
			}
			else {
				newPlanet = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Planet Models/Planet or Moon - LowRes.prefab", typeof(GameObject));
				if (newPlanet != null) {
					planet.GetComponent<MeshFilter>().sharedMesh = newPlanet.GetComponent<MeshFilter>().sharedMesh;
				}
			}
		}
	}

	// Creates a ring
	public void createRing() {
		//--- Find Ring Object Start ---//
		if (transform.Find("Ring Object") == null) {
			if (ringPolycount == ringRes.High) {
				ring = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Planet Models/Planet_Ring.prefab", typeof(GameObject));
			}
			else {
				ring = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Planet Models/Planet_Ring LowRes.prefab", typeof(GameObject));
			}
			ring = Instantiate(ring, transform.position, transform.rotation);
			ring.transform.localScale = this.transform.lossyScale;
			ring.transform.parent = this.transform;
			ring.name = "Ring Object";
			ring.transform.localEulerAngles = ringRotation;
		} else {
			ring = transform.Find("Ring Object").gameObject;
		}
		//--- Find Ring Object End ---//

		//--- Get Ring Material Start ---//
		if (ring.GetComponent<Renderer>().sharedMaterial.name == "Default-Material") {
			usedMaterialRing = ring.GetComponent<Renderer>().sharedMaterial;
			SetRingLayout();
		}
		//--- Get Ring Material End ---//
	}

	// Deletes the ring
	public void deleteRing() {
		DestroyImmediate(ring);
	}

	// Sets the rotation of the ring
	void SetRingRotation () {
		//--- Ring Rotation Start ---//
		TAFRotateObject ringRotateScript;
		if (ring != null) {
			if (ring.gameObject.GetComponent<TAFRotateObject>() == null) {
				ringRotateScript = ring.gameObject.AddComponent<TAFRotateObject>() as TAFRotateObject;
			} else {
				ringRotateScript = ring.gameObject.GetComponent<TAFRotateObject>();
			}
			if (ringRotateScript != null) {
				ringRotateScript.setRotationDirection(ringRotationSpeed);
			}
		}
		//--- Ring Rotation End ---//
	}

	// Defines the layout for the ring
	public void SetRingLayout () {
		//--- Ring Layout Changes Start ---//
		if (ring != null) {
			switch(ringLayout)
			{
			// Sets Simple Ring Values
			case ringStyle.Simple: 
				ringSimpleShader = Shader.Find("TAF's Planet Shader/Ring - Simple");
				usedMaterialRing = new Material(ringSimpleShader);
				tempTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Ring/Simple"});
				ringSimpleSurfaceTexture = (Texture)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(tempTextures[ringSimpleTextureIndex]), typeof(Texture));
				usedMaterialRing.SetTexture ("_Surface", ringSimpleSurfaceTexture);
				break;
			// Sets Advanced Ring Values
			case ringStyle.Advanced:
				ringAdvancedShader = Shader.Find("TAF's Planet Shader/Ring - Advanced");
				usedMaterialRing = new Material(ringAdvancedShader);
				usedMaterialRing.SetColor ("_Color1", ringAdvancedColor1);
				usedMaterialRing.SetColor ("_Color2", ringAdvancedColor2);
				usedMaterialRing.SetFloat ("_EmissionPower", ringAdvancedEmissionPower);
				usedMaterialRing.SetFloat ("_Alpha", ringAdvancedAlpha);
				tempTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Ring/Advanced"});
				ringAdvancedSurfaceTexture = (Texture)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(tempTextures[ringAdvancedTextureIndex]), typeof(Texture));
				usedMaterialRing.SetTexture ("_Surface", ringAdvancedSurfaceTexture);
				break;
			// Sets Custom Ring Values
			case ringStyle.Custom:
				if (ringPrefab != null && ringPrefab.GetComponent<MeshFilter>() != null) {
					ring.GetComponent<MeshFilter>().sharedMesh = ringPrefab.GetComponent<MeshFilter>().sharedMesh;
					if (ringPrefab.GetComponent<Renderer>().sharedMaterial != null) {
						usedMaterialRing = ringPrefab.GetComponent<Renderer>().sharedMaterial;
					}
					else {
						usedMaterialRing = new Material(ringAdvancedShader);
					}
				}else {
					usedMaterialRing = new Material(ringAdvancedShader);
					if (ringPolycount == ringRes.High) {
						newRing = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Planet Models/Planet_Ring.prefab", typeof(GameObject));
						if (newRing != null) {
							ring.GetComponent<MeshFilter>().sharedMesh = newRing.GetComponent<MeshFilter>().sharedMesh;
						}
					}
					else {
						newRing = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Planet Models/Planet_Ring LowRes.prefab", typeof(GameObject));
						if (newRing != null) {
							ring.GetComponent<MeshFilter>().sharedMesh = newRing.GetComponent<MeshFilter>().sharedMesh;
						}
					}

				}
				if (customRingMaterial != null) {
					usedMaterialRing = customRingMaterial;
				}
				break;
			// Sets Default Ring Values
			default:
				usedMaterialRing = new Material(ringAdvancedShader);
				break;
			}
		}

		ring.GetComponent<Renderer>().sharedMaterial = usedMaterialRing;
		if (ringLayout != ringStyle.Custom) {
			ring.GetComponent<Renderer>().sharedMaterial.name = planetName + "Ring Material";
		}
		ring.transform.localEulerAngles = ringRotation;

		//--- Ring Layout Changes End ---//
	}

	// Creates the moons
	public void createMoons() {
		//--- Find Moon Object Start ---//
		if (moons != null) {
			foreach (TAFMoonObject moonObject in moons) {
				if (moonObject.GetPlanetCenter() == null) {
					moonObject.SetPlanetCenter(new GameObject());
					moonObject.GetPlanetCenter().name = moonObject.moonName;
					moonObject.GetPlanetCenter().transform.parent = this.transform;
					moonObject.GetPlanetCenter().transform.localPosition = new Vector3(0,0,0);
				}

				if (moonObject.GetMoon() == null) {
					if (moonObject.moonPolycount == TAFMoonObject.moonRes.High) {
						moonObject.SetMoon((GameObject)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Planet Models/Planet or Moon - HighRes.prefab", typeof(GameObject)));
					}
					else {
						moonObject.SetMoon((GameObject)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Planet Models/Planet or Moon - LowRes.prefab", typeof(GameObject)));
					}
					moonObject.SetMoon(Object.Instantiate(moonObject.GetMoon(), new Vector3(0,0,0),Quaternion.identity));
					moonObject.GetMoon().transform.localScale =  new Vector3(moonObject.moonSize, moonObject.moonSize, moonObject.moonSize);
					moonObject.GetMoon().transform.parent = moonObject.GetPlanetCenter().transform;
					moonObject.GetMoon().transform.localEulerAngles = moonObject.moonRotation;
					moonObject.GetPlanetCenter().transform.localEulerAngles = moonObject.moonPlanetRotation;
					moonObject.GetMoon().transform.localPosition = new Vector3( moonObject.moonDistanceFromPlanet, 0, 0);
					moonObject.GetMoon().name = "Moon Object";
					moonObject.GetPlanetCenter().name = moonObject.moonName;
				}
				//--- Find Moon Object End ---//

				//--- Get Moon Material Start ---//
				if (moonObject.moonMaterial == null && moonObject.GetMoon().GetComponent<Renderer>().sharedMaterial != null) {
					if (moonObject.GetMoon().GetComponent<Renderer>().sharedMaterial.name == "Default-Material") {
						usedMaterialMoon = new Material(moonShader);
						moonObject.GetMoon().GetComponent<Renderer>().sharedMaterial = usedMaterialMoon;
					}
					else {
						//Übernehme Inspector-Einstellungen
					}
				} 
				else {
					usedMaterialMoon = moonObject.moonMaterial;
					moonObject.GetMoon().GetComponent<Renderer>().sharedMaterial = usedMaterialMoon;
				}
				//--- Get Moon Material End ---//
			}
		}
	}

	// Add a new moon
	public void addNewMoon() {
		tempMoons = new TAFMoonObject[moons.Length + 1];
		int i;
		for (i = 0; i < moons.Length; i++) {
			tempMoons[i] = moons[i];
		}
		moons = tempMoons;
		moons[moons.Length -1] = new TAFMoonObject();
		moons[moons.Length -1].resetMoonSetting();
		createMoons();
	}

	// Delete a specific moon
	public void deleteMoon(int j) {
		moons[j].DestroyMoon();

		tempMoons = new TAFMoonObject[moons.Length - 1];
		int i;
		for (i = 0; i < j; i++) {
			tempMoons[i] = moons[i];
		}
		for (i = j + 1; i < moons.Length; i++) {
			tempMoons[i - 1] = moons[i];
		}
		moons = tempMoons;
	}

	// Set the layout of a specific moon
	public void SetMoonLayout (TAFMoonObject moon) {
		//--- Moon Layout Changes Start ---//
		if (moon != null) {
			switch(moon.moonLayout)
			{
			// Sets Simple Moon Values
			case TAFMoonObject.moonStyle.Simple:
				simpleShader = Shader.Find("TAF's Planet Shader/Planet and Moon - Simple");
				usedMaterialMoon = new Material (simpleShader);
				usedMaterialMoon.SetColor ("_RimColor", moon.moonSimpleRimColor);
				usedMaterialMoon.SetColor ("_SpecColor", moon.moonSimpleSpecColor);
				usedMaterialMoon.SetFloat ("_SpecPower", moon.moonSimpleSpecPower);
				usedMaterialMoon.SetFloat ("_GlossPower", moon.moonSimpleGlossPower);
				usedMaterialMoon.SetFloat ("_RimPower", moon.moonSimpleRimPower);
				usedMaterialMoon.SetFloat ("_RimThickness", moon.moonSimpleRimThickness);
				tempTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Moon/Simple"});
				moon.moonSimpleSurfaceTexture = (Texture)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(tempTextures[moon.moonSimpleTextureIndex]), typeof(Texture));
				usedMaterialMoon.SetTexture ("_Surface", moon.moonSimpleSurfaceTexture);
				break;
			// Sets Advanced Moon Values
			case TAFMoonObject.moonStyle.Advanced:
				moonShader = Shader.Find("TAF's Planet Shader/Moon - Advanced");
				usedMaterialMoon = new Material(moonShader);
				usedMaterialMoon.SetColor ("_PrimaryColor", moon.moonAdvancedPrimaryColor);
				usedMaterialMoon.SetColor ("_SecondaryColor", moon.moonAdvancedSecondaryColor);
				usedMaterialMoon.SetColor ("_IlluminColor", moon.moonAdvancedIlluminColor);
				usedMaterialMoon.SetColor ("_RimColor", moon.moonAdvancedRimColor);
				usedMaterialMoon.SetColor ("_SpecColor", moon.moonAdvancedSpecColor);
				usedMaterialMoon.SetFloat ("_SpecPower", moon.moonAdvancedSpecPower);
				usedMaterialMoon.SetFloat ("_GlossPower", moon.moonAdvancedGlossPower);
				usedMaterialMoon.SetFloat ("_RimPower", moon.moonAdvancedRimPower);
				usedMaterialMoon.SetFloat ("_RimThickness", moon.moonAdvancedRimThickness);
				tempTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Moon/Advanced"});
				moon.moonAdvancedSurfaceTexture = (Texture)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(tempTextures[moon.moonAdvancedTextureIndex]), typeof(Texture));
				usedMaterialMoon.SetTexture ("_Surface", moon.moonAdvancedSurfaceTexture);
				if (moon.moonAdvancedIlluminTextureIndex > 0) {
					tempTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Planet/Illumination"});
					moon.moonAdvancedIlluminTexture = (Texture)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(tempTextures[moon.moonAdvancedIlluminTextureIndex - 1]), typeof(Texture));
				}
				else {
					moon.moonAdvancedIlluminTexture = null;
				}
				usedMaterialMoon.SetTexture ("_IlluminMap", moon.moonAdvancedIlluminTexture);
				usedMaterialMoon.SetFloat("_ShadowSideIntensity", moon.moonAdvancedDarkSideIllumin);
				break;
			// Sets Custom Moon Values
			case TAFMoonObject.moonStyle.Custom:
				if (moon.moonPrefab != null && moon.moonPrefab.GetComponent<MeshFilter>() != null) {
					moon.GetMoon().GetComponent<MeshFilter>().sharedMesh = moon.moonPrefab.GetComponent<MeshFilter>().sharedMesh;
					if (moon.moonPrefab.GetComponent<Renderer>().sharedMaterial != null) {
						usedMaterialMoon = moon.moonPrefab.GetComponent<Renderer>().sharedMaterial;
					}
					else {
						usedMaterialMoon = new Material(moonShader);
					}
				}else {
					usedMaterialMoon = new Material(moonShader);
				}
				if (moon.customMoonMaterial != null) {
					usedMaterialMoon = moon.customMoonMaterial;
				}
				break;
			// Sets Custom Moon Values
			default:
				usedMaterialMoon = new Material(moonShader);
				break;
			}
		}

		moon.GetMoon().GetComponent<Renderer>().sharedMaterial = usedMaterialMoon;
		if (moon.moonLayout != TAFMoonObject.moonStyle.Custom) {
			moon.GetMoon().GetComponent<Renderer>().sharedMaterial.name = moon.moonName + " Material";
		}
		moon.GetMoon().transform.localEulerAngles = moon.moonRotation;

		if (moon.moonPolycount == TAFMoonObject.moonRes.High) {
			newMoon = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Planet Models/Planet or Moon - HighRes.prefab", typeof(GameObject));
			if (newMoon != null) {
				moon.GetMoon().GetComponent<MeshFilter>().sharedMesh = newMoon.GetComponent<MeshFilter>().sharedMesh;
			}
		}
		else {
			newMoon = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Planet Models/Planet or Moon - LowRes.prefab", typeof(GameObject));
			if (newMoon != null) {
				moon.GetMoon().GetComponent<MeshFilter>().sharedMesh = newMoon.GetComponent<MeshFilter>().sharedMesh;
			}
		}

		//--- Moon Layout Changes End ---//
	}

	// Sets the rotation of a moon
	void SetMoonRotation (TAFMoonObject moonObject) {
		//--- Moon SelfRotation Start ---//
		TAFRotateObject moonRotateScript;
		if (moonObject.GetMoon() != null) {
			if (moonObject.GetMoon().gameObject.GetComponent<TAFRotateObject>() == null) {
				moonRotateScript = moonObject.GetMoon().gameObject.AddComponent<TAFRotateObject>() as TAFRotateObject;
			} else {
				moonRotateScript = moonObject.GetMoon().gameObject.GetComponent<TAFRotateObject>();
			}
			if (moonRotateScript != null) {
				moonRotateScript.setRotationDirection(moonObject.moonRotationSpeed);
			}
		}
		//--- Moon SelfRotation End ---//

		//--- Moon PlanetRotation Start ---//
		if (moonObject.GetPlanetCenter() != null) {
			if (moonObject.GetPlanetCenter().gameObject.GetComponent<TAFRotateObject>() == null) {
				moonRotateScript = moonObject.GetPlanetCenter().gameObject.AddComponent<TAFRotateObject>() as TAFRotateObject;
			} else {
				moonRotateScript = moonObject.GetPlanetCenter().gameObject.GetComponent<TAFRotateObject>();
			}
			if (moonRotateScript != null) {
				moonRotateScript.setRotationDirection(Vector3.up * moonObject.moonPlanetRotationSpeed);
			}
		}
		//--- Moon PlanetRotation End ---//
	}

	// Draws the orbit of each moon
	public void OnDrawGizmosSelected() 
	{
		//--- Moon Gizmo Start ---//
		foreach (TAFMoonObject moon in moons) {
			if (moon.GetPlanetCenter() != null) {
				Handles.color = Color.white;
				Handles.DrawWireDisc(moon.GetPlanetCenter().transform.position, moon.GetPlanetCenter().transform.up , moon.moonDistanceFromPlanet);
			}
		}
		//--- Moon Gizmo End ---//
	}
	#endif
}