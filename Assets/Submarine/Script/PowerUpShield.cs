using UnityEngine;
using System.Collections;

public class PowerUpShield : MonoBehaviour {
	void Update(){
		if (GameManager.Instance.State == GameManager.GameState.Playing)
			transform.Translate (GameManager.Instance.Speed * -1 * Time.deltaTime, 0, 0);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<Player> ()) {
			other.GetComponent<Player> ().shieldEnegry = 100;
			other.GetComponent<Player> ().UseShield ();

			GlobalValue.CollectShieldPowerUp++;

			Destroy (gameObject);
		}
	}

	void OnbecameInvisible(){
		Destroy (gameObject);
	}
}
