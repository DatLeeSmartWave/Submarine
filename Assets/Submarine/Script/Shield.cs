﻿using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	public GameObject colliderFX;
	public AudioClip soundCollider;
	Animator anim;

	void Start(){
		anim = GetComponent<Animator> ();
	}
	
	public void Close(){
		anim.SetTrigger ("Close");
	}

	//call by animation event
	public void ClosebyAnimator(){
		gameObject.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D other){
		var Enemy = other.GetComponent<Enemy> ();

		if (Enemy) {
			Enemy.Hit (int.MaxValue);		//kill all enemies

			SoundManager.PlaySfx (soundCollider);
			if (colliderFX != null)
				Instantiate (colliderFX, other.gameObject.transform.position, Quaternion.identity);
		}
	}
}
