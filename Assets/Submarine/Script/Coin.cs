using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
	
	public GameObject CollectedEffect;

	//called by Submarine script
	public void Collect(){
		if (CollectedEffect != null)
			Instantiate (CollectedEffect, transform.position, Quaternion.identity);

		SoundManager.PlaySfx (GameManager.Instance.SoundManager.soundCollectCoin);

		Destroy (gameObject);
	}
}
