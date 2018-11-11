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
using UnityEditor;
using System.IO;

/*
 Description:
 	Changes the layout of the TAFPlanetCreator-Script.
*/

[CustomEditor(typeof(TAFPlanetCreator))]
public class TAFPlanetCreatorEditor : Editor {

	TAFPlanetCreator creatorScript;

	string[] tempTextures;

	Texture2D blueChannelTexture;

	//--- Planet Variables Start ---//
	SerializedProperty planetObject;
	SerializedProperty planetName;
	SerializedProperty planetRotation;
	SerializedProperty planetRotationSpeed;
	SerializedProperty planetSize;
	SerializedProperty planetPolycount;
	SerializedProperty planetLayout;

	SerializedProperty cloudMovingSpeed;

	string[] modus;
	SerializedProperty modusIndex;

	bool planetSettingsVisible;

	SerializedProperty simpleRimColor;
	SerializedProperty simpleSpecColor;
	SerializedProperty simpleSpecPower;
	SerializedProperty simpleGlossPower;
	SerializedProperty simpleRimPower;
	SerializedProperty simpleRimThickness;
	SerializedProperty simpleTextureIndex;
	SerializedProperty simpleTexture;
	string[] simpleTextures;
	string[] simpleTexturesNames;
	bool simpleSurfaceVisible = false;
	bool simpleSpecularVisible = false;
	bool simpleRimLightVisible = false;

	SerializedProperty foreignPrimaryColor;
	SerializedProperty foreignSecondaryColor;
	SerializedProperty foreignAtmosphereColor;
	SerializedProperty foreignRimColor;
	SerializedProperty foreignSpecColor;
	SerializedProperty foreignSpecPower;
	SerializedProperty foreignGlossPower;
	SerializedProperty foreignRimPower;
	SerializedProperty foreignRimThickness;
	SerializedProperty foreignTextureIndex;
	SerializedProperty foreignCloudTextureIndex;
	SerializedProperty foreignIlluminTextureIndex;
	SerializedProperty foreignSurfaceTexture;
	SerializedProperty foreignCloudTexture;
	SerializedProperty foreignCloudColor;
	SerializedProperty foreignIlluminTexture;
	SerializedProperty foreignIlluminColor;
	SerializedProperty foreignDarkSideIllumin;
	string[] foreignSurfaceTextures;
	string[] foreignSurfaceTexturesNames;
	string[] foreignCloudTextures;
	string[] foreignCloudTexturesNames;
	string[] foreignIlluminTextures;
	string[] foreignIlluminTexturesNames;
	bool foreignSurfaceVisible = false;
	bool foreignSpecularVisible = false;
	bool foreignCloudsVisible = false;
	bool foreignRimLightVisible = false;
	bool foreignIlluminationVisible = false;

	SerializedProperty earthlikeLandColor;
	SerializedProperty earthlikeWaterColor;
	SerializedProperty earthlikeIceColor;
	SerializedProperty earthlikeAtmosphereColor;
	SerializedProperty earthlikeRimColor;
	SerializedProperty earthlikeSpecColor;
	SerializedProperty earthlikeSpecPower;
	SerializedProperty earthlikeGlossPower;
	SerializedProperty earthlikeRimPower;
	SerializedProperty earthlikeRimThickness;
	SerializedProperty earthlikeTextureIndex;
	SerializedProperty earthlikeCloudTextureIndex;
	SerializedProperty earthlikeIlluminTextureIndex;
	SerializedProperty earthlikeSurfaceTexture;
	SerializedProperty earthlikeCloudTexture;
	SerializedProperty earthlikeCloudColor;
	SerializedProperty earthlikeIlluminTexture;
	SerializedProperty earthlikeIlluminColor;
	SerializedProperty earthlikeDarkSideIllumin;
	string[] earthlikeSurfaceTextures;
	string[] earthlikeSurfaceTexturesNames;
	string[] earthlikeCloudTextures;
	string[] earthlikeCloudTexturesNames;
	string[] earthlikeIlluminTextures;
	string[] earthlikeIlluminTexturesNames;
	bool earthlikeSurfaceVisible = false;
	bool earthlikeSpecularVisible = false;
	bool earthlikeCloudsVisible = false;
	bool earthlikeRimLightVisible = false;
	bool earthlikeIlluminationVisible = false;

	SerializedProperty mainMoonPrimaryColor;
	SerializedProperty mainMoonSecondaryColor;
	SerializedProperty mainMoonRimColor;
	SerializedProperty mainMoonSpecColor;
	SerializedProperty mainMoonSpecPower;
	SerializedProperty mainMoonGlossPower;
	SerializedProperty mainMoonRimPower;
	SerializedProperty mainMoonRimThickness;
	SerializedProperty mainMoonTextureIndex;
	SerializedProperty mainMoonIlluminTextureIndex;
	SerializedProperty mainMoonSurfaceTexture;
	SerializedProperty mainMoonIlluminTexture;
	SerializedProperty mainMoonIlluminColor;
	SerializedProperty mainMoonDarkSideIllumin;
	string[] mainMoonSurfaceTextures;
	string[] mainMoonSurfaceTexturesNames;
	string[] mainMoonIlluminTextures;
	string[] mainMoonIlluminTexturesNames;
	bool mainMoonSurfaceVisible = false;
	bool mainMoonSpecularVisible = false;
	bool mainMoonRimLightVisible = false;
	bool mainMoonIlluminationVisible = false;

	SerializedProperty customPlanet;
	SerializedProperty customPlanetMaterial;
	bool customPrefabVisible = false;
	bool customMaterialVisible = false;
	//--- Planet Variables End ---//

	//--- Ring Variables Start ---//
	SerializedProperty ringRotation;
	SerializedProperty ringObject;
	SerializedProperty ringSize;
	SerializedProperty ringRotationSpeed;
	SerializedProperty ringPolycount;
	SerializedProperty ringLayout;

	string[] ringModus;
	SerializedProperty ringModusIndex;

	bool ringSettingsVisible;

	SerializedProperty ringSimpleSurfaceTexture;
	string[] ringSimpleSurfaceTextures;
	string[] ringSimpleSurfaceTexturesNames;
	SerializedProperty ringSimpleTextureIndex;
	bool ringSimpleSurfaceVisible = false;

	SerializedProperty ringAdvancedSurfaceTexture;
	SerializedProperty ringAdvancedColor1;
	SerializedProperty ringAdvancedColor2;
	SerializedProperty ringAdvancedEmissionPower;
	SerializedProperty ringAdvancedAlpha;
	string[] ringAdvancedSurfaceTextures;
	string[] ringAdvancedSurfaceTexturesNames;
	SerializedProperty ringAdvancedTextureIndex;
	bool ringAdvancedSurfaceVisible = false;
	bool ringAdvancedIntensityVisible = false;

	SerializedProperty customRing;
	SerializedProperty customRingMaterial;
	bool customRingPrefabVisible = false;
	bool customRingMaterialVisible = false;
	//--- Ring Variables End ---//

	//--- Moon Variables Start ---//
		bool moonsSettingsVisible;
	bool[] moonObjectsVisible;
	string[] moonPolycount;
	int moonPolycountIndex;
	string[] moonModus;
	[SerializeField]
	int[] moonModusIndex = new int[4];

	bool moonSimpleSurfaceVisible = false;
	bool moonSimpleSpecularVisible = false;
	bool moonSimpleRimLightVisible = false;
	string[] moonSimpleSurfaceTextures;
	string[] moonSimpleSurfaceTexturesNames;

	bool moonAdvancedSurfaceVisible = false;
	bool moonAdvancedSpecularVisible = false;
	bool moonAdvancedRimLightVisible = false;
	bool moonAdvancedIlluminVisible = false;
	string[] moonAdvancedSurfaceTextures;
	string[] moonAdvancedSurfaceTexturesNames;
	string[] moonAdvancedIlluminTextures;
	string[] moonAdvancedIlluminTexturesNames;

	bool customMoonPrefabVisible = false;
	bool customMoonMaterialVisible = false;
	//--- Moon Variables End ---//

	GUIStyle headlineStyle;

