using UnityEngine;
using System.Collections;

public class PowerUpGun : MonoBehaviour {
	public int bulletAdded = 20;
	void Update(){
		if (GameManager.Instance.State == GameManager.GameState.Playing)
			transform.Translate (GameManager.Instance.Speed * -1 * Time.deltaTime, 0, 0);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<Player> ()) {
			GlobalValue.Bullet += bulletAdded;
			SoundManager.PlaySfx (GameManager.Instance.SoundManager.soundPowerUpGun);

			GlobalValue.CollectBulletPowerUp++;

			Destroy (gameObject);
		}
	}

	void OnbecameInvisible(){
		Destroy (gameObject);
	}
}
