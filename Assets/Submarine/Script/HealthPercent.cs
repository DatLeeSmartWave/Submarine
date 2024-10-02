/// <summary>
/// this object is UI, and it will be shown everytime the player change its health and will be hide after a delay time 
/// </summary>
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthPercent : MonoBehaviour {

	public float delay = 1.5f;
	public Vector2 offset;
	float time;
	Text healthPercent;

	int priHealth;
	int originalHealth;

	void Start(){
		healthPercent = GetComponent<Text> ();
		priHealth = GameManager.Instance.Player.health;
		originalHealth = priHealth;
		healthPercent.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = GameManager.Instance.Player.transform.position + (Vector3)offset;

		time += Time.deltaTime;
		if (time >= delay || GameManager.Instance.Player.health <= 0)
			healthPercent.enabled = false;

		if (priHealth != GameManager.Instance.Player.health) {
			time = 0;
			healthPercent.enabled = true;
			healthPercent.text = (((float)GameManager.Instance.Player.health / (float)originalHealth) *100).ToString("0") + "%";
		}

		priHealth = GameManager.Instance.Player.health;
	}
}