	//Fills variables on start.
	void OnEnable() {

		creatorScript = (TAFPlanetCreator)target;

		#if UNITY_EDITOR
		if (!EditorApplication.isPlaying) {
				foreach (TAFPlanetCreator script in Resources.FindObjectsOfTypeAll(typeof(TAFPlanetCreator)) as TAFPlanetCreator[]) {
					script.OnValidate();
				}
		}
		#endif

		headlineStyle = new GUIStyle ();
		headlineStyle.richText = true;

		//--- Set Planet Variables Start ---//
		planetObject = serializedObject.FindProperty("planet");
		planetName = serializedObject.FindProperty("planetName");
		planetRotation = serializedObject.FindProperty("planetRotation");
		planetRotationSpeed = serializedObject.FindProperty("planetRotationSpeed");
		planetSize = serializedObject.FindProperty("planetSize");
		planetPolycount = serializedObject.FindProperty("planetPolycount");
		planetLayout = serializedObject.FindProperty("planetLayout");
		modusIndex = serializedObject.FindProperty("modusIndex");
		cloudMovingSpeed = serializedObject.FindProperty("cloudMovingSpeed");

		customPlanet = serializedObject.FindProperty("planetPrefab");
		customPlanetMaterial = serializedObject.FindProperty("customPlanetMaterial");

		simpleRimColor = serializedObject.FindProperty("simpleRimColor");
		simpleSpecColor = serializedObject.FindProperty("simpleSpecColor");
		simpleSpecPower = serializedObject.FindProperty("simpleSpecPower");
		simpleGlossPower = serializedObject.FindProperty("simpleGlossPower");
		simpleRimPower = serializedObject.FindProperty("simpleRimPower");
		simpleRimThickness = serializedObject.FindProperty("simpleRimThickness");
		simpleTextureIndex = serializedObject.FindProperty("simpleTextureIndex");
		simpleTexture = serializedObject.FindProperty("simpleSurfaceTexture");

		foreignPrimaryColor = serializedObject.FindProperty("foreignPrimaryColor");
		foreignSecondaryColor = serializedObject.FindProperty("foreignSecondaryColor");
		foreignAtmosphereColor = serializedObject.FindProperty("foreignAtmosphereColor");
		foreignRimColor = serializedObject.FindProperty("foreignRimColor");
		foreignSpecColor = serializedObject.FindProperty("foreignSpecColor");
		foreignSpecPower = serializedObject.FindProperty("foreignSpecPower");
		foreignGlossPower = serializedObject.FindProperty("foreignGlossPower");
		foreignRimPower = serializedObject.FindProperty("foreignRimPower");
		foreignRimThickness = serializedObject.FindProperty("foreignRimThickness");
		foreignTextureIndex = serializedObject.FindProperty("foreignTextureIndex");
		foreignCloudTextureIndex = serializedObject.FindProperty("foreignCloudTextureIndex");
		foreignIlluminTextureIndex = serializedObject.FindProperty("foreignIlluminTextureIndex");
		foreignSurfaceTexture = serializedObject.FindProperty("foreignSurfaceTexture");
		foreignCloudTexture = serializedObject.FindProperty("foreignCloudTexture");
		foreignCloudColor = serializedObject.FindProperty("foreignCloudColor");
		foreignIlluminTexture = serializedObject.FindProperty("foreignIlluminTexture");
		foreignIlluminColor = serializedObject.FindProperty("foreignIlluminColor");
		foreignDarkSideIllumin = serializedObject.FindProperty("foreignDarkSideIllumin");

		earthlikeLandColor = serializedObject.FindProperty("earthlikeLandColor");
		earthlikeWaterColor = serializedObject.FindProperty("earthlikeWaterColor");
		earthlikeIceColor = serializedObject.FindProperty("earthlikeIceColor");
		earthlikeAtmosphereColor = serializedObject.FindProperty("earthlikeAtmosphereColor");
		earthlikeRimColor = serializedObject.FindProperty("earthlikeRimColor");
		earthlikeSpecColor = serializedObject.FindProperty("earthlikeSpecColor");
		earthlikeSpecPower = serializedObject.FindProperty("earthlikeSpecPower");
		earthlikeGlossPower = serializedObject.FindProperty("earthlikeGlossPower");
		earthlikeRimPower = serializedObject.FindProperty("earthlikeRimPower");
		earthlikeRimThickness = serializedObject.FindProperty("earthlikeRimThickness");
		earthlikeTextureIndex = serializedObject.FindProperty("earthlikeTextureIndex");
		earthlikeCloudTextureIndex = serializedObject.FindProperty("earthlikeCloudTextureIndex");
		earthlikeIlluminTextureIndex = serializedObject.FindProperty("earthlikeIlluminTextureIndex");
		earthlikeSurfaceTexture = serializedObject.FindProperty("earthlikeSurfaceTexture");
		earthlikeCloudTexture = serializedObject.FindProperty("earthlikeCloudTexture");
		earthlikeCloudColor = serializedObject.FindProperty("earthlikeCloudColor");
		earthlikeIlluminTexture = serializedObject.FindProperty("earthlikeIlluminTexture");
		earthlikeIlluminColor = serializedObject.FindProperty("earthlikeIlluminColor");
		earthlikeDarkSideIllumin = serializedObject.FindProperty("earthlikeDarkSideIllumin");

		mainMoonPrimaryColor = serializedObject.FindProperty("mainMoonPrimaryColor");
		mainMoonSecondaryColor = serializedObject.FindProperty("mainMoonSecondaryColor");
		mainMoonRimColor = serializedObject.FindProperty("mainMoonRimColor");
		mainMoonSpecColor = serializedObject.FindProperty("mainMoonSpecColor");
		mainMoonSpecPower = serializedObject.FindProperty("mainMoonSpecPower");
		mainMoonGlossPower = serializedObject.FindProperty("mainMoonGlossPower");
		mainMoonRimPower = serializedObject.FindProperty("mainMoonRimPower");
		mainMoonRimThickness = serializedObject.FindProperty("mainMoonRimThickness");
		mainMoonTextureIndex = serializedObject.FindProperty("mainMoonTextureIndex");
		mainMoonIlluminTextureIndex = serializedObject.FindProperty("mainMoonIlluminTextureIndex");
		mainMoonSurfaceTexture = serializedObject.FindProperty("mainMoonSurfaceTexture");
		mainMoonIlluminTexture = serializedObject.FindProperty("mainMoonIlluminTexture");
		mainMoonIlluminColor = serializedObject.FindProperty("mainMoonIlluminColor");
		mainMoonDarkSideIllumin = serializedObject.FindProperty("mainMoonDarkSideIllumin");

		modus = new string[] {"Foreign Planet", "Earthlike", "Moon", "Simple", "Custom"};

		//Find Textures for "Simple" Material
		simpleTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Planet/Simple"});
		simpleTexturesNames = simpleTextures;
		for (int i = 0; i < simpleTextures.Length; i++) {
			simpleTexturesNames [i] = Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath (simpleTextures [i]));
		}

