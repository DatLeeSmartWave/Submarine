using UnityEngine;
using System.Collections;

public class PowerUpMagnet : MonoBehaviour {
	void Update(){
		if (GameManager.Instance.State == GameManager.GameState.Playing)
			transform.Translate (GameManager.Instance.Speed * -1 * Time.deltaTime, 0, 0);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<Player> ()) {
			other.GetComponent<Player> ().UseMagnet ();
			SoundManager.PlaySfx (GameManager.Instance.SoundManager.soundPowerUpMagnet);

			GlobalValue.CollectMagnetPowerUp++;

			Destroy (gameObject);
		}
	}

	void OnbecameInvisible(){
		Destroy (gameObject);
	}
}
