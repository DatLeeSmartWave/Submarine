/// <summary>
/// This object is used to trigger when detect play then set all object active
/// </summary>
using UnityEngine;
using System.Collections;

public class TriggerEnemy : MonoBehaviour {
	public GameObject[] Objects;

	// Use this for initialization
	void Awake () {
		if (Objects.Length == 0) {
			gameObject.SetActive (false);
			return;
		}
		foreach (var obj in Objects)
			obj.SetActive (false);
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<Player> ()) {
			foreach (var obj in Objects)
				obj.SetActive (true);
		}
	}
}