		//Find Textures for "Foreign" Material
		foreignSurfaceTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Planet/Foreign"});
		foreignSurfaceTexturesNames = foreignSurfaceTextures;
		for (int i = 0; i < foreignSurfaceTextures.Length; i++) {
			foreignSurfaceTexturesNames [i] = Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath (foreignSurfaceTextures [i]));
		}
		tempTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Planet/Clouds"});
		foreignCloudTextures = new string[tempTextures.Length + 1];
		foreignCloudTextures[0] = "None";
		foreignCloudTexturesNames = foreignCloudTextures;
		for (int i = 1; i < foreignCloudTextures.Length; i++) {
			foreignCloudTextures[i] = tempTextures[i-1];
			foreignCloudTexturesNames [i] = Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath (foreignCloudTextures [i]));
		}
		tempTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Planet/Illumination"});
		foreignIlluminTextures = new string[tempTextures.Length + 1];
		foreignIlluminTextures[0] = "None";
		foreignIlluminTexturesNames = foreignIlluminTextures;
		for (int i = 1; i < foreignIlluminTextures.Length; i++) {
			foreignIlluminTextures[i] = tempTextures[i-1];
			foreignIlluminTexturesNames [i] = Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath (foreignIlluminTextures [i]));
		}

		//Find Textures for "Earthlike" Material
		earthlikeSurfaceTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Planet/Earthlike"});
		earthlikeSurfaceTexturesNames = earthlikeSurfaceTextures;
		for (int i = 0; i < earthlikeSurfaceTextures.Length; i++) {
			earthlikeSurfaceTexturesNames [i] = Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath (earthlikeSurfaceTextures [i]));
		}
		tempTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Planet/Clouds"});
		earthlikeCloudTextures = new string[tempTextures.Length + 1];
		earthlikeCloudTextures[0] = "None";
		earthlikeCloudTexturesNames = earthlikeCloudTextures;
		for (int i = 1; i < earthlikeCloudTextures.Length; i++) {
			earthlikeCloudTextures[i] = tempTextures[i-1];
			earthlikeCloudTexturesNames [i] = Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath (earthlikeCloudTextures [i]));
		}
		tempTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Planet/Illumination"});
		earthlikeIlluminTextures = new string[tempTextures.Length + 1];
		earthlikeIlluminTextures[0] = "None";
		earthlikeIlluminTexturesNames = earthlikeIlluminTextures;
		for (int i = 1; i < earthlikeIlluminTextures.Length; i++) {
			earthlikeIlluminTextures[i] = tempTextures[i-1];
			earthlikeIlluminTexturesNames [i] = Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath (earthlikeIlluminTextures [i]));
		}

		//Find Textures for "Moon" Material
		mainMoonSurfaceTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Moon/Advanced"});
		mainMoonSurfaceTexturesNames = mainMoonSurfaceTextures;
		for (int i = 0; i < mainMoonSurfaceTextures.Length; i++) {
			mainMoonSurfaceTexturesNames [i] = Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath (mainMoonSurfaceTextures [i]));
		}
		tempTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Planet/Illumination"});
		mainMoonIlluminTextures = new string[tempTextures.Length + 1];
		mainMoonIlluminTextures[0] = "None";
		mainMoonIlluminTexturesNames = mainMoonIlluminTextures;
		for (int i = 1; i < mainMoonIlluminTextures.Length; i++) {
			mainMoonIlluminTextures[i] = tempTextures[i-1];
			mainMoonIlluminTexturesNames [i] = Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath (mainMoonIlluminTextures [i]));
		}
		//--- Set Planet Variables End ---//

		//--- Set Ring Variables Start ---//
		ringRotation = serializedObject.FindProperty("ringRotation");
		ringObject = serializedObject.FindProperty("ring");
		ringSize = serializedObject.FindProperty("ringSize");
		ringRotationSpeed = serializedObject.FindProperty("ringRotationSpeed");
		ringPolycount = serializedObject.FindProperty("ringPolycount");
		ringLayout = serializedObject.FindProperty("ringLayout");

		ringModus  = new string[] {"Simple", "Advanced", "Custom"};
		ringModusIndex = serializedObject.FindProperty("ringModusIndex");

		ringSimpleSurfaceTexture = serializedObject.FindProperty("ringSimpleSurfaceTexture");
		ringSimpleTextureIndex = serializedObject.FindProperty("ringSimpleTextureIndex");

		ringAdvancedSurfaceTexture = serializedObject.FindProperty("ringAdvancedSurfaceTexture");
		ringAdvancedColor1 = serializedObject.FindProperty("ringAdvancedColor1");
		ringAdvancedColor2 = serializedObject.FindProperty("ringAdvancedColor2");
		ringAdvancedEmissionPower = serializedObject.FindProperty("ringAdvancedEmissionPower");
		ringAdvancedAlpha = serializedObject.FindProperty("ringAdvancedAlpha");
		ringAdvancedTextureIndex = serializedObject.FindProperty("ringAdvancedTextureIndex");

		customRing = serializedObject.FindProperty("ringPrefab");
		customRingMaterial = serializedObject.FindProperty("customRingMaterial");

		//Find Textures for "Simple" Material
		ringSimpleSurfaceTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Ring/Simple"});
		ringSimpleSurfaceTexturesNames = ringSimpleSurfaceTextures;
		for (int i = 0; i < ringSimpleSurfaceTextures.Length; i++) {
			ringSimpleSurfaceTexturesNames [i] = Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath (ringSimpleSurfaceTextures [i]));
		}

		//Find Textures for "Advanced" Material
		ringAdvancedSurfaceTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Ring/Advanced"});
		ringAdvancedSurfaceTexturesNames = ringAdvancedSurfaceTextures;
		for (int i = 0; i < ringAdvancedSurfaceTextures.Length; i++) {
			ringAdvancedSurfaceTexturesNames [i] = Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath (ringAdvancedSurfaceTextures [i]));
		}
		//--- Set Ring Variables End ---//

		//--- Set Moon Variables Start ---//
		moonObjectsVisible = new bool[4];
		moonPolycount = new string[] {"High", "Low"};
		moonModus = new string[] {"Simple", "Advanced", "Custom"};

		//Find Textures for "Simple" Material
		moonSimpleSurfaceTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Moon/Simple"});
		moonSimpleSurfaceTexturesNames = moonSimpleSurfaceTextures;
		for (int i = 0; i < moonSimpleSurfaceTextures.Length; i++) {
			moonSimpleSurfaceTexturesNames [i] = Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath (moonSimpleSurfaceTextures [i]));
		}

		//Find Textures for "Advanced" Material
		moonAdvancedSurfaceTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Moon/Advanced"});
		moonAdvancedSurfaceTexturesNames = moonAdvancedSurfaceTextures;
		for (int i = 0; i < moonAdvancedSurfaceTextures.Length; i++) {
			moonAdvancedSurfaceTexturesNames [i] = Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath (moonAdvancedSurfaceTextures [i]));
		}
		tempTextures = AssetDatabase.FindAssets ("t:Texture", new string[] {"Assets/TAF's Planet Creator/Resources/Textures/Planet/Illumination"});
		moonAdvancedIlluminTextures = new string[tempTextures.Length + 1];
		moonAdvancedIlluminTextures[0] = "None";
		moonAdvancedIlluminTexturesNames = moonAdvancedIlluminTextures;
		for (int i = 1; i < moonAdvancedIlluminTextures.Length; i++) {
			moonAdvancedIlluminTextures[i] = tempTextures[i-1];
			moonAdvancedIlluminTexturesNames [i] = Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath (moonAdvancedIlluminTextures [i]));
		}

		//--- Set Planet Variables End ---//
	}

	//Create and update GUI elements
	public override void OnInspectorGUI() {

		serializedObject.Update();

		GUILayout.Space(10);

		//--- Planet GUI Start ---//
		planetSettingsVisible = DrawMainHeader("Planet Settings", planetSettingsVisible);

		if (planetSettingsVisible) {

			if (planetObject.objectReferenceValue != null) {

				//--- Planet General Settings Start ---//
				DrawSubHeader("General");
				BeginContents();

				//Planet Name
				GUILayout.BeginHorizontal();
				planetName.stringValue = EditorGUILayout.TextField("Name", planetName.stringValue);
				GUILayout.EndHorizontal();

				//Planet Size
				GUILayout.BeginHorizontal();
				planetSize.floatValue = EditorGUILayout.FloatField("Size", planetSize.floatValue);
				GUILayout.EndHorizontal();

				//Planet Polycount
				GUILayout.BeginHorizontal();
				EditorGUILayout.PropertyField(planetPolycount);
				GUILayout.EndHorizontal();

				EndContents();
				//--- Planet General Settings End ---//

				//--- Planet Rotation Start ---//
				GUILayout.Space(-8f);

				DrawSubHeader("Rotation");
				BeginContents();
				//Planet Rotation
				GUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Angle", GUILayout.MaxWidth(100));
				planetRotation.vector3Value = EditorGUILayout.Vector3Field("", planetRotation.vector3Value, GUILayout.MinWidth(90));
				GUILayout.EndHorizontal();

				//Planet Rotation Speed
				GUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Rotation Speed", GUILayout.MaxWidth(100));
				planetRotationSpeed.vector3Value = EditorGUILayout.Vector3Field("", planetRotationSpeed.vector3Value, GUILayout.MinWidth(90));
				GUILayout.EndHorizontal();

				EndContents();
				//--- Planet Rotation End ---//

				//--- Planet Layout Start ---//
				GUILayout.Space(-8f);
				DrawSubHeader("Layout");
				BeginContents();

				//Modus
				GUI.contentColor = Color.black;
				GUILayout.BeginHorizontal();
				EditorStyles.popup.fixedHeight = 22;
				EditorStyles.popup.fontSize = 12;
				modusIndex.intValue = EditorGUILayout.Popup(modusIndex.intValue, modus, GUILayout.MinHeight(25));
				EditorStyles.popup.fixedHeight = 16;
				EditorStyles.popup.fontSize = 10;
				GUILayout.EndHorizontal();
				planetLayout.enumValueIndex = modusIndex.intValue;

				planetModus();

				EndContents();
				//--- Planet Layout End ---//
			}
			else {
				//Create Planet
				BeginContents();
				GUILayout.BeginHorizontal();
				GUILayout.Space(18);
				if (GUILayout.Button("Create New Planet")) {
					creatorScript.createPlanet();
				}
				GUILayout.Space(18);
				GUILayout.EndHorizontal();
				EndContents();
			}

			GUILayout.Space(5);
		}
		//--- Planet GUI End ---//

		GUILayout.Space(10);

		//--- Ring GUI Start ---//
		ringSettingsVisible = DrawMainHeader("Ring Settings", ringSettingsVisible);

		if (ringSettingsVisible) {

			if (ringObject.objectReferenceValue != null) {

				//--- Ring General Settings Start ---//
				DrawSubHeader("General");
				BeginContents();

				//Ring Size
				GUILayout.BeginHorizontal();
				ringSize.floatValue = EditorGUILayout.FloatField("Size", ringSize.floatValue);
				GUILayout.EndHorizontal();

				//Ring Polycount
				GUILayout.BeginHorizontal();
				EditorGUILayout.PropertyField(ringPolycount);
				GUILayout.EndHorizontal();

				EndContents();
				//--- Ring General Settings End ---//

				//--- Ring Rotation Start---//
				GUILayout.Space(-8f);

				DrawSubHeader("Rotation");
				BeginContents();
				//Ring Rotation Angle
				GUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Angle", GUILayout.MaxWidth(100));
				ringRotation.vector3Value = EditorGUILayout.Vector3Field("", ringRotation.vector3Value, GUILayout.MinWidth(90));
				GUILayout.EndHorizontal();

				//Ring Rotation Speed
				GUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Rotation Speed", GUILayout.MaxWidth(100));
				ringRotationSpeed.vector3Value = EditorGUILayout.Vector3Field("", ringRotationSpeed.vector3Value, GUILayout.MinWidth(90));
				GUILayout.EndHorizontal();

				EndContents();
				//--- Ring Rotation End---//

				//--- Ring Layout Start---//
				GUILayout.Space(-8f);
				DrawSubHeader("Layout");
				BeginContents();

				//Modus
				GUI.contentColor = Color.black;
				GUILayout.BeginHorizontal();
				EditorStyles.popup.fixedHeight = 22;
				EditorStyles.popup.fontSize = 12;
				ringModusIndex.intValue = EditorGUILayout.Popup(ringModusIndex.intValue, ringModus, GUILayout.MinHeight(25));
				EditorStyles.popup.fixedHeight = 16;
				EditorStyles.popup.fontSize = 10;
				GUILayout.EndHorizontal();
				ringLayout.enumValueIndex = ringModusIndex.intValue;

				ringLayoutModus();

				EndContents();
				//--- Ring Layout End---//

				//--- Ring Delete Start---//
				GUILayout.Space(-8f);
				DrawSubHeader("Remove");
				BeginContents();
				GUILayout.BeginHorizontal();
				GUILayout.Space(10);
				EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
				GUILayout.Space (0);
				if (GUILayout.Button("- Delete Ring Object", EditorStyles.boldLabel)) {
					creatorScript.deleteRing();
				}
				GUILayout.EndVertical ();
				GUILayout.Space(10);
				GUILayout.EndHorizontal();
				EndContents();
				//--- Ring Delete End---//
			}
			else {
				//--- Ring Create Start---//
				BeginContents();
				GUILayout.BeginHorizontal();
				GUILayout.Space(10);
				EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
				GUILayout.Space (0);
				if (GUILayout.Button("+ Create New Ring", EditorStyles.boldLabel)) {
					creatorScript.createRing();
				}
				GUILayout.EndVertical ();
				GUILayout.Space(10);
				GUILayout.EndHorizontal();
				EndContents();
				//--- Ring Create End---//
			}

			GUILayout.Space(5);
		}
		//--- Ring GUI End ---//

		GUILayout.Space(10);

		//--- Moon GUI Start ---//
		moonsSettingsVisible = DrawMainHeader("Moons Settings", moonsSettingsVisible);
		int i;
		if (moonsSettingsVisible) {
			BeginContents();
			for (i = 0; i < creatorScript.moons.Length; i++) {
				//For every single Moon
				moonModusIndex[i] = creatorScript.moons[i].moonModusIndex;
				GUILayout.BeginHorizontal ();
				GUILayout.Space (10);
				GUILayout.BeginVertical ();
				EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
				GUILayout.Space (0);
				if (!moonObjectsVisible[i]) {
					if (GUILayout.Button("▼ " + creatorScript.moons[i].moonName, EditorStyles.boldLabel)) {
						moonObjectsVisible[i] = true;
					}
					EditorGUILayout.EndVertical();
					GUILayout.EndHorizontal ();
					GUILayout.Space (10);
					GUILayout.EndHorizontal ();
				}
				else {
					if (GUILayout.Button("▲ " + creatorScript.moons[i].moonName, EditorStyles.boldLabel)) {
						moonObjectsVisible[i] = false;
					}
					EditorGUILayout.EndVertical();
					GUILayout.Space (-5);

					//--- Moon General Settings Start ---//
					DrawSubSubHeader("General");
					EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));

					GUILayout.Space (3);

					//Moon Name
					GUILayout.BeginHorizontal();
					creatorScript.moons[i].moonName = EditorGUILayout.TextField("Name", creatorScript.moons[i].moonName);
					GUILayout.EndHorizontal();

					//Moon Size
					GUILayout.BeginHorizontal();
					creatorScript.moons[i].moonSize = EditorGUILayout.FloatField("Size", creatorScript.moons[i].moonSize);
					GUILayout.EndHorizontal();

					//Moon Polycount
					GUILayout.BeginHorizontal();
					moonPolycountIndex = EditorGUILayout.Popup ("Moon Polycount", moonPolycountIndex, moonPolycount);
					if (moonPolycountIndex == 1) {
						creatorScript.moons[i].moonPolycount = TAFMoonObject.moonRes.Low;
					}
					else {
						creatorScript.moons[i].moonPolycount = TAFMoonObject.moonRes.High;
					}
					GUILayout.EndHorizontal();
					GUILayout.Space (3);
					GUILayout.EndVertical ();
					//--- Moon General Settings End ---//

					//--- Moon Rotation Start ---//
					GUILayout.Space(-5f);
					DrawSubSubHeader("Rotation");
					EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
					GUILayout.Space (3);
					//Moon Rotation Angle
					GUILayout.BeginHorizontal();
					creatorScript.moons[i].moonRotation = EditorGUILayout.Vector3Field("Angle", creatorScript.moons[i].moonRotation);
					GUILayout.EndHorizontal();

					//Moon Rotation Speed
					GUILayout.BeginHorizontal();
					creatorScript.moons[i].moonRotationSpeed = EditorGUILayout.Vector3Field("Rotation Speed", creatorScript.moons[i].moonRotationSpeed);
					GUILayout.EndHorizontal();
					GUILayout.Space (3);
					GUILayout.EndVertical ();
					//--- Moon Rotation End ---//

					//--- Moon Orbit Start ---//
					GUILayout.Space(-5f);
					DrawSubSubHeader("Orbit");
					EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
					GUILayout.Space (3);

					//Distance
					GUILayout.BeginHorizontal();
					creatorScript.moons[i].moonDistanceFromPlanet = EditorGUILayout.FloatField("Radius", creatorScript.moons[i].moonDistanceFromPlanet);
					GUILayout.EndHorizontal();

					//Orbit Angle
					GUILayout.BeginHorizontal();
					creatorScript.moons[i].moonPlanetRotation = EditorGUILayout.Vector3Field("Angle", creatorScript.moons[i].moonPlanetRotation, GUILayout.MinWidth(90));
					GUILayout.EndHorizontal();

					//Orbit Rotation Speed
					GUILayout.BeginHorizontal();
					creatorScript.moons[i].moonPlanetRotationSpeed = EditorGUILayout.FloatField("Speed", creatorScript.moons[i].moonPlanetRotationSpeed);
					GUILayout.EndHorizontal();
					GUILayout.Space (3);
					GUILayout.EndVertical ();
					//--- Moon Orbit End ---//

					//--- Moon Layout Start ---//
					GUILayout.Space(-5f);
					DrawSubSubHeader("Layout");
					EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
					GUILayout.Space (3);

					//Modus
					GUI.contentColor = Color.black;
					GUILayout.BeginHorizontal();
					EditorStyles.popup.fixedHeight = 22;
					EditorStyles.popup.fontSize = 12;
					moonModusIndex[i] = EditorGUILayout.Popup(moonModusIndex[i], moonModus, GUILayout.MinHeight(25));
					creatorScript.moons[i].moonModusIndex = moonModusIndex[i];
					EditorStyles.popup.fixedHeight = 16;
					EditorStyles.popup.fontSize = 10;
					GUILayout.EndHorizontal();

					moonLayoutModus(i);

					GUILayout.Space (3);
					GUILayout.EndVertical ();
					//--- Moon Layout End ---//

					//--- Moon Delete Start ---//
					GUILayout.Space(-5f);
					DrawSubSubHeader("Remove");
					EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
					GUILayout.Space (3);
					GUILayout.BeginHorizontal();
					GUILayout.Space(10);
					EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
					GUILayout.Space (0);
					if (GUILayout.Button("- Delete Moon", EditorStyles.boldLabel)) {
						creatorScript.deleteMoon(i);
						moonObjectsVisible[i] = false;
					}
					EditorGUILayout.EndVertical();
					GUILayout.Space(10);
					GUILayout.EndHorizontal();
					GUILayout.Space (5);
					//--- Moon Delete End ---//

					EditorGUILayout.EndVertical();
					GUILayout.Space (10);
					GUILayout.EndHorizontal ();
					GUILayout.Space (10);
					GUILayout.EndHorizontal ();
					GUILayout.Space (-10);
				}
			}
			//--- Moon Add Start ---//
			if (creatorScript.moons.Length < 4) {
				GUILayout.Space(5);
				GUILayout.BeginHorizontal();
				GUILayout.Space(10);
				EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
				GUILayout.Space (0);
				if (GUILayout.Button("+ Add New Moon", EditorStyles.boldLabel)) {
					creatorScript.addNewMoon();
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space(10);
				GUILayout.EndHorizontal();
				EndContents();
			}
			//--- Moon Add End ---//
		}
		//--- Moon GUI End ---//

		if (GUI.changed) {
			#if UNITY_EDITOR
			if (PrefabUtility.GetPrefabParent(creatorScript.gameObject) != null) {
				PrefabUtility.DisconnectPrefabInstance(creatorScript.gameObject);
			}
			#endif
			creatorScript.OnValidate();
		}

		serializedObject.ApplyModifiedProperties();
		GUILayout.Space (10);
	}

	//Defines the planet layout options
	void planetModus() {

		switch (modusIndex.intValue) {
		//Modus: Foreign
		case 0:
			//Texture Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
			GUILayout.Space (0);
			if (!foreignSurfaceVisible) {
				if (GUILayout.Button("▼ Surface", EditorStyles.boldLabel)) {
					foreignSurfaceVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Surface", EditorStyles.boldLabel)) {
					foreignSurfaceVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				//Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				EditorGUI.DrawPreviewTexture (new Rect (GUILayoutUtility.GetLastRect ().position.x + 10, GUILayoutUtility.GetLastRect ().position.y + 4, 75, 75), (Texture)foreignSurfaceTexture.objectReferenceValue);
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				//Texture Popup
				EditorGUILayout.LabelField ("Texture", GUILayout.MaxWidth (90));
				GUILayout.BeginHorizontal ();
				foreignTextureIndex.intValue = EditorGUILayout.Popup (foreignTextureIndex.intValue, foreignSurfaceTexturesNames);
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				//Primary Color
				EditorGUILayout.LabelField ("Primary Color", GUILayout.MaxWidth (90));
				EditorGUILayout.PropertyField (foreignPrimaryColor, new GUIContent (""));
				//Secondary Color
				EditorGUILayout.LabelField ("Secondary Color", GUILayout.MaxWidth (100));
				EditorGUILayout.PropertyField (foreignSecondaryColor, new GUIContent (""));
				//Atmosphere Color
				EditorGUILayout.LabelField ("Atmosphere Color", GUILayout.MaxWidth (110));
				EditorGUILayout.PropertyField (foreignAtmosphereColor, new GUIContent (""));
				GUILayout.Space (10);
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
				GUILayout.Space (-10);
			}

			//Specular Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button);
			GUILayout.Space (0);
			if (!foreignSpecularVisible) {
				if (GUILayout.Button("▼ Specular & Gloss", EditorStyles.boldLabel, GUILayout.MaxHeight(15f))) {
					foreignSpecularVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Specular & Gloss", EditorStyles.boldLabel,GUILayout.MaxHeight(15f))) {
					foreignSpecularVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				//Specular Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				EditorGUI.DrawPreviewTexture (new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), (Texture)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Editor Images/Specularity_Image.jpg", typeof(Texture)));

				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				//Specular & Gloss Settings
				EditorGUILayout.LabelField ("Color", GUILayout.MaxWidth (100));
				EditorGUILayout.PropertyField (foreignSpecColor, new GUIContent (""));
				EditorGUILayout.LabelField ("Specular", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (foreignSpecPower, new GUIContent (""));
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				EditorGUILayout.LabelField ("Gloss", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (foreignGlossPower, new GUIContent (""));
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}

			//Clouds Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button);
			GUILayout.Space (0);
			if (!foreignCloudsVisible) {
				if (GUILayout.Button("▼ Clouds", EditorStyles.boldLabel, GUILayout.MaxHeight(15f))) {
					foreignCloudsVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Clouds", EditorStyles.boldLabel,GUILayout.MaxHeight(15f))) {
					foreignCloudsVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				//Clouds Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				if (foreignCloudTexture.objectReferenceValue != null) {
					EditorGUI.DrawPreviewTexture (new Rect (GUILayoutUtility.GetLastRect ().position.x + 10, GUILayoutUtility.GetLastRect ().position.y + 4, 75, 75), (Texture)foreignCloudTexture.objectReferenceValue);
				}
				else {
					EditorGUI.DrawRect(new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), new Color32(82,82,82,255));
				}
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				//Texture Popup
				EditorGUILayout.LabelField ("Texture", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				foreignCloudTextureIndex.intValue = EditorGUILayout.Popup (foreignCloudTextureIndex.intValue, foreignCloudTexturesNames);
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				//Clouds Settings
				EditorGUILayout.LabelField ("Color", GUILayout.MaxWidth (100));
				EditorGUILayout.PropertyField (foreignCloudColor, new GUIContent (""));
				EditorGUILayout.LabelField ("Speed", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (cloudMovingSpeed, new GUIContent (""));
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}

			//RimLight Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button);
			GUILayout.Space (0);
			if (!foreignRimLightVisible) {
				if (GUILayout.Button("▼ Rim Light", EditorStyles.boldLabel, GUILayout.MaxHeight(15f))) {
					foreignRimLightVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Rim Light", EditorStyles.boldLabel,GUILayout.MaxHeight(15f))) {
					foreignRimLightVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				//RimLight Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				EditorGUI.DrawPreviewTexture (new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), (Texture)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Editor Images/Rimlight_Image.jpg", typeof(Texture)));
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical();
				//RimLight Settings
				EditorGUILayout.LabelField ("Color", GUILayout.MaxWidth (100));
				EditorGUILayout.PropertyField (foreignRimColor, new GUIContent (""));
				EditorGUILayout.LabelField ("Intensity", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (foreignRimPower, new GUIContent (""));
				GUILayout.Space(18);
				GUILayout.EndHorizontal ();
				EditorGUILayout.LabelField ("Range", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (foreignRimThickness, new GUIContent (""));
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}

			//Illumination Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button);
			GUILayout.Space (0);
			if (!foreignIlluminationVisible) {
				if (GUILayout.Button("▼ Illumination", EditorStyles.boldLabel, GUILayout.MaxHeight(15f))) {
					foreignIlluminationVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Illumination", EditorStyles.boldLabel,GUILayout.MaxHeight(15f))) {
					foreignIlluminationVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				//Clouds Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				if (foreignIlluminTexture.objectReferenceValue != null) {
					EditorGUI.DrawPreviewTexture (new Rect (GUILayoutUtility.GetLastRect ().position.x + 10, GUILayoutUtility.GetLastRect ().position.y + 4, 75, 75), (Texture)foreignIlluminTexture.objectReferenceValue);
				}
				else {
					EditorGUI.DrawRect(new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), new Color32(82,82,82,255));
				}
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				//Texture Popup
				EditorGUILayout.LabelField ("Texture", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				foreignIlluminTextureIndex.intValue = EditorGUILayout.Popup (foreignIlluminTextureIndex.intValue, foreignIlluminTexturesNames);
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				//Clouds Settings
				EditorGUILayout.LabelField ("Color", GUILayout.MaxWidth (100));
				EditorGUILayout.PropertyField (foreignIlluminColor, new GUIContent (""));
				EditorGUILayout.LabelField ("Dark Side Illumination", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (foreignDarkSideIllumin, new GUIContent (""));
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			break;
		//Modus: Earthlike
		case 1:
			//Texture Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
			GUILayout.Space (0);
			if (!earthlikeSurfaceVisible) {
				if (GUILayout.Button("▼ Surface", EditorStyles.boldLabel)) {
					earthlikeSurfaceVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Surface", EditorStyles.boldLabel)) {
					earthlikeSurfaceVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				//Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				EditorGUI.DrawPreviewTexture (new Rect (GUILayoutUtility.GetLastRect ().position.x + 10, GUILayoutUtility.GetLastRect ().position.y + 4, 75, 75), (Texture)earthlikeSurfaceTexture.objectReferenceValue);
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				//Texture Popup
				EditorGUILayout.LabelField ("Texture", GUILayout.MaxWidth (90));
				GUILayout.BeginHorizontal ();
				earthlikeTextureIndex.intValue = EditorGUILayout.Popup (earthlikeTextureIndex.intValue, earthlikeSurfaceTexturesNames);
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				//land Color
				EditorGUILayout.LabelField ("Land Color", GUILayout.MaxWidth (90));
				EditorGUILayout.PropertyField (earthlikeLandColor, new GUIContent (""));
				//Water Color
				EditorGUILayout.LabelField ("Water Color", GUILayout.MaxWidth (100));
				EditorGUILayout.PropertyField (earthlikeWaterColor, new GUIContent (""));
				//Ice Color
				EditorGUILayout.LabelField ("Ice Color", GUILayout.MaxWidth (100));
				EditorGUILayout.PropertyField (earthlikeIceColor, new GUIContent (""));
				//Atmosphere Color
				EditorGUILayout.LabelField ("Atmosphere Color", GUILayout.MaxWidth (110));
				EditorGUILayout.PropertyField (earthlikeAtmosphereColor, new GUIContent (""));
				GUILayout.Space (10);
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
				GUILayout.Space (-10);
			}

			//Specular Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button);
			GUILayout.Space (0);
			if (!earthlikeSpecularVisible) {
				if (GUILayout.Button("▼ Specular & Gloss", EditorStyles.boldLabel, GUILayout.MaxHeight(15f))) {
					earthlikeSpecularVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Specular & Gloss", EditorStyles.boldLabel,GUILayout.MaxHeight(15f))) {
					earthlikeSpecularVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				//Specular Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				EditorGUI.DrawTextureAlpha (new Rect (GUILayoutUtility.GetLastRect ().position.x + 10, GUILayoutUtility.GetLastRect ().position.y + 4, 75, 75), (Texture)earthlikeSurfaceTexture.objectReferenceValue);
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				//Specular & Gloss Settings
				EditorGUILayout.LabelField ("Color", GUILayout.MaxWidth (100));
				EditorGUILayout.PropertyField (earthlikeSpecColor, new GUIContent (""));
				EditorGUILayout.LabelField ("Specular", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (earthlikeSpecPower, new GUIContent (""));
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				EditorGUILayout.LabelField ("Gloss", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (earthlikeGlossPower, new GUIContent (""));
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}

			//Clouds Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button);
			GUILayout.Space (0);
			if (!earthlikeCloudsVisible) {
				if (GUILayout.Button("▼ Clouds", EditorStyles.boldLabel, GUILayout.MaxHeight(15f))) {
					earthlikeCloudsVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Clouds", EditorStyles.boldLabel,GUILayout.MaxHeight(15f))) {
					earthlikeCloudsVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				//Clouds Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				if (earthlikeCloudTexture.objectReferenceValue != null) {
					EditorGUI.DrawPreviewTexture (new Rect (GUILayoutUtility.GetLastRect ().position.x + 10, GUILayoutUtility.GetLastRect ().position.y + 4, 75, 75), (Texture)earthlikeCloudTexture.objectReferenceValue);
				}
				else {
					EditorGUI.DrawRect(new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), new Color32(82,82,82,255));
				}
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				//Texture Popup
				EditorGUILayout.LabelField ("Texture", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				earthlikeCloudTextureIndex.intValue = EditorGUILayout.Popup (earthlikeCloudTextureIndex.intValue, earthlikeCloudTexturesNames);
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				//Clouds Settings
				EditorGUILayout.LabelField ("Color", GUILayout.MaxWidth (100));
				EditorGUILayout.PropertyField (earthlikeCloudColor, new GUIContent (""));
				EditorGUILayout.LabelField ("Speed", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (cloudMovingSpeed, new GUIContent (""));
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}

			//RimLight Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button);
			GUILayout.Space (0);
			if (!earthlikeRimLightVisible) {
				if (GUILayout.Button("▼ Rim Light", EditorStyles.boldLabel, GUILayout.MaxHeight(15f))) {
					earthlikeRimLightVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Rim Light", EditorStyles.boldLabel,GUILayout.MaxHeight(15f))) {
					earthlikeRimLightVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				//RimLight Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				EditorGUI.DrawPreviewTexture (new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), (Texture)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Editor Images/Rimlight_Image.jpg", typeof(Texture)));
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical();
				//RimLight Settings
				EditorGUILayout.LabelField ("Color", GUILayout.MaxWidth (100));
				EditorGUILayout.PropertyField (earthlikeRimColor, new GUIContent (""));
				EditorGUILayout.LabelField ("Intensity", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (earthlikeRimPower, new GUIContent (""));
				GUILayout.Space(18);
				GUILayout.EndHorizontal ();
				EditorGUILayout.LabelField ("Range", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (earthlikeRimThickness, new GUIContent (""));
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}

			//Illumination Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button);
			GUILayout.Space (0);
			if (!earthlikeIlluminationVisible) {
				if (GUILayout.Button("▼ Illumination", EditorStyles.boldLabel, GUILayout.MaxHeight(15f))) {
					earthlikeIlluminationVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Illumination", EditorStyles.boldLabel,GUILayout.MaxHeight(15f))) {
					earthlikeIlluminationVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				//Clouds Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				if (earthlikeIlluminTexture.objectReferenceValue != null) {
					EditorGUI.DrawPreviewTexture (new Rect (GUILayoutUtility.GetLastRect ().position.x + 10, GUILayoutUtility.GetLastRect ().position.y + 4, 75, 75), (Texture)earthlikeIlluminTexture.objectReferenceValue);
				}
				else {
					EditorGUI.DrawRect(new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), new Color32(82,82,82,255));
				}
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				//Texture Popup
				EditorGUILayout.LabelField ("Texture", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				earthlikeIlluminTextureIndex.intValue = EditorGUILayout.Popup (earthlikeIlluminTextureIndex.intValue, earthlikeIlluminTexturesNames);
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				//Clouds Settings
				EditorGUILayout.LabelField ("Color", GUILayout.MaxWidth (100));
				EditorGUILayout.PropertyField (earthlikeIlluminColor, new GUIContent (""));
				EditorGUILayout.LabelField ("Dark Side Illumination", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (earthlikeDarkSideIllumin, new GUIContent (""));
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			break;
		//Modus: Moon
		case 2:
			//Texture Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
			GUILayout.Space (0);
			if (!mainMoonSurfaceVisible) {
				if (GUILayout.Button("▼ Surface", EditorStyles.boldLabel)) {
					mainMoonSurfaceVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Surface", EditorStyles.boldLabel)) {
					mainMoonSurfaceVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				//Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				EditorGUI.DrawPreviewTexture (new Rect (GUILayoutUtility.GetLastRect ().position.x + 10, GUILayoutUtility.GetLastRect ().position.y + 4, 75, 75), (Texture)mainMoonSurfaceTexture.objectReferenceValue);
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				//Texture Popup
				EditorGUILayout.LabelField ("Texture", GUILayout.MaxWidth (90));
				GUILayout.BeginHorizontal ();
				mainMoonTextureIndex.intValue = EditorGUILayout.Popup (mainMoonTextureIndex.intValue, mainMoonSurfaceTexturesNames);
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				//Primary Color
				EditorGUILayout.LabelField ("Primary Color", GUILayout.MaxWidth (90));
				EditorGUILayout.PropertyField (mainMoonPrimaryColor, new GUIContent (""));
				//Secondary Color
				EditorGUILayout.LabelField ("Secondary Color", GUILayout.MaxWidth (100));
				EditorGUILayout.PropertyField (mainMoonSecondaryColor, new GUIContent (""));
				GUILayout.Space (10);
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
				GUILayout.Space (-10);
			}

			//Specular Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button);
			GUILayout.Space (0);
			if (!mainMoonSpecularVisible) {
				if (GUILayout.Button("▼ Specular & Gloss", EditorStyles.boldLabel, GUILayout.MaxHeight(15f))) {
					mainMoonSpecularVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Specular & Gloss", EditorStyles.boldLabel,GUILayout.MaxHeight(15f))) {
					mainMoonSpecularVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				//Specular Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				EditorGUI.DrawPreviewTexture (new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), (Texture)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Editor Images/Specularity_Image.jpg", typeof(Texture)));
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				//Specular & Gloss Settings
				EditorGUILayout.LabelField ("Color", GUILayout.MaxWidth (100));
				EditorGUILayout.PropertyField (mainMoonSpecColor, new GUIContent (""));
				EditorGUILayout.LabelField ("Specular", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (mainMoonSpecPower, new GUIContent (""));
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				EditorGUILayout.LabelField ("Gloss", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (mainMoonGlossPower, new GUIContent (""));
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}

			//RimLight Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button);
			GUILayout.Space (0);
			if (!mainMoonRimLightVisible) {
				if (GUILayout.Button("▼ Rim Light", EditorStyles.boldLabel, GUILayout.MaxHeight(15f))) {
					mainMoonRimLightVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Rim Light", EditorStyles.boldLabel,GUILayout.MaxHeight(15f))) {
					mainMoonRimLightVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				//RimLight Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				EditorGUI.DrawPreviewTexture (new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), (Texture)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Editor Images/Rimlight_Image.jpg", typeof(Texture)));
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical();
				//RimLight Settings
				EditorGUILayout.LabelField ("Color", GUILayout.MaxWidth (100));
				EditorGUILayout.PropertyField (mainMoonRimColor, new GUIContent (""));
				EditorGUILayout.LabelField ("Intensity", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (mainMoonRimPower, new GUIContent (""));
				GUILayout.Space(18);
				GUILayout.EndHorizontal ();
				EditorGUILayout.LabelField ("Range", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (mainMoonRimThickness, new GUIContent (""));
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}

			//Illumination Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button);
			GUILayout.Space (0);
			if (!mainMoonIlluminationVisible) {
				if (GUILayout.Button("▼ Illumination", EditorStyles.boldLabel, GUILayout.MaxHeight(15f))) {
					mainMoonIlluminationVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Illumination", EditorStyles.boldLabel,GUILayout.MaxHeight(15f))) {
					mainMoonIlluminationVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				//Clouds Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				if (mainMoonIlluminTexture.objectReferenceValue != null) {
					EditorGUI.DrawPreviewTexture (new Rect (GUILayoutUtility.GetLastRect ().position.x + 10, GUILayoutUtility.GetLastRect ().position.y + 4, 75, 75), (Texture)mainMoonIlluminTexture.objectReferenceValue);
				}
				else {
					EditorGUI.DrawRect(new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), new Color32(82,82,82,255));
				}
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				//Texture Popup
				EditorGUILayout.LabelField ("Texture", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				mainMoonIlluminTextureIndex.intValue = EditorGUILayout.Popup (mainMoonIlluminTextureIndex.intValue, mainMoonIlluminTexturesNames);
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				//Clouds Settings
				EditorGUILayout.LabelField ("Color", GUILayout.MaxWidth (100));
				EditorGUILayout.PropertyField (mainMoonIlluminColor, new GUIContent (""));
				EditorGUILayout.LabelField ("Dark Side Illumination", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (mainMoonDarkSideIllumin, new GUIContent (""));
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			break;
		//Modus: Simple
		case 3:
			//Texture Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
			GUILayout.Space (0);
			if (!simpleSurfaceVisible) {
				if (GUILayout.Button("▼ Surface", EditorStyles.boldLabel)) {
					simpleSurfaceVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Surface", EditorStyles.boldLabel)) {
					simpleSurfaceVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				//Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				EditorGUI.DrawPreviewTexture (new Rect (GUILayoutUtility.GetLastRect ().position.x + 10, GUILayoutUtility.GetLastRect ().position.y + 4, 75, 75), (Texture)simpleTexture.objectReferenceValue);
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				//Texture Popup
				EditorGUILayout.LabelField ("Texture", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				simpleTextureIndex.intValue = EditorGUILayout.Popup (simpleTextureIndex.intValue, simpleTexturesNames);
				GUILayout.Space (10);
				GUILayout.EndVertical ();
				GUILayout.Space (45);
				GUILayout.EndHorizontal ();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
				GUILayout.Space (-10);
			}

			//Specular Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
			GUILayout.Space (0);
			if (!simpleSpecularVisible) {
				if (GUILayout.Button("▼ Specular & Gloss", EditorStyles.boldLabel)) {
					simpleSpecularVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Specular & Gloss", EditorStyles.boldLabel)) {
					simpleSpecularVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				//Specular Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				EditorGUI.DrawTextureAlpha (new Rect (GUILayoutUtility.GetLastRect ().position.x + 10, GUILayoutUtility.GetLastRect ().position.y + 4, 75, 75), (Texture)simpleTexture.objectReferenceValue);
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				//Specular & Gloss Settings
				EditorGUILayout.LabelField ("Color", GUILayout.MaxWidth (100));
				EditorGUILayout.PropertyField (simpleSpecColor, new GUIContent (""));
				EditorGUILayout.LabelField ("Specular", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (simpleSpecPower, new GUIContent (""));
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				EditorGUILayout.LabelField ("Gloss", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (simpleGlossPower, new GUIContent (""));
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}

			//RimLight Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
			GUILayout.Space (0);
			if (!simpleRimLightVisible) {
				if (GUILayout.Button("▼ RimLight", EditorStyles.boldLabel)) {
					simpleRimLightVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ RimLight", EditorStyles.boldLabel)) {
					simpleRimLightVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				//RimLight Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				EditorGUI.DrawPreviewTexture (new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), (Texture)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Editor Images/Rimlight_Image.jpg", typeof(Texture)));
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical();
				//RimLight Settings
				EditorGUILayout.LabelField ("Color", GUILayout.MaxWidth (100));
				EditorGUILayout.PropertyField (simpleRimColor, new GUIContent (""));
				EditorGUILayout.LabelField ("Intensity", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (simpleRimPower, new GUIContent (""));
				GUILayout.Space(18);
				GUILayout.EndHorizontal ();
				EditorGUILayout.LabelField ("Range", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (simpleRimThickness, new GUIContent (""));
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			break;
		//Modus: Custom
		case 4: 
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
			GUILayout.Space (0);
			if (!customPrefabVisible) {
				if (GUILayout.Button("▼ Planet Prefab (Optional)", EditorStyles.boldLabel)) {
					customPrefabVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Planet Prefab (Optional)", EditorStyles.boldLabel)) {
					customPrefabVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				if (customPlanet.objectReferenceValue != null && (Texture)AssetPreview.GetAssetPreview(customPlanet.objectReferenceValue) != null) {
					EditorGUI.DrawPreviewTexture (new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), (Texture)AssetPreview.GetAssetPreview(customPlanet.objectReferenceValue));
				}
				else {
					EditorGUI.DrawRect(new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), new Color32(82,82,82,255));
				}
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				EditorGUILayout.LabelField ("Prefab Object", GUILayout.MaxWidth (100));
				EditorGUILayout.PropertyField (customPlanet, new GUIContent (""));
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.Space (35);
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}

			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
			GUILayout.Space (0);
			if (!customMaterialVisible) {
				if (GUILayout.Button("▼ Planet Material (Optional)", EditorStyles.boldLabel)) {
					customMaterialVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Planet Material (Optional)", EditorStyles.boldLabel)) {
					customMaterialVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				if (customPlanetMaterial.objectReferenceValue != null && (Texture)AssetPreview.GetAssetPreview(customPlanetMaterial.objectReferenceValue) != null) {
					EditorGUI.DrawPreviewTexture (new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), (Texture)AssetPreview.GetAssetPreview(customPlanetMaterial.objectReferenceValue));
				}
				else {
					EditorGUI.DrawRect(new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), new Color32(82,82,82,255));
				}
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				EditorGUILayout.LabelField ("Material", GUILayout.MaxWidth (100));
				EditorGUILayout.PropertyField (customPlanetMaterial, new GUIContent (""));
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.Space (35);
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			break;
		}
	}

	//Defines ring layout options.
	void ringLayoutModus() {

		switch (ringModusIndex.intValue) {
		//Modus: Simple
		case 0:
			//Texture Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
			GUILayout.Space (0);
			if (!ringSimpleSurfaceVisible) {
				if (GUILayout.Button("▼ Surface", EditorStyles.boldLabel)) {
					ringSimpleSurfaceVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Surface", EditorStyles.boldLabel)) {
					ringSimpleSurfaceVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				//Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				EditorGUI.DrawPreviewTexture (new Rect (GUILayoutUtility.GetLastRect ().position.x + 10, GUILayoutUtility.GetLastRect ().position.y + 4, 75, 75), (Texture)ringSimpleSurfaceTexture.objectReferenceValue);
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				//Texture Popup
				EditorGUILayout.LabelField ("Texture", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				ringSimpleTextureIndex.intValue = EditorGUILayout.Popup (ringSimpleTextureIndex.intValue, ringSimpleSurfaceTexturesNames);
				GUILayout.Space (10);
				GUILayout.EndVertical ();
				GUILayout.Space (45);
				GUILayout.EndHorizontal ();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
				GUILayout.Space (-10);
			}
			break;
		//Modus: Advanced
		case 1:
			//Texture Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
			GUILayout.Space (0);
			if (!ringAdvancedSurfaceVisible) {
				if (GUILayout.Button("▼ Surface", EditorStyles.boldLabel)) {
					ringAdvancedSurfaceVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Surface", EditorStyles.boldLabel)) {
					ringAdvancedSurfaceVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				//Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				EditorGUI.DrawPreviewTexture (new Rect (GUILayoutUtility.GetLastRect ().position.x + 10, GUILayoutUtility.GetLastRect ().position.y + 4, 75, 75), (Texture)ringAdvancedSurfaceTexture.objectReferenceValue);
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				//Texture Popup
				EditorGUILayout.LabelField ("Texture", GUILayout.MaxWidth (90));
				GUILayout.BeginHorizontal ();
				ringAdvancedTextureIndex.intValue = EditorGUILayout.Popup (ringAdvancedTextureIndex.intValue, ringAdvancedSurfaceTexturesNames);
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				//Color1
				EditorGUILayout.LabelField ("Color 1", GUILayout.MaxWidth (90));
				EditorGUILayout.PropertyField (ringAdvancedColor1, new GUIContent (""));
				//Color2
				EditorGUILayout.LabelField ("Color 2", GUILayout.MaxWidth (100));
				EditorGUILayout.PropertyField (ringAdvancedColor2, new GUIContent (""));
				GUILayout.Space (10);
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
				GUILayout.Space (-10);
			}

			//Intensity
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
			GUILayout.Space (0);
			if (!ringAdvancedIntensityVisible) {
				if (GUILayout.Button("▼ Intensity", EditorStyles.boldLabel)) {
					ringAdvancedIntensityVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Intensity", EditorStyles.boldLabel)) {
					ringAdvancedIntensityVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				//Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				EditorGUI.DrawPreviewTexture (new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), (Texture)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Editor Images/Ring_Image.jpg", typeof(Texture)));
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				//Emission Power
				EditorGUILayout.LabelField ("Emission Power", GUILayout.MaxWidth (90));
				GUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (ringAdvancedEmissionPower, new GUIContent (""));
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				//Alpha
				EditorGUILayout.LabelField ("Transparency", GUILayout.MaxWidth (90));
				GUILayout.BeginHorizontal ();
				EditorGUILayout.PropertyField (ringAdvancedAlpha, new GUIContent (""));
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.Space (20);
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
				GUILayout.Space (-10);
			}
			break;
		//Modus: Custom
		case 2: 
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
			GUILayout.Space (0);
			if (!customRingPrefabVisible) {
				if (GUILayout.Button("▼ Ring Prefab (Optional)", EditorStyles.boldLabel)) {
					customRingPrefabVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Ring Prefab (Optional)", EditorStyles.boldLabel)) {
					customRingPrefabVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				if (customRing.objectReferenceValue != null && (Texture)AssetPreview.GetAssetPreview(customRing.objectReferenceValue) != null) {
					EditorGUI.DrawPreviewTexture (new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), (Texture)AssetPreview.GetAssetPreview(customRing.objectReferenceValue));
				}
				else {
					EditorGUI.DrawRect(new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), new Color32(82,82,82,255));
				}
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				EditorGUILayout.LabelField ("Prefab Object", GUILayout.MaxWidth (100));
				EditorGUILayout.PropertyField (customRing, new GUIContent (""));
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.Space (35);
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}

			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
			GUILayout.Space (0);
			if (!customRingMaterialVisible) {
				if (GUILayout.Button("▼ Ring Material (Optional)", EditorStyles.boldLabel)) {
					customRingMaterialVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Ring Material (Optional)", EditorStyles.boldLabel)) {
					customRingMaterialVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				if (customRingMaterial.objectReferenceValue != null && (Texture)AssetPreview.GetAssetPreview(customRingMaterial.objectReferenceValue) != null) {
					EditorGUI.DrawPreviewTexture (new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), (Texture)AssetPreview.GetAssetPreview(customRingMaterial.objectReferenceValue));
				}
				else {
					EditorGUI.DrawRect(new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), new Color32(82,82,82,255));
				}
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				EditorGUILayout.LabelField ("Material", GUILayout.MaxWidth (100));
				EditorGUILayout.PropertyField (customRingMaterial, new GUIContent (""));
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.Space (35);
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			break;
		}
	}

	//Defines the moon layout options.
	void moonLayoutModus(int i) {

		switch (moonModusIndex[i]) {
		//Modus: Simple
		case 0:
			creatorScript.moons[i].moonLayout = TAFMoonObject.moonStyle.Simple;
			//Texture Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
			GUILayout.Space (0);
			if (!moonSimpleSurfaceVisible) {
				if (GUILayout.Button("▼ Surface", EditorStyles.boldLabel)) {
					moonSimpleSurfaceVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Surface", EditorStyles.boldLabel)) {
					moonSimpleSurfaceVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				//Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				EditorGUI.DrawPreviewTexture (new Rect (GUILayoutUtility.GetLastRect ().position.x + 10, GUILayoutUtility.GetLastRect ().position.y + 4, 75, 75), creatorScript.moons[i].moonSimpleSurfaceTexture);
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				//Texture Popup
				EditorGUILayout.LabelField ("Texture", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				creatorScript.moons[i].moonSimpleTextureIndex = EditorGUILayout.Popup (creatorScript.moons[i].moonSimpleTextureIndex, moonSimpleSurfaceTexturesNames);
				GUILayout.Space (10);
				GUILayout.EndVertical ();
				GUILayout.Space (45);
				GUILayout.EndHorizontal ();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
				GUILayout.Space (-10);
			}

			//Specular Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
			GUILayout.Space (0);
			if (!moonSimpleSpecularVisible) {
				if (GUILayout.Button("▼ Specular & Gloss", EditorStyles.boldLabel)) {
					moonSimpleSpecularVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Specular & Gloss", EditorStyles.boldLabel)) {
					moonSimpleSpecularVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				//Specular Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				EditorGUI.DrawTextureAlpha (new Rect (GUILayoutUtility.GetLastRect ().position.x + 10, GUILayoutUtility.GetLastRect ().position.y + 4, 75, 75), creatorScript.moons[i].moonSimpleSurfaceTexture);
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				//Specular & Gloss Settings
				EditorGUILayout.LabelField ("Color", GUILayout.MaxWidth (90));
				creatorScript.moons[i].moonSimpleSpecColor = EditorGUILayout.ColorField (creatorScript.moons[i].moonSimpleSpecColor);
				EditorGUILayout.LabelField ("Specular", GUILayout.MaxWidth (90));
				GUILayout.BeginHorizontal ();
				creatorScript.moons[i].moonSimpleSpecPower = EditorGUILayout.FloatField (creatorScript.moons[i].moonSimpleSpecPower);
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				EditorGUILayout.LabelField ("Gloss", GUILayout.MaxWidth (90));
				GUILayout.BeginHorizontal ();
				creatorScript.moons[i].moonSimpleGlossPower = EditorGUILayout.FloatField (creatorScript.moons[i].moonSimpleGlossPower);
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}

			//RimLight Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
			GUILayout.Space (0);
			if (!moonSimpleRimLightVisible) {
				if (GUILayout.Button("▼ RimLight", EditorStyles.boldLabel)) {
					moonSimpleRimLightVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ RimLight", EditorStyles.boldLabel)) {
					moonSimpleRimLightVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				//RimLight Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				EditorGUI.DrawPreviewTexture (new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), (Texture)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Editor Images/Rimlight_Image.jpg", typeof(Texture)));
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical();
				//RimLight Settings
				EditorGUILayout.LabelField ("Color", GUILayout.MaxWidth (90));
				creatorScript.moons[i].moonSimpleRimColor = EditorGUILayout.ColorField (creatorScript.moons[i].moonSimpleRimColor);
				EditorGUILayout.LabelField ("Intensity", GUILayout.MaxWidth (90));
				GUILayout.BeginHorizontal ();
				creatorScript.moons[i].moonSimpleRimPower = EditorGUILayout.FloatField (creatorScript.moons[i].moonSimpleRimPower);
				GUILayout.Space(18);
				GUILayout.EndHorizontal ();
				EditorGUILayout.LabelField ("Range", GUILayout.MaxWidth (90));
				GUILayout.BeginHorizontal ();
				creatorScript.moons[i].moonSimpleRimThickness = EditorGUILayout.FloatField (creatorScript.moons[i].moonSimpleRimThickness);
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			break;
		//Modus: Advanced
		case 1:
			creatorScript.moons[i].moonLayout = TAFMoonObject.moonStyle.Advanced;
			//Texture Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
			GUILayout.Space (0);
			if (!moonAdvancedSurfaceVisible) {
				if (GUILayout.Button("▼ Surface", EditorStyles.boldLabel)) {
					moonAdvancedSurfaceVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Surface", EditorStyles.boldLabel)) {
					moonAdvancedSurfaceVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				//Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				EditorGUI.DrawPreviewTexture (new Rect (GUILayoutUtility.GetLastRect ().position.x + 10, GUILayoutUtility.GetLastRect ().position.y + 4, 75, 75), creatorScript.moons[i].moonAdvancedSurfaceTexture);
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				//Texture Popup
				EditorGUILayout.LabelField ("Texture", GUILayout.MaxWidth (90));
				GUILayout.BeginHorizontal ();
				creatorScript.moons[i].moonAdvancedTextureIndex = EditorGUILayout.Popup (creatorScript.moons[i].moonAdvancedTextureIndex, moonAdvancedSurfaceTexturesNames);
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				//Primary Color
				EditorGUILayout.LabelField ("Primary Color", GUILayout.MaxWidth (90));
				creatorScript.moons[i].moonAdvancedPrimaryColor = EditorGUILayout.ColorField (creatorScript.moons[i].moonAdvancedPrimaryColor);
				//Secondary Color
				EditorGUILayout.LabelField ("Secondary Color", GUILayout.MaxWidth (100));
				creatorScript.moons[i].moonAdvancedSecondaryColor = EditorGUILayout.ColorField (creatorScript.moons[i].moonAdvancedSecondaryColor);
				GUILayout.Space (10);
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
				GUILayout.Space (-10);
			}

			//Specular Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button);
			GUILayout.Space (0);
			if (!moonAdvancedSpecularVisible) {
				if (GUILayout.Button("▼ Specular & Gloss", EditorStyles.boldLabel, GUILayout.MaxHeight(15f))) {
					moonAdvancedSpecularVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Specular & Gloss", EditorStyles.boldLabel,GUILayout.MaxHeight(15f))) {
					moonAdvancedSpecularVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				//Specular Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				EditorGUI.DrawPreviewTexture (new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), (Texture)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Editor Images/Specularity_Image.jpg", typeof(Texture)));
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				//Specular & Gloss Settings
				EditorGUILayout.LabelField ("Color", GUILayout.MaxWidth (100));
				creatorScript.moons[i].moonAdvancedSpecColor = EditorGUILayout.ColorField (creatorScript.moons[i].moonAdvancedSpecColor);
				EditorGUILayout.LabelField ("Specular", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				creatorScript.moons[i].moonAdvancedSpecPower = EditorGUILayout.FloatField (creatorScript.moons[i].moonAdvancedSpecPower);
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				EditorGUILayout.LabelField ("Gloss", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				creatorScript.moons[i].moonAdvancedGlossPower = EditorGUILayout.FloatField (creatorScript.moons[i].moonAdvancedGlossPower);
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}

			//RimLight Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button);
			GUILayout.Space (0);
			if (!moonAdvancedRimLightVisible) {
				if (GUILayout.Button("▼ Rim Light", EditorStyles.boldLabel, GUILayout.MaxHeight(15f))) {
					moonAdvancedRimLightVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Rim Light", EditorStyles.boldLabel,GUILayout.MaxHeight(15f))) {
					moonAdvancedRimLightVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				//RimLight Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				EditorGUI.DrawPreviewTexture (new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), (Texture)AssetDatabase.LoadAssetAtPath("Assets/TAF's Planet Creator/Resources/Editor Images/Rimlight_Image.jpg", typeof(Texture)));
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical();
				//RimLight Settings
				EditorGUILayout.LabelField ("Color", GUILayout.MaxWidth (100));
				creatorScript.moons[i].moonAdvancedRimColor = EditorGUILayout.ColorField (creatorScript.moons[i].moonAdvancedRimColor);
				EditorGUILayout.LabelField ("Intensity", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				creatorScript.moons[i].moonAdvancedRimPower = EditorGUILayout.FloatField (creatorScript.moons[i].moonAdvancedRimPower);
				GUILayout.Space(18);
				GUILayout.EndHorizontal ();
				EditorGUILayout.LabelField ("Range", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				creatorScript.moons[i].moonAdvancedRimThickness = EditorGUILayout.FloatField (creatorScript.moons[i].moonAdvancedRimThickness);
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}

			//Illumination Label
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button);
			GUILayout.Space (0);
			if (!moonAdvancedIlluminVisible) {
				if (GUILayout.Button("▼ Illumination", EditorStyles.boldLabel, GUILayout.MaxHeight(15f))) {
					moonAdvancedIlluminVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Illumination", EditorStyles.boldLabel,GUILayout.MaxHeight(15f))) {
					moonAdvancedIlluminVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				//Illumination Texture Preview
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				if (creatorScript.moons[i].moonAdvancedIlluminTexture != null) {
					EditorGUI.DrawPreviewTexture (new Rect (GUILayoutUtility.GetLastRect ().position.x + 10, GUILayoutUtility.GetLastRect ().position.y + 4, 75, 75), creatorScript.moons[i].moonAdvancedIlluminTexture);
				}
				else {
					EditorGUI.DrawRect(new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), new Color32(82,82,82,255));
				}
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				//Texture Popup
				EditorGUILayout.LabelField ("Texture", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				creatorScript.moons[i].moonAdvancedIlluminTextureIndex = EditorGUILayout.Popup (creatorScript.moons[i].moonAdvancedIlluminTextureIndex, moonAdvancedIlluminTexturesNames);
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				EditorGUILayout.LabelField ("Color", GUILayout.MaxWidth (100));
				creatorScript.moons[i].moonAdvancedIlluminColor = EditorGUILayout.ColorField (creatorScript.moons[i].moonAdvancedIlluminColor);
				EditorGUILayout.LabelField ("Dark Side Illumination", GUILayout.MaxWidth (100));
				GUILayout.BeginHorizontal ();
				creatorScript.moons[i].moonAdvancedDarkSideIllumin = EditorGUILayout.FloatField (creatorScript.moons[i].moonAdvancedDarkSideIllumin);
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			break;
		//Modus: Custom
		case 2: 
			creatorScript.moons[i].moonLayout = TAFMoonObject.moonStyle.Custom;
			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
			GUILayout.Space (0);
			if (!customMoonPrefabVisible) {
				if (GUILayout.Button("▼ Moon Prefab (Optional)", EditorStyles.boldLabel)) {
					customMoonPrefabVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Moon Prefab (Optional)", EditorStyles.boldLabel)) {
					customMoonPrefabVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				if (creatorScript.moons[i].moonPrefab!= null && (Texture)AssetPreview.GetAssetPreview(creatorScript.moons[i].moonPrefab) != null) {
					EditorGUI.DrawPreviewTexture (new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), (Texture)AssetPreview.GetAssetPreview(creatorScript.moons[i].moonPrefab));
				}
				else {
					EditorGUI.DrawRect(new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), new Color32(82,82,82,255));
				}
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				EditorGUILayout.LabelField ("Prefab Object", GUILayout.MaxWidth (100));
				creatorScript.moons[i].moonPrefab = (GameObject)EditorGUILayout.ObjectField("", creatorScript.moons[i].moonPrefab, typeof(GameObject), false);
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.Space (35);
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}

			GUILayout.BeginHorizontal ();
			GUILayout.Space (10);
			GUILayout.BeginVertical ();
			EditorGUILayout.BeginVertical(GUI.skin.button, GUILayout.MinHeight(10f));
			GUILayout.Space (0);
			if (!customMoonMaterialVisible) {
				if (GUILayout.Button("▼ Moon Material (Optional)", EditorStyles.boldLabel)) {
					customMoonMaterialVisible = true;
				}
				EditorGUILayout.EndVertical();
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			else {
				if (GUILayout.Button("▲ Moon Material (Optional)", EditorStyles.boldLabel)) {
					customMoonMaterialVisible = false;
				}
				EditorGUILayout.EndVertical();
				GUILayout.Space (-5);
				EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(10f));
				GUILayout.Space (3);
				GUILayout.BeginHorizontal ();
				GUILayout.FlexibleSpace ();
				EditorGUILayout.LabelField ("", GUILayout.MaxWidth (90));
				if (creatorScript.moons[i].customMoonMaterial != null && (Texture)AssetPreview.GetAssetPreview(creatorScript.moons[i].customMoonMaterial) != null) {
					EditorGUI.DrawPreviewTexture (new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), (Texture)AssetPreview.GetAssetPreview(creatorScript.moons[i].customMoonMaterial));
				}
				else {
					EditorGUI.DrawRect(new Rect(GUILayoutUtility.GetLastRect().position.x+10, GUILayoutUtility.GetLastRect().position.y+4, 75,75), new Color32(82,82,82,255));
				}
				GUILayout.FlexibleSpace ();
				GUILayout.BeginVertical ();
				EditorGUILayout.LabelField ("Material", GUILayout.MaxWidth (100));
				creatorScript.moons[i].customMoonMaterial = (Material)EditorGUILayout.ObjectField ("", creatorScript.moons[i].customMoonMaterial, typeof(Material), false);
				GUILayout.Space (18);
				GUILayout.EndHorizontal ();
				GUILayout.EndVertical ();
				GUILayout.Space (35);
				GUILayout.EndHorizontal ();
				GUILayout.Space (10);
				EditorGUILayout.EndVertical();
				GUILayout.Space (10);
				GUILayout.EndHorizontal ();
			}
			break;
		}
	}

	//Defines the beginning of a content area.
	public void BeginContents ()
	{
		GUILayout.BeginHorizontal();
		EditorGUILayout.BeginHorizontal(GUI.skin.textArea, GUILayout.MinHeight(10f));
		GUILayout.BeginVertical();
		GUILayout.Space(5f);
	}

	//Defines the end of a content area.
	public void EndContents ()
	{
		GUILayout.Space(5f);
		GUILayout.EndVertical();
		EditorGUILayout.EndHorizontal();

		GUILayout.Space(10f);
		GUILayout.EndHorizontal();

		GUILayout.Space(3f);
	}

	//Displays a headline with given text.
	public bool DrawMainHeader (string text, bool isVisible)
	{
		GUILayout.BeginHorizontal();
		EditorGUILayout.BeginHorizontal(GUI.skin.button, GUILayout.MinHeight(20f));
		if (isVisible) {
			text = "<b>▲<size=13>" + text + "</size></b>";
			if (GUILayout.Button(text, headlineStyle)) {
				EditorGUILayout.EndHorizontal();
				GUILayout.Space(10f);
				GUILayout.EndHorizontal();
				GUI.backgroundColor = Color.white;
				GUILayout.Space(-5f);
				return false;
			}
			else {
				EditorGUILayout.EndHorizontal();
				GUILayout.Space(10f);
				GUILayout.EndHorizontal();
				GUI.backgroundColor = Color.white;
				GUILayout.Space(-5f);
				return true;
			}
		}
		else {
			text = "<b>▼<size=13>" + text + "</size></b>";
			if (GUILayout.Button(text, headlineStyle)) {
				EditorGUILayout.EndHorizontal();
				GUILayout.Space(10f);
				GUILayout.EndHorizontal();
				GUI.backgroundColor = Color.white;
				GUILayout.Space(-5f);
				return true;
			}
			else {
				EditorGUILayout.EndHorizontal();
				GUILayout.Space(10f);
				GUILayout.EndHorizontal();
				GUI.backgroundColor = Color.white;
				GUILayout.Space(-5f);
				return false;
			}
		}
	}

	//Displays a subheadline with given text.
	public void DrawSubHeader (string text)
	{
		GUILayout.BeginHorizontal();
		EditorGUILayout.BeginHorizontal(GUI.skin.box, GUILayout.MinHeight(10f));

		text = "<b><size=11> " + text + "</size></b>";
		GUILayout.Label(text, headlineStyle);
		EditorGUILayout.EndHorizontal();
		GUILayout.Space(10f);
		GUILayout.EndHorizontal();
		GUI.backgroundColor = Color.white;
		GUILayout.Space(-5f);
	}

	//Displays a subsubheadline with given text.
	public void DrawSubSubHeader (string text)
	{
		GUILayout.BeginHorizontal();
		EditorGUILayout.BeginHorizontal(GUI.skin.box, GUILayout.MinHeight(10f));

		text = "<b><size=11> " + text + "</size></b>";
		GUILayout.Label(text, headlineStyle);
		EditorGUILayout.EndHorizontal();
		GUILayout.EndHorizontal();
		GUI.backgroundColor = Color.white;
		GUILayout.Space(-5f);
	}
}

#if UNITY_EDITOR
//Class to readjust the materials after play mode
[InitializeOnLoad]
public static class PlayStateNotifier
{
	//Initialize check for playmodeStateChange and EditorUpdate
	static PlayStateNotifier()
	{
		EditorApplication.playModeStateChanged += ModeChanged;
	} 

	//Reacts on playmodeStateChange
	static void ModeChanged (PlayModeStateChange state)
	{
		if (state == PlayModeStateChange.EnteredEditMode) {
			foreach (TAFPlanetCreator script in Resources.FindObjectsOfTypeAll(typeof(TAFPlanetCreator)) as TAFPlanetCreator[]) {
				script.OnValidate();
			}
		}
	}
}
#endif