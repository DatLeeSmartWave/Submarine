using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int damage = 20;
	public int score = 10;
	public GameObject destroyFX;
	public AudioClip soundDestroy;

	//called by Player
	public virtual void Hit(int takedamage){
		if (gameObject.CompareTag ("Bomb")) {
			SoundManager.PlaySfx (GameManager.Instance.SoundManager.soundExplosion);

		}

		if (destroyFX)
			Instantiate (destroyFX, transform.position, Quaternion.identity);

		SoundManager.PlaySfx (soundDestroy);

		GameManager.Instance.Score += score;
		GameManager.Instance.ShowFloatingText (score + "", transform.position, Color.yellow);

		if (gameObject.CompareTag ("Bomb"))
			GlobalValue.BombDestroy++;
		
		Destroy (gameObject);
	}
}
