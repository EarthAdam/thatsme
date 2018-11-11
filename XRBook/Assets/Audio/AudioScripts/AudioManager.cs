using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip audioClip;
    public AudioClip liftOffAudioClip;
    public AudioSource audioSource;

    // Use this for initialization
	void Start () {
        audioSource.clip = audioClip;
	}
	
	// Update is called once per frame
	void Update () {
    // Should change this to on componentOverlap with hand
        if (Input.GetKeyDown(KeyCode.Space))
            audioSource.Stop();
        
	}
}
