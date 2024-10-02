using UnityEngine;
using System.Collections;

public class Magnet : MonoBehaviour {
	float timer;
	float timeCounter = 0;
	// Use this for initialization
	public void init (float time) {
		timer = time;
		timeCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timeCounter += Time.deltaTime;
		if (timeCounter >= timer)
			gameObject.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Coin")) {
			other.gameObject.AddComponent<MoveToPlayer> ();
		}
	}
}
