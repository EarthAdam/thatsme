using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyActivator : MonoBehaviour {
	private Animator animator;
	void Awake (){
		animator = GetComponent <Animator>();
	}
	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Boy"){
			animator.SetBool("move", true);
		
	}}

	}

