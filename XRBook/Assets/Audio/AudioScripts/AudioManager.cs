using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {


    public AudioClip audioClip;
    public AudioSource audioSource;
    public AudioClip[] narrationClips = {  };
    public string bookLetter = "";


    void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start ()
    {
        audioSource.clip = audioClip;

        if (bookLetter == "A") { 
            audioClip = narrationClips[0];
        } else if (bookLetter == "B")
        {
            audioClip = narrationClips[1];
        }
	}
	
	// Update is called once per frame
	void Update () {

        // Should change this to on componentOverlap with hand
        MyInput();
        
	}
    void MyInput ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            audioSource.PlayOneShot(audioClip,0.8f);
    }
}
